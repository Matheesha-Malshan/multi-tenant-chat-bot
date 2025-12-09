using multi_tenant_chatBot.Dto;

namespace multi_tenant_chatBot.Service;

public interface IChatHistoryService
{
    Task<List<ChatHistoryResponseDto>> RetriveAllChatHistory(int chatBotId);
}