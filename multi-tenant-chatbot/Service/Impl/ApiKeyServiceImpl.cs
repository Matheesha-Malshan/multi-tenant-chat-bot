using System;
using System.Threading.Tasks;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service.Impl;

public class ApiKeyServiceImpl: IApiKeyService
{
    private readonly AppDb _appDb;
    
    public ApiKeyServiceImpl(AppDb appDb)
    {
        _appDb=appDb;
        
    }
    public async Task<string> GenerateApiKey(ApiKeyDto dto)
    {
        var chatBotEntity = await _appDb.ChatBots.FindAsync(dto.ChatBotId);
        if (chatBotEntity == null)
        {
            return "not chat bot found";
        }
        string apiKey = Guid.NewGuid().ToString();
        chatBotEntity.ApiKey = apiKey;
        
        _appDb.ChatBots.Update(chatBotEntity);
        await _appDb.SaveChangesAsync();
        
        return apiKey;

    }
}