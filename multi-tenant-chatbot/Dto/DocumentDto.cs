namespace multi_tenant_chatBot.Dto;

public class DocumentDto
{
    public int Id { get; set; }
    public int ChatBotId { get; set; }
    public string? Name { get; set; }
    public string? Size { get; set; }
    public DateOnly UploadDate { get; set; }
    public string? Status { get; set; }
    public required IFormFile File { get; set; }
    public string? FileUrl { get; set; }
    
}