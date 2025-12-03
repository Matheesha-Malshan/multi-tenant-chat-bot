using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multi_tenant_chatBot.Model;

public class OrganizationEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    [MaxLength(255)]
    public string? PhoneNumber { get; set; }
    [MaxLength(255)]
    public string? Email { get; set; }
}