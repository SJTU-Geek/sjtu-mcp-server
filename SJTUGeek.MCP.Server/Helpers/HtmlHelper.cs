using ReverseMarkdown;

namespace SJTUGeek.MCP.Server.Helpers
{
    public static class HtmlHelper
    {
        public static string HtmlToPlainText(string html)
        {
            var config = new ReverseMarkdown.Config
            {
                // Include the unknown tag completely in the result (default as well)
                UnknownTags = Config.UnknownTagsOption.Bypass,
                // generate GitHub flavoured markdown, supported for BR, PRE and table tags
                GithubFlavored = false,
                // will ignore all comments
                RemoveComments = true,
                // remove markdown output for links where appropriate
                SmartHrefHandling = true,
                CleanupUnnecessarySpaces = true,
            };
            var converter = new ReverseMarkdown.Converter(config);
            return converter.Convert(html);
        }
    }
}
