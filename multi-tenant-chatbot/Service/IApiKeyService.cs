using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service;

public interface IApiKeyService
{
    Task<string> GenerateApiKey(ApiKeyDto dto);
}