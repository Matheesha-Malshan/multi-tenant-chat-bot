using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController
{
    private readonly IDocumentService _documentService;
    
    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }
    
    [HttpPost("create")]
    public async Task CreateDocument([FromForm]DocumentDto documentDto)
    {
         await _documentService.CreateDocument(documentDto);
    }
}