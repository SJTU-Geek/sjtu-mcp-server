using Microsoft.Extensions.AI;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using SJTUGeek.MCP.Server.Extensions;
using System.ComponentModel;

namespace SJTUGeek.MCP.Server.StaticTools;

[McpServerToolType]
public class TestTool
{
    [McpServerTool(Name = "test"), Description("Test system.")]
    public static CallToolResponse Test(RequestContext<CallToolRequestParams> context)
    {
        byte[] bytes = File.ReadAllBytes(@"C:\Users\teru\Downloads\test_img.jpg");
        return new CallToolResponse() { IsError = false, Content = new List<Content>() { 
            new Content() { Text = "场景1" } ,
            new DataContent(bytes, "image/png").ToContent(),
        } };
    }
}
