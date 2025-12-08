using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels.impl;

public class Llama4Scout:IModels
{
    public bool CheckModelType(LlmModelsDto llmModelsDto)
    {
        return llmModelsDto == LlmModelsDto.Llama4Scout;
    }

    public Task<string> CreateChat(string userQuery,LlmConfigurationsDto configDto)
    {
        throw new NotImplementedException();
    }
}