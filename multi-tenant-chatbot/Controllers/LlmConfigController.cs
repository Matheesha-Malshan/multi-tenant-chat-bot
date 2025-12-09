using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class LlmConfigController:ControllerBase
{
    private readonly ILlmConfigService _configService;

    public LlmConfigController(ILlmConfigService configService)
    {
        _configService = configService;
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> CreateConfig([FromBody] LlmConfigurationsDto configDto)
    {
        await _configService.CreateConfig(configDto);
        return Ok();
    }
}