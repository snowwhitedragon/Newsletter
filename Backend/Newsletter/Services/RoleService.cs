using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter.Services {
    public class RoleService : IRolesService {
        private readonly AppDbContext _context;
        public RoleService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<User>> AssignRoleToUserAsync(int userId, int roleId) {
            var response = new Response<User>();
            try {
                var role = await this._context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
                if (role == null) {
                    response.AddError("Rolle existiert nicht.");
                    return response;
                }

                var user = await this._context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) {
                    response.AddError("User existiert nicht.");
                    return response;
                }

                user.Roles.Add(role);
                await _context.SaveChangesAsync();
                response.Result = user;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }
    }
}
