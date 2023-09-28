

using AutoMapper;
using chaos.Dtos.App;
using chaos.Dtos.Organization;
using chaos.Models;

namespace chaos.Mappings; 

public class OrganizationMappings: Profile {

    public OrganizationMappings(){
        CreateMap<Organization, GetOrganization>();
        CreateMap<UpdateOrganization, Organization>()
        .ForAllMembers((opts)=> opts.Condition((src, dest, srcMember) => srcMember is not null));
        CreateMap<CreateOrganization, Organization>();
        CreateMap<Apps, GetApp>(); 
    }

}