namespace multi_tenant_chatBot.Llm;

public interface IEmbeddingsCreater
{
    Task<float[]?> CreateEmbeddings(string promt);
}