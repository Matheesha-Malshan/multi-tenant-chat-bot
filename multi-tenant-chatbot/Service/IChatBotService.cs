using System.Threading.Tasks;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service;

public interface IChatBotService
{
    Task<ChatBotEntity> CreateChatBot(ChatBotDto chatBotDto);
}