using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.LlmModels;

public interface IModels
{
    bool CheckModelType(LlmModelsDto llmModelsDto);
    Task<string> CreateChat(string userQuery,LlmConfigurationsDto configDto,string words);
}