using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_tenant_chatBot.Model;

public class LlmConfigurationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(ChatBotEntity))]
    public int ChatBotId { get; set; }
    
    [MaxLength(255)]
    public string? ModelName { get; set; }
    public float Temperature { get; set; }
    public int MaxToken { get; set; }
    public float TopP { get; set; }
    
    public ChatBotEntity? ChatBotEntity { get; set; }
}