using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatBotController:ControllerBase
{
    private readonly IChatBotService _chatBotService;

    public ChatBotController(IChatBotService chatBotService)
    {
        _chatBotService = chatBotService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateChatBot([FromBody]ChatBotDto chatBotDto)
    {
        await _chatBotService.CreateChatBot(chatBotDto);
        return Ok();
    }
}