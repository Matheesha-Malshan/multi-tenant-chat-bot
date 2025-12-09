using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels;

public interface IModelSelector
{
    IModels GetModels(LlmModelsDto llmModelsDto);
}