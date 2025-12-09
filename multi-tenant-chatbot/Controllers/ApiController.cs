using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController
{
    private readonly IApiKeyService _apiKeyService;
    
    public ApiController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
        
    }
        
    [HttpGet("create-apikey")]
    public async Task<string> GenerateApiKey([FromBody]ApiKeyDto dto)
    {
        return await _apiKeyService.GenerateApiKey(dto);
    }
}