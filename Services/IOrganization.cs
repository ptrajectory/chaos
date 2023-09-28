
using chaos.Dtos.Organization;

namespace chaos.Services;

public interface IOrganization {


    public string createOrganization(CreateOrganization NewOrganizationData);

    public GetOrganization? getOrganization(string OrganizationID);

    public GetOrganization? updateOrganization(string OrganizationID, UpdateOrganization data);

}