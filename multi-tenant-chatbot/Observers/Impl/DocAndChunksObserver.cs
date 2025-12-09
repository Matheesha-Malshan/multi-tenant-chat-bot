using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Observers.Impl;

public class DocAndChunksObserver:IAnalysisObserver
{
    private readonly ISubjectObserver _observer;
    private readonly IServiceScopeFactory _scopeFactory;

    public DocAndChunksObserver(ISubjectObserver observer,IServiceScopeFactory serviceScopeFactory)
    {
        _scopeFactory=serviceScopeFactory;
        _observer = observer;
        AddObserver();
      
    }
    public void AddObserver()
    {
        _observer.AddObserver(this);
    }
    public async Task Analyze(AnalysisDto dto)
    {
        using var scope = _scopeFactory.CreateScope();
        var appDb = scope.ServiceProvider.GetRequiredService<AppDb>();
        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

        var analysisEntity = await appDb.Analysis
            .FirstOrDefaultAsync(a => a.ChatBotId == dto.ChatBotId);
        

        if (analysisEntity == null)
        {
            
            AnalysisEntity analysis = mapper.Map<AnalysisDto,AnalysisEntity>(dto);
            await appDb.Analysis.AddAsync(analysis);
            await appDb.SaveChangesAsync();
           
        }
        else
        {
            analysisEntity.NumberOfDocuments= analysisEntity.NumberOfDocuments + 1;
            analysisEntity.TotalChunks=analysisEntity.TotalChunks+dto.TotalChunks;
        
            appDb.Analysis.Update(analysisEntity);
            await appDb.SaveChangesAsync();
        }
        
    }
}