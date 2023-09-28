

using chaos.Dtos.Organization;
using chaos.Dtos.User;
using chaos.Services;
using Microsoft.AspNetCore.Mvc;

namespace chaos.Controllers;

[ApiController]
[Route("/api/organizations")]
public class OrganizationController: ControllerBase {

    private readonly IOrganization _organization;
    public OrganizationController(IOrganization organization){
        this._organization = organization;
    }


    [HttpPost]
    public ActionResult createOrgnization(CreateOrganization NewOrgDetails){
        var organization_id = this._organization.createOrganization(NewOrgDetails);
        return Ok(organization_id);
    }

    [HttpGet]
    public ActionResult<GetUser>  getOrganization(string id){

        var organization = this._organization.getOrganization(id);

        return Ok(organization);
    }


    [HttpPatch]
    public ActionResult<UpdateOrganization> updateOrganization(string id, UpdateOrganization UpdatedOrganizationData){
        var new_organization = this._organization.updateOrganization(id, UpdatedOrganizationData);

        return Ok(new_organization);
    }

}