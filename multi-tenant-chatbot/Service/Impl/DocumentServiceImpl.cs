using AutoMapper;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.File;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service.Impl;

public class DocumentServiceImpl:IDocumentService
{
    private readonly IMapper _mapper;
    private readonly AppDb _appDb;
    private readonly IFileService _fileService;
    private readonly ILogger<DocumentServiceImpl> _logger;
    
    public DocumentServiceImpl(IMapper mapper, AppDb appDb,IFileService fileService, ILogger<DocumentServiceImpl> logger)
    {
        _mapper = mapper;
        _appDb = appDb;
        _fileService = fileService;
        _logger = logger;
    }

    public async Task CreateDocument(DocumentDto documentDto)
    {
        var document = _mapper.Map<DocumentDto,DocumentEntity>(documentDto);
       
        using var transaction = await _appDb.Database.BeginTransactionAsync();

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