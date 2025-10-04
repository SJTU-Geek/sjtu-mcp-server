using System.CommandLine.Binding;
using System.CommandLine;
using System;
using System.Net;

namespace SJTUGeek.MCP.Server.Models
{
    public class AppCmdOption
    {
        public static AppCmdOption Default { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableStdio { get; set; }
        public string? JaAuthCookie { get; set; }
        public List<string>? EnabledToolGroups { get; set; }
        public string? BgeRerankModel { get; set; }
        public string? LlmRerankModel { get; set; }
    }

    public class AppCmdOptionBinder : BinderBase<AppCmdOption>
    {
        private readonly Option<int> _portOption;
        private readonly Option<string> _hostOption;
        private readonly Option<bool> _stdioOption;
        private readonly Option<string?> _cookieOption;
        private readonly Option<List<string>?> _toolGroupOption;
        private readonly Option<string?> _bgeRerankModelOption;
        private readonly Option<string?> _llmRerankModelOption;

        public AppCmdOptionBinder(Option<int> portOption, Option<string> hostOption,  Option<bool> stdioOption, Option<string?> cookieOption, Option<List<string>?> toolGroupOption, Option<string?> bgeRerankModelOption, Option<string?> llmRerankModelOption)
        {
            _portOption = portOption;
            _hostOption = hostOption;
            _stdioOption = stdioOption;
            _cookieOption = cookieOption;
            _toolGroupOption = toolGroupOption;
            _bgeRerankModelOption = bgeRerankModelOption;
            _llmRerankModelOption = llmRerankModelOption;
        }

        protected override AppCmdOption GetBoundValue(BindingContext bindingContext)
        {
            return ValidateValues(bindingContext);
        }

        private AppCmdOption ValidateValues(BindingContext bindingContext)
        {
            var opt = new AppCmdOption
            {
                Port = bindingContext.ParseResult.GetValueForOption(_portOption),
                Host = bindingContext.ParseResult.GetValueForOption(_hostOption),
                EnableStdio = bindingContext.ParseResult.GetValueForOption(_stdioOption),
                JaAuthCookie = bindingContext.ParseResult.GetValueForOption(_cookieOption),
                EnabledToolGroups = bindingContext.ParseResult.GetValueForOption(_toolGroupOption),
                BgeRerankModel = bindingContext.ParseResult.GetValueForOption(_bgeRerankModelOption),
                LlmRerankModel = bindingContext.ParseResult.GetValueForOption(_llmRerankModelOption)
            };

            return opt;
        }
    }
}
