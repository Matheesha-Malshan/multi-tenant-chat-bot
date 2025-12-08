using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels.impl;

public class KimiK2:IModels
{
    public bool CheckModelType(LlmModelsDto llmModelsDto)
    {
        return llmModelsDto == LlmModelsDto.KimiK2;
    }

    public Task<string> CreateChat(string userQuery,LlmConfigurationsDto configDto)
    {
        throw new NotImplementedException();
    }
}