using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newsletter.Data;
using Newsletter.Entities;
using Newsletter.Entities.Data;
using Newsletter.Services.Contracts;

namespace Newsletter.Services {
    public class AuthenticationService : IAuthenticationService {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public AuthenticationService(AppDbContext context) {
            this._context = context;
            this._passwordHasher = new PasswordHasher<User>();
        }

        public async Task<Response<bool>> RegisterAsync(RegistrationData data) {
            var response = new Response<bool>();

            try {
                var existingUser = _context.Users.FirstOrDefaultAsync(u => u.Username == data.Username);
                if (existingUser != null) {
                    response.AddError("Benutzername ist bereits vergeben");
                    return response;
                }

                var newUser = new User {
                    Username = data.Username,
                    DisplayName = data.DisplayName,
                    RegistratedAt = DateTime.Now,
                    OrganizationId = data.OrganizationId
                };

                newUser.PasswordHash = this._passwordHasher.HashPassword(newUser, data.Password);
                newUser.Roles = await this._context.Roles.Where(x => data.Roles.Contains(x.Id)).ToListAsync();

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                response.Result = true;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }
            
            return response;
        }

        public async Task<Response<User>> VerifyAsync(LoginData data) {
            var response = new Response<User>();
            try {
                var user = await _context.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Username == data.Username);
                if (user == null) {
                    response.AddError("Ungültiger Benutzername oder Passwort");
                    return response;
                }

                bool isValid = this.ValidatePassword(user, data.Password);
                if (!isValid) {
                    response.AddError("Ungültiger Benutzername oder Passwort");
                    return response;
                }

                response.Result = user;
            } catch (Exception ex) {
                response.AddError(ex.Message);
            }

            return response;
        }

        private bool ValidatePassword(User user, string password) {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}