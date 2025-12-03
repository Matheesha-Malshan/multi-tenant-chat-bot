using AutoMapper;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Configurations;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<OrganizationDetailsDto, OrganizationEntity>();
    }
}