using Microsoft.EntityFrameworkCore;
using multi_tenant_chatBot.Configurations;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.DocRead;
using multi_tenant_chatBot.DocRead.Impl;
using multi_tenant_chatBot.Embeddings;
using multi_tenant_chatBot.Embeddings.Impl;
using multi_tenant_chatBot.File;
using multi_tenant_chatBot.File.Impl;
using multi_tenant_chatBot.Llm;
using multi_tenant_chatBot.Observers;
using multi_tenant_chatBot.Observers.Impl;
using multi_tenant_chatBot.States;
using multi_tenant_chatBot.Service;
using multi_tenant_chatBot.Service.Impl;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog(); // <-- Add this line
    
    builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddScoped<IOrganizationService,OrganizationServiceImpl>();
    builder.Services.AddScoped<IChatBotService,ChatBotServiceImpl>();
    builder.Services.AddScoped<ILlmConfigService,LlmConfigServiceImpl>();
    builder.Services.AddScoped<IDocumentService,DocumentServiceImpl>();
    builder.Services.AddScoped<IFileService,FileServiceImpl>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddScoped<IReaderSelector, ReaderSelectorImpl>();
    builder.Services.AddScoped<IReaders, PdfReaderImpl>();
    builder.Services.AddScoped<IReaders,TextReaderImpl>();
    builder.Services.AddScoped<IChunckCreater,ChunkCreater>();
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IEmbeddingsCreater, EmbeddingsCreater>();
    builder.Services.AddScoped<ISavingEmbeddings, SavingEmbeddings>();
    builder.Services.AddSingleton<ISubjectObserver, SubjectObserverImpl>();
    builder.Services.AddSingleton<DocAndChunksObserver>();
    
    var connectionString = "server=localhost;user=root;password=1234;database=ragPipeline";
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));


    builder.Services.AddDbContext<AppDb>(options => 
        options.UseMySql(connectionString, serverVersion));
    
    var app = builder.Build();
    
    
    app.UseWebSockets();
    app.Map("/ws/chat", WebSocketHandler.Handle);
    
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
    
    var observer = app.Services.GetRequiredService<DocAndChunksObserver>();
    
    
    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}


















