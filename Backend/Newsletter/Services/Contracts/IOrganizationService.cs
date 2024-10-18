using Newsletter.Data;
using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface IOrganizationService : IViewService<OrganizationData, OrganizationSearchRequest> {
    }
}
