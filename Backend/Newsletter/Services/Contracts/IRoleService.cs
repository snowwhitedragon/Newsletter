using Newsletter.Data;
using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface IRoleService: IViewService<HeaderData, SearchRequestBase> {
        Task<Response<bool>> AssignRoleToUserAsync(Guid userId, Guid roleId);
        Task<Response<IEnumerable<HeaderData>>> GetUserRolesAsync(Guid userId);
    }
}
