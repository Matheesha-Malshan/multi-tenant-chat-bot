using Microsoft.EntityFrameworkCore;
using multi_tenant_chatBot.Configurations;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Service;
using multi_tenant_chatBot.Service.OrganizationServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IOrganizationService,OrganizationServiceImpl>();


builder.Services.AddAutoMapper(typeof(MappingProfile));


var connectionString = "server=localhost;user=root;password=1234;database=ragPipeline";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));


builder.Services.AddDbContext<AppDb>(options => 
    options.UseMySql(connectionString, serverVersion));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDb>();
    db.Database.EnsureCreated();
}




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
