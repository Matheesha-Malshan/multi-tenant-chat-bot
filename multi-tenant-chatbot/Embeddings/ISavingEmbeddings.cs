using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Embeddings;

public interface ISavingEmbeddings
{
    Task CreateEmbeddingsOnDb(DocumentDto document, float[]? embeddings,string sentense);
}