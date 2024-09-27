using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IOrganizationService : IViewService<Organization, SearchRequestBase> {
    }
}
