
using AutoMapper;
using chaos.Dtos.Organization;
using chaos.Exceptions.Services;
using chaos.Models;

namespace chaos.Services;


public class Organization : IOrganization
{

    private readonly ChatContext _context;
    private readonly IMapper _mapper;

    public Organization(ChatContext context, IMapper mapper){
        this._context = context;
        this._mapper = mapper;
        
    }

    public string createOrganization(CreateOrganization NewOrganizationData)
    {
        var organization = this._mapper.Map<Models.Organization>(NewOrganizationData);
       
        this._context.ORGANIZATION.Add(organization);   
        
        this._context.SaveChanges();

        if(organization.ID is null){
            throw new ChaosAppException(resource: "organization", incoming: organization, message: "Organization ID is null"){
                tag = "CREATE ORGANIZATION"
            };
        }

        return organization.ID;
    }

    public GetOrganization? getOrganization(string OrganizationID)
    {
        var org = this._context.ORGANIZATION.FirstOrDefault((org)=>org.ID == OrganizationID);

        var dto = this._mapper.Map<GetOrganization>(org);

        return dto;
    }

    public GetOrganization? updateOrganization(string OrganizationID, UpdateOrganization data)
    {

        var org = this._context.ORGANIZATION.Find(OrganizationID);

        if(org is null) return null;

        this._mapper.Map<UpdateOrganization, Models.Organization>(data, org);

        this._context.SaveChanges();

        var dto = this._mapper.Map<GetOrganization>(org);

        return dto;
    }
}