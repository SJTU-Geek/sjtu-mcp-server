using ModelContextProtocol.Server;
using SJTUGeek.MCP.Server.Tools;
using SJTUGeek.MCP.Server.Tools.SjtuJw;
using SJTUGeek.MCP.Server.Tools.SjtuVenue;

namespace SJTUGeek.MCP.Server.Extensions
{
    public static class McpScriptBuilderExtensions
    {
        public static void ConfigureMcpOptions(McpServerOptions options)
        {
            options.Capabilities ??= new();
            options.Capabilities.Tools ??= new();
            options.Capabilities.Tools.ToolCollection ??= new();
        }

        public static IMcpServerBuilder WithAllMyTools(this IMcpServerBuilder builder)
        {
            return builder
                .WithTools<SjtuVenueTool>()
                .WithTools<SjtuMailTool>()
                .WithTools<SjtuJwTool>();

            //var schemaCreateOptions = new Microsoft.Extensions.AI.AIJsonSchemaCreateOptions()
            //{
            //    TransformOptions = new Microsoft.Extensions.AI.AIJsonSchemaTransformOptions()
            //    {
            //        RequireAllProperties = true,
            //    }
            //};

            //foreach (var toolType in toolTypes)
            //{
            //    if (toolType is not null)
            //    {
            //        foreach (var toolMethod in toolType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            //        {
            //            if (toolMethod.GetCustomAttribute<McpServerToolAttribute>() is not null)
            //            {
            //                builder.Services.AddSingleton((Func<IServiceProvider, McpServerTool>)(toolMethod.IsStatic ?
            //                    services => McpServerTool.Create(toolMethod, options: new() { Services = services, SchemaCreateOptions = schemaCreateOptions }) :
            //                    services => McpServerTool.Create(toolMethod, r => CreateTarget(r.Services, toolType), new() { Services = services, SchemaCreateOptions = schemaCreateOptions })));
            //            }
            //        }
            //    }
            //}

            //return builder;
        }
    }
}
