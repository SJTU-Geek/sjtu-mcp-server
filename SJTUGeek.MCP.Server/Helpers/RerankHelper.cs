using LLama;
using LLama.Common;
using LLama.Extensions;
using LLama.Native;
using SJTUGeek.MCP.Server.Models;

namespace SJTUGeek.MCP.Server.Helpers
{
    public class RerankHelper
    {
        private readonly LLamaReranker? _reranker;

        public RerankHelper()
        {
            if (AppCmdOption.Default.BgeRerankModel != null)
            {
                var path = Path.GetFullPath(AppCmdOption.Default.BgeRerankModel);
                var @params = new ModelParams(path)
                {
                    ContextSize = 0,
                    PoolingType = LLamaPoolingType.Rank,

                };
                using var weights = LLamaWeights.LoadFromFile(@params);
                _reranker = new LLamaReranker(weights, @params);
            }
        }

        public async Task<(float, int)> FindMostRelevant(string input, List<string> documents)
        {
            if (_reranker != null)
            {
                var scores = await _reranker.GetRelevanceScores(input, documents, normalize: true);

                var max = scores.Select((score, index) => (score, index))
                                     .MaxBy(x => x.score);

                return max;
            }
            throw new Exception("no rerank model found");
        }
    }
}
