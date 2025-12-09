using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.ApiService;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
[RequiredApiKey]
public class SemanticSearchController: ControllerBase
{
    private readonly ISemanticSearchService _semanticSearchService;
    
    public SemanticSearchController(ISemanticSearchService semanticSearchService)
    {
        _semanticSearchService = semanticSearchService;
    }
    
    [HttpPost("askQuery")]
    public async Task<string> AskQueary(SematicSearchingDto semanticSearchingDto)
    {
        
        var apiKey=HttpContext.Items["ApiKey"]?.ToString();
        
        if (apiKey == null)
        {
            return "no api key found";
        }
        
        semanticSearchingDto.ApiKey = apiKey;
        return await _semanticSearchService.SearchEmbeddings(semanticSearchingDto);
        
    }
}