using AutoMapper;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Embeddings;
using multi_tenant_chatBot.File;
using multi_tenant_chatBot.Llm;
using multi_tenant_chatBot.Model;
using multi_tenant_chatBot.Observers;

namespace multi_tenant_chatBot.Service.Impl;

public class DocumentServiceImpl:IDocumentService
{
    private readonly IMapper _mapper;
    private readonly AppDb _appDb;
    private readonly IFileService _fileService;
    private readonly ILogger<DocumentServiceImpl> _logger;
    private readonly IChunckCreater _chunkCreater;
    private readonly IEmbeddingsCreater _embeddingsCreater;
    private readonly ISavingEmbeddings _savingEmbeddings;
    private readonly ISubjectObserver _subjectObserver;
    public DocumentServiceImpl(IMapper mapper, AppDb appDb,IFileService fileService, 
        ILogger<DocumentServiceImpl> logger,IChunckCreater  chunkCreater,
        IEmbeddingsCreater embeddingsCreater,ISavingEmbeddings savingEmbeddings,
        ISubjectObserver  subjectObserver)
    {
        _mapper = mapper;
        _appDb = appDb;
        _fileService = fileService;
        _logger = logger;
        _chunkCreater = chunkCreater;
        _embeddingsCreater = embeddingsCreater;
        _savingEmbeddings = savingEmbeddings;
        _subjectObserver = subjectObserver;
    }

    public async Task CreateDocument(DocumentDto documentDto)
    {
        var document = _mapper.Map<DocumentDto,DocumentEntity>(documentDto);
       
        using var transaction = await _appDb.Database.BeginTransactionAsync();
        
        DocumentResponseDto responseDto=new DocumentResponseDto();
        responseDto.Message = "document is uploaded";
        responseDto.Status = "Processing";
        await States.WebSocketManager.SendMessageToAll(responseDto);
        
        try
        {
            
            await _appDb.Document.AddAsync(document);
            await _appDb.SaveChangesAsync();
            documentDto.Id = document.Id;
            documentDto.FileUrl=_fileService.CreatePath(documentDto);
            document.FileUrl=documentDto.FileUrl;
            await _fileService.SaveFile(documentDto);
            
            _appDb.Document.Update(document);
            await _appDb.SaveChangesAsync();
            
            string[] chunkList=await _chunkCreater.ChunkCreate(documentDto);

            foreach (var sentense in chunkList)
            {
                float[]? a = await _embeddingsCreater.CreateEmbeddings(sentense);
                await _savingEmbeddings.CreateEmbeddingsOnDb(documentDto,a);
            }
            responseDto.Message = "document is Proceed successfully";
            responseDto.Status = "Proceed";
            responseDto.Chunks=chunkList.Count();
            
            await States.WebSocketManager.SendMessageToAll(responseDto);
            
            document.Status="Proceed";
            document.Chunks=chunkList.Count();

            _appDb.Document.Update(document);
            await _appDb.SaveChangesAsync();
            
            _subjectObserver.AddChange(document);
            
            await transaction.CommitAsync();

            _logger.LogInformation($"Document {document.Id} created successfully");

        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, $"error creating document {document.Id}");
            throw;
        }

    }
    
    

   
}