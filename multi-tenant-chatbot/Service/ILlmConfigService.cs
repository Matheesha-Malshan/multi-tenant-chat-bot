using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service;

public interface ILlmConfigService
{
     Task<LlmConfigurationEntity> CreateConfig(LlmConfigurationsDto configDto);
     Task<LlmConfigurationEntity> DefaultConfig(LlmConfigurationsDto configDto);
}