using Newsletter.Data;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IAuthenticationService {
        Task<Response<User>> RegisterAsync(RegistrationData data);
        Task<Response<bool>> VerifyAsync(LoginData data);
        Task<Response<IEnumerable<Role>>> GetUserRolesAsync(int id);
    }
}
