using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using SJTUGeek.MCP.Server.Helpers;
using SJTUGeek.MCP.Server.Tools.SjtuJw;
using SJTUGeek.MCP.Server.Tools.SjtuMail;
using System.ComponentModel;

namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    [McpServerToolType]
    public class SjtuVenueTool
    {
        private readonly ILogger<SjtuVenueTool> _logger;
        private readonly SjtuVenueService _venue;
        private readonly RerankHelper _rerank;

        public SjtuVenueTool(ILogger<SjtuVenueTool> logger, SjtuVenueService venue, RerankHelper rerank)
        {
            _logger = logger;
            _venue = venue;
            _rerank = rerank;
        }

        [McpServerTool(Name = "book_venue"), Description("Book a sports venue")]
        public async Task<object> ToolBookVenue(
            string venue_name
        )
        {
            await _venue.Login();
            var vs = await _venue.GetVenues();

            var target = await _rerank.FindMostRelevant(venue_name, vs.Select(v => v.VenueName).ToList());

            return $"{venue_name} 匹配 {vs[target.Item2].VenueName}（可信度：{target.Item1}）";
        }
    }
}
