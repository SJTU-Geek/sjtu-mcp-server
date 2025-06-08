using ModelContextProtocol.Protocol;
using Python.Runtime;
using SJTUGeek.MCP.Server.Extensions;
using SJTUGeek.MCP.Server.Helpers;
using SJTUGeek.MCP.Server.Models;
using SJTUGeek.MCP.Server.Modules;
using SJTUGeek.MCP.Server.Tools.SjtuJw;
using SJTUGeek.MCP.Server.Tools.SjtuMail;
using SJTUGeek.MCP.Server.Tools.SjtuVenue;
using System.CommandLine;

namespace SJTUGeek.MCP.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var portOption = new Option<int>(
                aliases: new string[] { "--port", "-p" },
                getDefaultValue: () => 5173,
                description: "指定 SSE 服务监听的端口号。"
            );
            var hostOption = new Option<string>(
                aliases: new string[] { "--host", "-h" },
                getDefaultValue: () => "localhost",
                description: "指定 SSE 服务监听的主机名或 IP 地址。"
            );
            var pyDllOption = new Option<string?>(
                aliases: new string[] { "--pydll" },
                getDefaultValue: () => null,
                description: "指定 Python 脚本运行环境的库文件（必须在 PATH 环境变量指定的目录下），例如 python310.dll。若不填写，则禁用 Python 脚本。"
            );
            var jsEngineOption = new Option<string?>(
                aliases: new string[] { "--jsengine" },
                getDefaultValue: () => null,
                description: "指定 JavaScript 脚本运行环境，只能填写“V8”。若不填写，则禁用 JavaScript 脚本。"
            );
            var sseOption = new Option<bool>(
                aliases: new string[] { "--sse" },
                getDefaultValue: () => true,
                description: "指定 MCP 服务器是否使用 SSE 方式进行交互，若 true，则使用 SSE 方式，否则使用 stdio 方式。"
            );
            var cookieOption = new Option<string?>(
                aliases: new string[] { "--cookie", "-C" },
                description: "指定用于 jAccount 认证的 JAAuthCookie 字符串。"
            );
            var toolGroupOption = new Option<List<string>?>(
                aliases: new string[] { "--tools" },
                getDefaultValue: () => new List<string>(),
                description: "指定启用的 MCP 工具组。"
            );
            var bgeRerankModelOption = new Option<string?>(
                aliases: new string[] { "--bge-rerank-model" },
                getDefaultValue: () => null,
                description: "指定 BGE 重排序模型的文件路径。"
            );
            var llmRerankModelOption = new Option<string?>(
                aliases: new string[] { "--llm-rerank-model" },
                getDefaultValue: () => null,
                description: "指定通用重排序模型的文件路径。"
            );

            var rootCommand = new RootCommand("Welcome to SJTUGeek.MCP!");
            rootCommand.AddOption(portOption);
            rootCommand.AddOption(hostOption);
            rootCommand.AddOption(pyDllOption);
            rootCommand.AddOption(jsEngineOption);
            rootCommand.AddOption(sseOption);
            rootCommand.AddOption(cookieOption);
            rootCommand.AddOption(toolGroupOption);
            rootCommand.AddOption(bgeRerankModelOption);
            rootCommand.AddOption(llmRerankModelOption);

            rootCommand.SetHandler((appOptions) =>
            {
                RunApp(appOptions);
            }, new AppCmdOptionBinder(
                portOption,
                hostOption,
                pyDllOption,
                jsEngineOption,
                sseOption,
                cookieOption,
                toolGroupOption,
                bgeRerankModelOption,
                llmRerankModelOption
            ));

            await rootCommand.InvokeAsync(args);
        }

        public static void RunApp(AppCmdOption appOptions)
        {
            //if (appOptions.IsError)
            //{
            //    Console.Error.WriteLine(appOptions.Message);
            //    return;
            //}
            AppCmdOption.Default = appOptions;

            if (appOptions.PythonDll != null)
            {
                Runtime.PythonDLL = appOptions.PythonDll;
                PythonEngine.Initialize();
                PythonEngine.BeginAllowThreads();
            }
            
            var builder = WebApplication.CreateBuilder();

            builder.Logging.AddConsole(consoleLogOptions =>
            {
                // Configure all logs to go to stderr
                consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            var mcpServerBuilder = builder.Services
                .AddMcpServer(McpScriptBuilderExtensions.ConfigureMcpOptions)
                .WithHttpTransport()
                .WithToolsFromCurrentAssembly()
                //.WithTools<AddTool>()
                //.WithTools<TestTool>()
                ;
            if (!appOptions.EnableSse)
                mcpServerBuilder.WithStdioServerTransport();

            //builder.Services.AddMcpScripts();

            builder.Services.AddMemoryCache(); // Singleton
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<JaCookieProvider>();
            builder.Services.AddSingleton<CookieContainerProvider>();
            builder.Services.AddScoped<HttpClientFactory>();
            builder.Services.AddSingleton<MemoryCacheWrapper>();

            builder.Services.AddScoped<SjtuMailService>();
            builder.Services.AddScoped<SjtuJwService>();
            builder.Services.AddScoped<SjtuVenueService>();

            builder.Services.AddSingleton<RerankHelper>();

            builder.WebHost.UseUrls($"http://{appOptions.Host}:{appOptions.Port}");

            //builder.Services.AddControllers();

            builder.Services.AddSingleton<Func<LoggingLevel>>(_ => () => LoggingLevel.Debug);

            var app = builder.Build();

            //app.UseAuthorization();
            //app.MapControllers();
            if (appOptions.EnableSse)
                app.MapMcp();
            app.UseMiddleware<AuthMiddleware>();
            app.Run();
        }
    }
}
