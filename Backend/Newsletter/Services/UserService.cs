using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter {
    public class UserService : IUserService {
        private readonly AppDbContext _context;
        private readonly Guid _userId;

        public UserService(AppDbContext context) {
            this._context = context;
        }

        public async Task<Response<User>> CreateAsync(User newEntry) {
            var response = new Response<User>();
            try {
                this._context.Users.Add(newEntry);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(Guid id) {
            var response = new Response<bool>();
            try {
                var entry = await this._context.Users.FindAsync(id);

                if (entry == null) {
                    response.AddError("Benutzer wurde nicht gefunden.");
                    return response;
                }

                this._context.Users.Remove(entry);
                await _context.SaveChangesAsync();
                response.Result = true;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<User>> GetByIdAsync(Guid id) {
            var response = new Response<User>();
            try {
                var article = await this._context.Users
                    .Include(x => x.Organization)
                    .Include(x => x.Roles)
                    .Include(x => x.Contact)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (article == null) {
                    response.AddError("Benutzer wurde nicht gefunden.");
                    return response;
                }

                response.Result = article;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public async Task<Response<MyUserData>> GetUserProfileAsync(Guid id) {
            var myProfileResponse = new Response<MyUserData>();
            var response = await this.GetByIdAsync(id);
            myProfileResponse.Errors = response.Errors;
            if (response.Result != null) {
                myProfileResponse.Result = new MyUserData(response.Result);
            }

            return myProfileResponse;
        }

        public async Task<Response<IEnumerable<User>>> SearchAsync(SearchRequestBase searchRequest) {
            var response = new Response<IEnumerable<User>>();
            try {
                var query = this._context.Users
                    .Include(x => x.Organization)
                    .Include(x => x.Roles)
                    .Include(x => x.Contact).AsQueryable();
                if (!string.IsNullOrWhiteSpace(searchRequest.SearchTerm)) {
                    query = query.Where(x => 
                    x.Username.Contains(searchRequest.SearchTerm));
                }

                response.Result = await query.OrderByDescending(x => x.RegistratedAt).ToListAsync();
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        public Task<Response<User>> UpdateAsync(User updatedEntry) {
            throw new NotImplementedException();
        }
    }
}
