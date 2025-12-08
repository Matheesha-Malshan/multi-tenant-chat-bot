using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class SemanticSearchController: ControllerBase
{
    private readonly ISemanticSearchService _semanticSearchService;

    public SemanticSearchController(ISemanticSearchService semanticSearchService)
    {
        _semanticSearchService = semanticSearchService;
    }
    
    [HttpPost("askQuery")]
    public async Task<IActionResult> AskQueary(SematicSearchingDto semanticSearchingDto)
    {
        await _semanticSearchService.SearchEmbeddings(semanticSearchingDto);
        return Ok();
    }
}