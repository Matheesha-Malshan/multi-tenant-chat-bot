using AutoMapper;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service.Impl;

public class ChatBotServiceImpl: IChatBotService
{
    private readonly IMapper _mapper;
    private readonly AppDb _appDb;
    private readonly ILlmConfigService _configService;
    private readonly ILogger<ChatBotServiceImpl> _logger;

    public ChatBotServiceImpl(IMapper mapper, AppDb appDb, ILlmConfigService configService, 
        ILogger<ChatBotServiceImpl> logger)
    {
        _mapper = mapper;
        _appDb = appDb;
        _configService = configService;
        _logger = logger;
    }

    public async Task<ChatBotEntity> CreateChatBot(ChatBotDto chatBotDto)
    {
        var chatBotEntity = _mapper.Map<ChatBotDto,ChatBotEntity>(chatBotDto);
        
        using var transaction = await _appDb.Database.BeginTransactionAsync();

        try
        {
            await _appDb.AddAsync(chatBotEntity);
            await _appDb.SaveChangesAsync();

            var result=await _configService.
                DefaultConfig(new LlmConfigurationsDto(
                    chatBotEntity.Id,
                    LlmModelsDto.KimiK2,
                    1,
                    45,
                    1
                    ));
            
            await transaction.CommitAsync();
            _logger.LogInformation($"Created chatbot with id {chatBotEntity.Id}");

        }
        catch (Exception e)
        {
            _logger.LogError(e,"Error creating chatbot entity");
            throw;
        }
        
        return chatBotEntity;
    }
    
    
    
}