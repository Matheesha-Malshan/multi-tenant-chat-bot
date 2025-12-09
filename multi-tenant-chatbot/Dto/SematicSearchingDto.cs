namespace multi_tenant_chatBot.Dto;

public class SematicSearchingDto
{
    public int ChatBotId{get;set;}
    public string Query { get; set; } = "";
    public string ApiKey { get; set; } = "";
}