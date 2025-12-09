namespace multi_tenant_chatBot.Dto;

public class ChatBotDto
{
    public int OrganizationId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SystemPromt { get; set; }
    
}