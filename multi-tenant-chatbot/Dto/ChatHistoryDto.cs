using MongoDB.Bson;

namespace multi_tenant_chatBot.Dto;

public class ChatHistoryDto
{
    public ObjectId Id { get; set; }
    public int ChatBotId { get; set; }
    public string? UserQuery { get; set; }
    public string? SystemQuery { get; set; }
    
}