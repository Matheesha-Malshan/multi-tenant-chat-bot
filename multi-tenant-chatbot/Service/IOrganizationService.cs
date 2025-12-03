using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service;

public interface IOrganizationService
{
     Task<OrganizationEntity> CreateOrganization(OrganizationDetailsDto org);
}