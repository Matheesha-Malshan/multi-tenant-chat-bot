using System.Text.Json.Serialization;

namespace multi_tenant_chatBot.Dto;

public class OllamaEmbeddings
{
    [JsonPropertyName("model")]
    public string Model{get;set;}
    
    [JsonPropertyName("embedding")]
    public float[] Embedding { get; set; }
}
