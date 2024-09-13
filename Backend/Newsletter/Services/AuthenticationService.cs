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

        public async Task<Response<IEnumerable<Role>>> GetUserRolesAsync(int id) {
            var response = new Response<IEnumerable<Role>>();
            var user = await this._context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) {
                response.AddError("Benutzer nicht gefunden.");
                return response;
            }

            response.Result = user.Roles.AsEnumerable();
            return response;
        }

        public async Task<Response<bool>> RegisterAsync(RegistrationData data) {
            var response = new Response<bool>();

            try {
                var existingUser = _context.Users.SingleOrDefault(u => u.Username == data.Username);
                if (existingUser != null) {
                    response.AddError("Benutzername ist bereits vergeben.");
                    return response;
                }

                var newUser = new User {
                    Username = data.Username,
                    RegistratedAt = DateTime.Now,
                };

                newUser.PasswordHash = this._passwordHasher.HashPassword(newUser, data.Password);
                newUser.Roles = await this._context.Roles.Where(x => x.Code == "GUEST").ToListAsync();

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

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == data.Username);
            if (user == null) {
                response.AddError("Ungültiger Benutzername oder Passwort.");
                return response;
            }

            bool isValid = this.ValidatePassword(user, data.Password);
            if (!isValid) {
                response.AddError("Ungültiger Benutzername oder Passwort.");
                return response;
            }

            response.Result = user;
            return response;
        }

        private bool ValidatePassword(User user, string password) {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}