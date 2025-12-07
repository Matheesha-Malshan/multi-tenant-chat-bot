using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_tenant_chatBot.Model;

public class AnalysisEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Chat))]
    public int ChatBotId { get; set; }
    
    public int TotalQueries {get; set; }
    public int NumberOfDocuments {get; set; }
    public int TotalChunks {get; set; }
    
    public ChatBotEntity?  Chat{get; set; }

    public AnalysisEntity(int chatId, int totalQueries, int totalChunks, int totalDocuments)
    {
        ChatBotId = chatId;
        TotalQueries = totalQueries;
        TotalChunks = totalChunks;
        NumberOfDocuments = totalDocuments;
    }
}