using Qdrant.Grpc;

namespace multi_tenant_chatBot.Embeddings;

public interface IRetriveEmbeddings
{

    Task<string> RetriveEmbeddingsAsWords(float[] queary, int chatbotId);
}