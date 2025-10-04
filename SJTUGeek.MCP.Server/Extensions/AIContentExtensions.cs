using Microsoft.Extensions.AI;
using ModelContextProtocol;
using ModelContextProtocol.Protocol;
using System.Text.Json;

namespace SJTUGeek.MCP.Server.Extensions
{
    public static class AIContentExtensions
    {
        public static string GetBase64Data(this DataContent dataContent)
        {
#if NET
            return Convert.ToBase64String(dataContent.Data.Span);
#else
        return MemoryMarshal.TryGetArray(dataContent.Data, out ArraySegment<byte> segment) ?
            Convert.ToBase64String(segment.Array!, segment.Offset, segment.Count) :
            Convert.ToBase64String(dataContent.Data.ToArray());
#endif
        }

        public static ContentBlock ToContent(this AIContent content) =>
        content switch
        {
            TextContent textContent => new TextContentBlock
            {
                Text = textContent.Text,
            },

            DataContent dataContent when dataContent.HasTopLevelMediaType("image") => new ImageContentBlock
            {
                Data = dataContent.Base64Data.ToString(),
                MimeType = dataContent.MediaType,
            },

            DataContent dataContent when dataContent.HasTopLevelMediaType("audio") => new AudioContentBlock
            {
                Data = dataContent.Base64Data.ToString(),
                MimeType = dataContent.MediaType,
            },

            DataContent dataContent => new EmbeddedResourceBlock
            {
                Resource = new BlobResourceContents
                {
                    Blob = dataContent.Base64Data.ToString(),
                    MimeType = dataContent.MediaType,
                }
            },

            _ => new TextContentBlock
            {
                Text = JsonSerializer.Serialize(content, McpJsonUtilities.DefaultOptions.GetTypeInfo(typeof(object))),
            }
        };
    }
}
