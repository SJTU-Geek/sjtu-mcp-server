using LLama;
using LLama.Common;
using LLama.Extensions;
using LLama.Native;

namespace SJTUGeek.MCP.Server.Helpers
{
    public class RerankHelper
    {
        private static string RerankingModelPath => Path.Combine(PathHelper.AppPath, "data", "bge-reranker-large-q8_0.gguf");
        private readonly LLamaReranker _reranker;

        public RerankHelper()
        {
            var @params = new ModelParams(RerankingModelPath)
            {
                ContextSize = 0,
                PoolingType = LLamaPoolingType.Rank,

            };
            using var weights = LLamaWeights.LoadFromFile(@params);
            _reranker = new LLamaReranker(weights, @params);
        }

        public async Task<(float, int)> FindMostRelevant(string input, List<string> documents)
        {
            var scores = await _reranker.GetRelevanceScores(input, documents, normalize: true);

            var max = scores.Select((score, index) => (score, index))
                                 .MaxBy(x => x.score);

            return max;
        }
    }
}
