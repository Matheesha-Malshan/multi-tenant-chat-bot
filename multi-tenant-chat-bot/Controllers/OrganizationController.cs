using Microsoft.AspNetCore.Mvc;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Service;

namespace multi_tenant_chatBot.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationService _organizationService;
    
    public OrganizationController(IOrganizationService organization){
         _organizationService=organization;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDetailsDto org)
    {
        var result=await _organizationService.CreateOrganization(org);
        return Ok();
    }
    
    



}
