using multi_tenant_chatBot.Dto;
using Qdrant.Grpc;

namespace multi_tenant_chatBot.Embeddings.Impl;

public class SavingEmbeddings:ISavingEmbeddings
{
    

    public async Task CreateEmbeddingsOnDb(DocumentDto document, float[]? embeddings)
    {
        var channel = QdrantChannel.ForAddress("http://localhost:6334");
        var client = new QdrantGrpcClient(channel);
        
        var upsert = new UpsertPoints()
        {
            CollectionName = "rag-pipeline",
            Points =
            {
                new PointStruct
                {
                    Id = new PointId { Uuid = Guid.NewGuid().ToString() }, 
                    Vectors = new Vectors
                    {
                        Vector = new Vector
                        {
                            Data = { embeddings } 
                        }
                    },
                    Payload =
                    {
                        ["chatbotId"] = document.ChatBotId,
                      
                    }
                }
            }
        };

        await client.Points.UpsertAsync(upsert);

    }
}