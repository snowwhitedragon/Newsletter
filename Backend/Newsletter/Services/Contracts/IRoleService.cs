using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IRoleService: IViewService<Role, SearchRequestBase> {
        Task<Response<User>> AssignRoleToUserAsync(Guid userId, Guid roleId);
    }
}
