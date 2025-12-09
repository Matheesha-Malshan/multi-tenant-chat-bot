using AutoMapper;
using Microsoft.EntityFrameworkCore;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service.Impl;

public class LlmConfigServiceImpl: ILlmConfigService
{
    private readonly IMapper _mapper;
    private readonly AppDb _appDb;

    public LlmConfigServiceImpl(IMapper mapper, AppDb appDb)
    {
        _mapper = mapper;
        _appDb = appDb;
    }

    public async Task<LlmConfigurationEntity> CreateConfig(LlmConfigurationsDto configDto)
    {

        var config = await _appDb.LlmConfig
            .FirstOrDefaultAsync(c => c.ChatBotId == configDto.ChatBotId);

        if (config == null)
        {
            throw new Exception("Config not found");
        }

        var configurationEntity = _mapper.Map(configDto, config);
        
        _appDb.LlmConfig.Update(configurationEntity);
        _appDb.SaveChanges();
        return _mapper.Map<LlmConfigurationEntity>(config);
    }
    
    public async Task<LlmConfigurationEntity> DefaultConfig(LlmConfigurationsDto configDto)
    {
        var configEntity = _mapper.Map<LlmConfigurationsDto, LlmConfigurationEntity>(configDto);
        await _appDb.AddAsync(configEntity);
        await _appDb.SaveChangesAsync();
        
        return configEntity;
        
    } 
}