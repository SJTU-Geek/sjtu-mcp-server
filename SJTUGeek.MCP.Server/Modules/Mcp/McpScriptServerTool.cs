using Microsoft.Extensions.AI;
using ModelContextProtocol.Protocol;
using Python.Runtime;
using SJTUGeek.MCP.Server.Extensions;
using SJTUGeek.MCP.Server.Models;
using SJTUGeek.MCP.Server.Modules;
using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelContextProtocol.Server;

public class McpScriptServerTool : McpServerTool
{
    public static List<McpScriptServerTool> CreateBatch(string scriptName)
    {
        try
        {
            List<McpScriptServerTool> list = new List<McpScriptServerTool>();
            using (Py.GIL())
            {
                using (PyModule scope = Py.CreateScope())
                {
                    scope.Import(
                        "scripts." + Path.GetFileNameWithoutExtension(scriptName), "tool"
                    );
                    scope.Import("json");
                    var metadata = scope.Eval("json.dumps(tool.METADATA)");
                    var metadata_json = JsonSerializer.Deserialize<ScriptInfo>(metadata.ToString());

                    foreach (var toolInfo in metadata_json.Tools)
                    {
                        
                        Tool toolDef = new Tool();
                        toolDef.Name = toolInfo.Name;
                        toolDef.Description = toolInfo.Description;
                        toolDef.InputSchema = toolInfo.Schema;

                        list.Add(new McpScriptServerTool(scriptName, toolInfo, toolDef)
                        {
                            CategoryName = metadata_json.Category
                        });
                    }
                }
            }
            return list;
        } 
        catch (PythonException ex)
        {
            throw;
        }
    }
     
    public string ScriptName { get; }
    public string CategoryName { get; set; }
    public ScriptToolInfo ToolInfo { get; }

    /// <summary>Initializes a new instance of the <see cref="McpServerTool"/> class.</summary>
    private McpScriptServerTool(string scriptName, ScriptToolInfo toolInfo, Tool toolDef)
    {
        ScriptName = scriptName;
        ToolInfo = toolInfo;
        ProtocolTool = toolDef;
    }

    /// <inheritdoc />
    public override string ToString() => ProtocolTool.Name.ToString();

    /// <inheritdoc />
    public override Tool ProtocolTool { get; }

    public override IReadOnlyList<object> Metadata => [];

    /// <inheritdoc />
    public override async ValueTask<CallToolResult> InvokeAsync(
        RequestContext<CallToolRequestParams> request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        object? result;
        try
        {
            using (Py.GIL())
            {
                ScriptToolSchema schema = JsonSerializer.Deserialize<ScriptToolSchema>(ToolInfo.Schema);
                var positionalArguments = new List<PyObject>();
                var keywordArguments = new PyDict();
                var argDict = request.Params?.Arguments;
                //if (argDict is not null)
                //{
                //    foreach (var kvp in argDict)
                //    {
                //        arguments.Add(kvp.Value.GetCommonValue().ToPython());
                //    }
                //}
                foreach (var definedArg in schema.Properties)
                {
                    if (argDict is not null)
                    {
                        if (schema.Required.Contains(definedArg.Key))
                        {
                            //positional argument
                            if (argDict.ContainsKey(definedArg.Key))
                            {
                                positionalArguments.Add(argDict[definedArg.Key].GetCommonValue().ToPython());
                            }
                            else
                            {
                                //required but not present
                                throw new ArgumentException("argument missing");
                            }
                        }
                        else
                        {
                            //keyword argument
                            if (argDict.ContainsKey(definedArg.Key))
                            {
                                keywordArguments.SetItem(definedArg.Key, argDict[definedArg.Key].GetCommonValue().ToPython());
                            }
                            else
                            {
                                //use its default value
                            }
                        }
                    }
                    else
                    {
                        if (!schema.Required.Contains(definedArg.Key))
                        {
                            //keyword argument
                            //use its default value
                        }
                        else //required but not present
                        {
                            throw new ArgumentException("argument missing");
                        }
                    }
                }

                using (PyModule scope = Py.CreateScope())
                {
                    //PyModule context_module = Py.CreateScope("mcp_context");
                    //context_module.Set("JaAuthCookie", cookieProvider.GetCookie());
                    //context_module.Set("ServiceProvider", request.Services);

                    //scope.Import("scripts.base");
                    PyObject context_module = scope.Import("scripts.base.mcp_context");
                    McpScriptToolExecuteContext context = new McpScriptToolExecuteContext(request.Services!);
                    context_module.SetAttr("CONTEXT", context.ToPython());

                    PyObject tool_module = scope.Import(
                        "scripts." + Path.GetFileNameWithoutExtension(ScriptName)
                    );
                    var callResult = tool_module.InvokeMethod(ToolInfo.EntryPoint, positionalArguments.ToArray(), keywordArguments);

                    if (PyTuple.IsTupleType(callResult))
                    {
                        if (callResult.GetItem(0).As<bool>() == false)
                        {
                            var errMessage = callResult.GetItem(1).As<string>();
                            return new CallToolResult()
                            {
                                IsError = true,
                                Content = [new TextContentBlock() { Text = errMessage, Type = "text" }],
                            }; 
                        }
                        callResult = callResult.GetItem(1);
                    }
                    if (PyString.IsStringType(callResult))
                    {
                        result = callResult.As<string>();
                    }
                    else if (callResult == PyType.None)
                    {
                        result = null;
                    }
                    else
                    {
                        dynamic pyJson = scope.Import("json");
                        var callResultJSON = pyJson.dumps(callResult);
                        result = JsonSerializer.Deserialize<JsonElement>(callResultJSON.ToString());
                    }
                }
            }
        }
        catch (PythonException ex)
        {
            return new CallToolResult()
            {
                IsError = true,
                Content = [new TextContentBlock() { Text = ex.Message, Type = "text" }],
            };
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            return new CallToolResult()
            {
                IsError = true,
                Content = [new TextContentBlock() { Text = $"An error occurred invoking '{request.Params?.Name}'.", Type = "text" }],
            };
        }

        return result switch
        {
            JsonElement json => ConvertJsonElementToCallToolResult(json),
            null => new()
            {
                Content = []
            },

            string text => new()
            {
                Content = [new TextContentBlock() { Text = text, Type = "text" }]
            },

            //Content content => new()
            //{
            //    Content = [content]
            //},

            //IEnumerable<string> texts => new()
            //{
            //    Content = [.. texts.Select(x => new Content() { Type = "text", Text = x ?? string.Empty })]
            //},

            //IEnumerable<Content> contents => new()
            //{
            //    Content = [.. contents]
            //},

            CallToolResult CallToolResult => CallToolResult,

            _ => new()
            {
                Content = [new TextContentBlock()
                {
                    Text = JsonSerializer.Serialize(result, McpJsonUtilities.DefaultOptions.GetTypeInfo(typeof(object))),
                    Type = "text"
                }]
            },
        };
    }

    private CallToolResult ConvertJsonElementToCallToolResult(JsonElement json)
    {
        if (json.ValueKind == JsonValueKind.Number)
            return new()
            {
                Content = [new TextContentBlock() { Text = json.ToString(), Type = "text" }]
            };
        else if (json.ValueKind == JsonValueKind.String)
            return new()
            {
                Content = [new TextContentBlock() { Text = json.ToString(), Type = "text" }]
            };
        else
        {
            return new()
            {
                Content = [new TextContentBlock()
                {
                    Text = JsonSerializer.Serialize(json, McpJsonUtilities.DefaultOptions.GetTypeInfo(typeof(object))),
                    Type = "text"
                }]
            };
        }
    }

}