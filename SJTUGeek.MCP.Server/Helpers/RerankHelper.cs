using CommunityToolkit.HighPerformance.Buffers;
using LLama;
using LLama.Batched;
using LLama.Common;
using LLama.Native;
using SJTUGeek.MCP.Server.Models;
using System.Numerics.Tensors;

namespace SJTUGeek.MCP.Server.Helpers
{
    public class RerankHelper
    {
        private readonly LLamaReranker? _reranker;
        private readonly BatchedExecutor? _llm;

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
                var weights = LLamaWeights.LoadFromFile(@params);
                _reranker = new LLamaReranker(weights, @params);
            }
            if (AppCmdOption.Default.LlmRerankModel != null)
            {
                var path = Path.GetFullPath(AppCmdOption.Default.LlmRerankModel);
                var @params = new ModelParams(path)
                {
                    ContextSize = 1024,
                    UBatchSize = 10
                };
                var weights = LLamaWeights.LoadFromFile(@params);
                _llm = new BatchedExecutor(weights, @params);
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
            else if (_llm != null)
            {
                var prefix = "<|im_start|>system\nJudge whether the Document meets the requirements based on the Query and the Instruct provided. Note that the answer can only be \"yes\" or \"no\".<|im_end|>\n<|im_start|>user\n";
                var suffix = "<|im_end|>\n<|im_start|>assistant\n<think>\n\n</think>\n\n";
                var prefixTokens = _llm.Context.Tokenize(prefix, false, true);
                var suffixTokens = _llm.Context.Tokenize(suffix, false, true);
                var conversations = new List<Conversation>();

                foreach (var doc in documents)
                {
                    var task = "Given a web search query, determine whether two names refer to the same location.";
                    var instruction = $"<Instruct>: {task}\n<Name1>: {input}\n<Name2>: {doc}";

                    var conversation = _llm.Create();
                    conversations.Add(conversation);
                    var textTokens = _llm.Context.Tokenize(instruction, false, true);
                    conversation.Prompt(prefixTokens.Concat(textTokens).Concat(suffixTokens).ToList());
                }

                var scores = new List<float>();

                void postCalcScore(Conversation conversation)
                {
                    var logitsArr = LLamaTokenDataArray.Create(conversation.Sample());
                    var tokenYes = (int)_llm.Context.Tokenize("yes")[0];
                    var tokenNo = (int)_llm.Context.Tokenize("no")[0];
                    var logitYes = logitsArr.Data.Span[tokenYes];
                    var logitNo = logitsArr.Data.Span[tokenNo];
                    float probYes = 0;
                    float probNo = 0;
                    using (SpanOwner<float> buffer = SpanOwner<float>.Allocate(2))
                    {
                        buffer.Span[0] = logitYes.Logit;
                        buffer.Span[1] = logitNo.Logit;
                        TensorPrimitives.SoftMax(buffer.Span, buffer.Span);
                        probYes = buffer.Span[0];
                        probNo = buffer.Span[1];
                    }
                    scores.Add(probYes);
                }

                while (conversations.Any(x => !x.IsDisposed))
                {
                    var decodeResult = await _llm.Infer();
                    conversations.Where(x => !x.RequiresInference && !x.IsDisposed).ToList().ForEach(x => {
                        postCalcScore(x);
                        x.Dispose();
                    });
                }

                var max = scores.Select((score, index) => (score, index))
                                     .MaxBy(x => x.score);

                return max;
            }
            else
            {
                double[][] inputs = TFIDF.Transform(new List<string>() { input }.Concat(documents).ToArray(), 0);
                inputs = TFIDF.Normalize(inputs);
                var inputVector = inputs[0];
                var documentVectors = inputs.Skip(1).ToArray();

                // 计算余弦相似度（归一化后等价于点积）
                double[] scores = new double[documentVectors.Length];
                for (int i = 0; i < documentVectors.Length; i++)
                {
                    double dotProduct = 0;
                    // 遍历向量的每个维度计算点积
                    for (int j = 0; j < inputVector.Length; j++)
                    {
                        dotProduct += inputVector[j] * documentVectors[i][j];
                    }
                    scores[i] = dotProduct == double.NaN ? 0 : dotProduct; // 存储相似度
                }

                var max = scores.Select((score, index) => ((float)score, index))
                                     .MaxBy(x => x.Item1);

                return max;
            }
        }
    }
}
