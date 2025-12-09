using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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
    private readonly IMongoClient _mongoClient;
    
    public SemanticSearchServiceImpl(IEmbeddingsCreater embeddingsCreater,
        IRetriveEmbeddings retriveEmbeddings,AppDb appDb,IMapper mapper,IModelSelector modelSelector,
        IMongoClient mongoClient)
    {
      _embeddingsCreater = embeddingsCreater;
      _retriveEmbeddings = retriveEmbeddings;
      _appDb = appDb;
      _mapper = mapper;
      _modelSelector = modelSelector;
      _mongoClient = mongoClient;
    }
    
    public async Task<string> SearchEmbeddings(SematicSearchingDto semanticSearchingDto)
    {
        var chatBotWithApiKey=await _appDb.ChatBots.FindAsync(semanticSearchingDto.ChatBotId);
        if (chatBotWithApiKey == null)
        {
            return "No api key found";
        }

        if (chatBotWithApiKey.ApiKey != semanticSearchingDto.ApiKey)
        {
            return "Invalid api key";
        }
        
        float[]? embeddings = await _embeddingsCreater.CreateEmbeddings(semanticSearchingDto.Query);

        if (embeddings==null)
        {
            return "No embedding found";
        }
        var configurationEntity = await _appDb.LlmConfig
            .FirstOrDefaultAsync(a => a.ChatBotId == semanticSearchingDto.ChatBotId);

        var congifDto=_mapper.Map<LlmConfigurationsDto>(configurationEntity);
        
        string words = await _retriveEmbeddings.RetriveEmbeddingsAsWords(embeddings, 
             semanticSearchingDto.ChatBotId);
         
        var model=_modelSelector.GetModels(congifDto.ModelName);
        
        string systemMessage=await model.CreateChat(semanticSearchingDto.Query, congifDto, words);
        
        await CreateChatHistory(semanticSearchingDto.ChatBotId,semanticSearchingDto.Query,systemMessage);
        
        return systemMessage; //await model.CreateChat(semanticSearchingDto.Query, congifDto, words);

    }

    public async Task CreateChatHistory(int chatBotId,string userQuery,string systemQuery)
    {   
        var mongoDatabase = _mongoClient.GetDatabase("Rag");
        var ragColl = mongoDatabase.GetCollection<ChatHistoryDto>("chatHistory");
        
        ChatHistoryDto chatHistoryDto=new ChatHistoryDto();
        chatHistoryDto.ChatBotId=chatBotId;
        chatHistoryDto.UserQuery=userQuery;
        chatHistoryDto.SystemQuery=systemQuery;
        await ragColl.InsertOneAsync(chatHistoryDto);
    }
    
}