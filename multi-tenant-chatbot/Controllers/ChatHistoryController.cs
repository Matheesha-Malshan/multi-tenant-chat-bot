using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatHistoryController
{
    private readonly IChatHistoryService _chatHistoryService;
    
    public ChatHistoryController(IChatHistoryService chatHistoryService)
    {
        _chatHistoryService=chatHistoryService;
        
    }
    
    [HttpGet("{chatBotId}")]
    public async Task<List<ChatHistoryResponseDto>> GetChatHistory(int chatBotId)
    {
        return await _chatHistoryService.RetriveAllChatHistory(chatBotId);
    }
}