using System.Threading.Tasks;
using AutoMapper;
using multi_tenant_chatBot.Data;
using multi_tenant_chatBot.Dto;
using multi_tenant_chatBot.Model;

namespace multi_tenant_chatBot.Service.Impl;

public class OrganizationServiceImpl : IOrganizationService
{
    private readonly AppDb _appDb;
    private readonly IMapper _mapper;

    public OrganizationServiceImpl(AppDb appDb, IMapper mapper)
    {
        _appDb = appDb;
        _mapper = mapper;
    }

    public async Task<OrganizationEntity> CreateOrganization(OrganizationDetailsDto org)
    {
        var organizationEntity = _mapper.Map<OrganizationDetailsDto, OrganizationEntity>(org);

        await _appDb.Organizations.AddAsync(organizationEntity);
        await _appDb.SaveChangesAsync();
        return organizationEntity;
    }

}   