using Microsoft.EntityFrameworkCore;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Data;

public class AppDb:DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
    }
    public DbSet<OrganizationEntity> Organizations { get; set; }
}