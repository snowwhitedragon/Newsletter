using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter.Services {
    public class RoleService : IRoleService {
        private readonly AppDbContext _context;
        public RoleService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<User>> AssignRoleToUserAsync(Guid userId, Guid roleId) {
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

        public async Task<Response<Role>> GetByIdAsync(Guid id) {
            var response = new Response<Role>();
            try {
                var role = await this._context.Roles.FirstOrDefaultAsync(r => r.Id == id);
                if (role == null) {
                    response.AddError("Rolle wurde nicht gefunden");
                    return response;
                }

                response.Result = role;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Role>>> GetUserRolesAsync(Guid userId) {
            var response = new Response<IEnumerable<Role>>();
            try {
                var roles = await this._context.Roles.Where(r => r.Users.Any(u => u.Id == userId)).ToListAsync();
                response.Result = roles;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<IEnumerable<Role>>> SearchAsync(SearchRequestBase searchRequest) {
            var response = new Response<IEnumerable<Role>>();
            try {
                IQueryable<Role> query = this._context.Roles;

                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(r => r.Title.Contains(searchRequest.SearchTerm) || r.Code.Contains(searchRequest.SearchTerm));
                }

                query = this.OrderBy(query, searchRequest);

                response.Result = await query.Skip(searchRequest.Skip).Take(searchRequest.Take).ToListAsync();
                if (!response.Result.Any()) {
                    response.AddError("Die Suche ergab keine Ergebnisse");
                }
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        private IQueryable<Role> OrderBy(IQueryable<Role> query, SearchRequestBase searchRequest) {
            switch (searchRequest.OrderBy) {
                case "Code":
                    return searchRequest.Descending ? query.OrderByDescending(r => r.Code) : query.OrderBy(r => r.Code);
                case "Title":
                    return searchRequest.Descending ? query.OrderByDescending(r => r.Title) : query.OrderBy(r => r.Title);
                default:
                    return query.OrderBy(r => r.Code);
            }
        }
    }
}
