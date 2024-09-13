using Newsletter.Data;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IRolesService {
        Task<Response<User>> AssignRoleToUserAsync(int userId, int roleId);
    }
}
