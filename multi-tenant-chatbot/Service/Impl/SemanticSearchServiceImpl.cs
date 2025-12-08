using AutoMapper;
using Microsoft.EntityFrameworkCore;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Embeddings;
using multi_tenant_chatBot.Llm;
using multi_tenant_chatBot.LlmModels;

namespace multi_tenant_chatBot.Service.Impl;

public class SemanticSearchServiceImpl:ISemanticSearchService
{
    private readonly IEmbeddingsCreater _embeddingsCreater;
    private readonly IRetriveEmbeddings _retriveEmbeddings;
    private readonly AppDb _appDb;
    private readonly IMapper _mapper;
    private readonly IModelSelector _modelSelector;
        
    public SemanticSearchServiceImpl(IEmbeddingsCreater embeddingsCreater,
        IRetriveEmbeddings retriveEmbeddings,AppDb appDb,IMapper mapper,IModelSelector modelSelector)
    {
      _embeddingsCreater = embeddingsCreater;
      _retriveEmbeddings = retriveEmbeddings;
      _appDb = appDb;
      _mapper = mapper;
      _modelSelector = modelSelector;
    }
    
    public async Task SearchEmbeddings(SematicSearchingDto semanticSearchingDto)
    {
        
        float[]? embeddings = await _embeddingsCreater.CreateEmbeddings(semanticSearchingDto.Query);

        if (embeddings==null)
        {
            return;
        }
        var configurationEntity = await _appDb.LlmConfig
            .FirstOrDefaultAsync(a => a.ChatBotId == semanticSearchingDto.ChatBotId);

        LlmConfigurationsDto congifDto=_mapper.Map<LlmConfigurationsDto>(configurationEntity);
            
        string words = await _retriveEmbeddings.RetriveEmbeddingsAsWords(embeddings, 
            semanticSearchingDto.ChatBotId);


        var model=_modelSelector.GetModels(congifDto.ModelName);
        await model.CreateChat(semanticSearchingDto.Query,congifDto);

    }
    
}