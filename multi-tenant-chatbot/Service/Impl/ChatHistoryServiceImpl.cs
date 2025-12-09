using MongoDB.Driver;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service.Impl;

public class ChatHistoryServiceImpl: IChatHistoryService
{
    private readonly IMongoClient  _mongoClient;
    
    public ChatHistoryServiceImpl(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
        
    }
    public async Task<List<ChatHistoryResponseDto>> RetriveAllChatHistory(int chatBotId)
    {
        List<ChatHistoryResponseDto> values = new List<ChatHistoryResponseDto>();

        
        var database = _mongoClient.GetDatabase("Rag");
        var collection=database.GetCollection<ChatHistoryDto>("chatHistory");

        var cursor=await collection.FindAsync(x => x.ChatBotId == chatBotId);
        var list=await cursor.ToListAsync();

        foreach (var chatHistoryDto in list)
        {
            values.Add(new ChatHistoryResponseDto(
                chatHistoryDto.UserQuery,
                chatHistoryDto.SystemQuery
                
                ));
        }
        
        return values;
    }
}