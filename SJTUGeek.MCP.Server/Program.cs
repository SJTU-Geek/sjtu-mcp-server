using ModelContextProtocol.Protocol;
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
    public static class Program
    {
        public static void Main(string[] args)
        {
            ParseArgs(args);
            RunApp(AppCmdOption.Default);
        }

        public static void ParseArgs(string[] args)
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
            var stdioOption = new Option<bool>(
                aliases: new string[] { "--use-stdio" },
                getDefaultValue: () => false,
                description: "指定 MCP 服务器是否使用 SSE 或者 Streamable HTTP 方式进行交互，若 true，则启用 HTTP 服务，否则使用 stdio 方式。"
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
            rootCommand.AddOption(stdioOption);
            rootCommand.AddOption(cookieOption);
            rootCommand.AddOption(toolGroupOption);
            rootCommand.AddOption(bgeRerankModelOption);
            rootCommand.AddOption(llmRerankModelOption);

            rootCommand.SetHandler((appOptions) =>
            {
                AppCmdOption.Default = appOptions;
            }, new AppCmdOptionBinder(
                portOption,
                hostOption,
                stdioOption,
                cookieOption,
                toolGroupOption,
                bgeRerankModelOption,
                llmRerankModelOption
            ));

            rootCommand.Invoke(args);
        }

        public static void RunApp(AppCmdOption appOptions)
        {
            if (appOptions.EnableStdio)
                RunStdioApp(appOptions);
            else
                RunHttpApp(appOptions);
        }

        public static void RunHttpApp(AppCmdOption appOptions)
        {
            var builder = WebApplication.CreateBuilder();

            var mcpServerBuilder = builder.Services
                .AddMcpServer(McpScriptBuilderExtensions.ConfigureMcpOptions)
                .WithHttpTransport()
                .WithAllMyTools()
                ;

            builder.Services.AddHttpContextAccessor();
            AddMcpServices(builder.Services);
            builder.WebHost.UseUrls($"http://{appOptions.Host}:{appOptions.Port}");
            builder.Services.AddSingleton<Func<LoggingLevel>>(_ => () => LoggingLevel.Debug);

            var app = builder.Build();

            app.MapMcp();
            app.UseMiddleware<AuthMiddleware>();
            app.Run();
        }

        public static void RunStdioApp(AppCmdOption appOptions)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Logging.AddConsole(consoleLogOptions =>
            {
                // Configure all logs to go to stderr
                consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            var mcpServerBuilder = builder.Services
                .AddMcpServer(McpScriptBuilderExtensions.ConfigureMcpOptions)
                .WithStdioServerTransport()
                .WithAllMyTools()
                ;

            AddMcpServices(builder.Services);
            builder.Services.AddSingleton<Func<LoggingLevel>>(_ => () => LoggingLevel.Debug);

            var app = builder.Build();
            app.Run();
        }

        public static void AddMcpServices(IServiceCollection services)
        {
            services.AddMemoryCache(); // Singleton

            services.AddScoped<JaCookieProvider>();
            services.AddSingleton<CookieContainerProvider>();
            services.AddScoped<HttpClientFactory>();
            services.AddSingleton<MemoryCacheWrapper>();

            services.AddScoped<SjtuMailService>();
            services.AddScoped<SjtuJwService>();
            services.AddScoped<SjtuVenueService>();

            services.AddSingleton<RerankHelper>();
        }
    }
}
