using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_tenant_chatBot.Model;

public class AnalysisEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TotalQueries {get; set; }
    public int NumberOfDocuments {get; set; }
    public int TotalChunks {get; set; }
}