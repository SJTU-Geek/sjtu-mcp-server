using ModelContextProtocol.Protocol.Types;
using Python.Runtime;
using SJTUGeek.MCP.Server.Extensions;
using SJTUGeek.MCP.Server.Models;
using SJTUGeek.MCP.Server.Modules;
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
                description: "ָ�� SSE ��������Ķ˿ںš�"
            );
            var hostOption = new Option<string>(
                aliases: new string[] { "--host", "-h" },
                getDefaultValue: () => "localhost",
                description: "ָ�� SSE ����������������� IP ��ַ��"
            );
            var pyDllOption = new Option<string?>(
                aliases: new string[] { "--pydll" },
                getDefaultValue: () => null,
                description: "ָ�� Python �ű����л����Ŀ��ļ��������� PATH ��������ָ����Ŀ¼�£������� python310.dll��������д������� Python �ű���"
            );
            var jsEngineOption = new Option<string?>(
                aliases: new string[] { "--jsengine" },
                getDefaultValue: () => null,
                description: "ָ�� JavaScript �ű����л�����ֻ����д��V8����������д������� JavaScript �ű���"
            );
            var sseOption = new Option<bool>(
                aliases: new string[] { "--sse" },
                getDefaultValue: () => true,
                description: "ָ�� MCP �������Ƿ�ʹ�� SSE ��ʽ���н������� true����ʹ�� SSE ��ʽ������ʹ�� stdio ��ʽ��"
            );
            var cookieOption = new Option<string?>(
                aliases: new string[] { "--cookie", "-C" },
                description: "ָ������ jAccount ��֤�� JAAuthCookie �ַ�����"
            );
            var toolGroupOption = new Option<List<string>>(
                aliases: new string[] { "--tools" },
                getDefaultValue: () => new List<string>() { "default" },
                description: "ָ�����õ� MCP �����顣"
            );

            var rootCommand = new RootCommand("Welcome to SJTUGeek.MCP!");
            rootCommand.AddOption(portOption);
            rootCommand.AddOption(hostOption);
            rootCommand.AddOption(pyDllOption);
            rootCommand.AddOption(jsEngineOption);
            rootCommand.AddOption(sseOption);
            rootCommand.AddOption(cookieOption);
            rootCommand.AddOption(toolGroupOption);

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
                toolGroupOption
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

            Runtime.PythonDLL = appOptions.PythonDll;
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();

            var builder = WebApplication.CreateBuilder();

            builder.Logging.AddConsole(consoleLogOptions =>
            {
                // Configure all logs to go to stderr
                consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            var mcpServerBuilder = builder.Services
                .AddMcpServer()
                //.WithTools<AddTool>()
                //.WithTools<TestTool>()
                ;
            if (!appOptions.EnableSse)
                mcpServerBuilder.WithStdioServerTransport();

            builder.Services.AddMcpScripts();

            builder.Services.AddMemoryCache(); // Singleton
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<JaCookieProvider>();

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
