namespace multi_tenant_chatBot.Dto;

public class ChatHistoryResponseDto
{
    public string? UserPromt{get;set;}
    public string? SystemMessage { get; set; }

    public ChatHistoryResponseDto(string? userPromt, string? systemMessage)
    {
        UserPromt = userPromt;
        SystemMessage = systemMessage;
    }
}