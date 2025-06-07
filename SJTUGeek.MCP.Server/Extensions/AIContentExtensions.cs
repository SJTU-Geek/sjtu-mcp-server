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

        public static Content ToContent(this AIContent content) =>
        content switch
        {
            TextContent textContent => new()
            {
                Text = textContent.Text,
                Type = "text",
            },

            DataContent dataContent => new()
            {
                Data = dataContent.GetBase64Data(),
                MimeType = dataContent.MediaType,
                Type =
                    dataContent.HasTopLevelMediaType("image") ? "image" :
                    dataContent.HasTopLevelMediaType("audio") ? "audio" :
                    "resource",
            },

            _ => new()
            {
                Text = JsonSerializer.Serialize(content, McpJsonUtilities.DefaultOptions.GetTypeInfo(typeof(object))),
                Type = "text",
            }
        };
    }
}
