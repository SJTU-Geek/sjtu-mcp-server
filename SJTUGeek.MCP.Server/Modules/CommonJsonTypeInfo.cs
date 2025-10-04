using System.Text.Json.Serialization;

namespace SJTUGeek.MCP.Server.Modules
{
    [JsonSerializable(typeof(Dictionary<string, string>))]
    internal partial class CommonJsonContext : JsonSerializerContext
    {
    }
}
