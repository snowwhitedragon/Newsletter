using Newsletter.Data;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IAuthenticationService {
        Task<Response<bool>> RegisterAsync(RegistrationData data);
        Task<Response<User>> VerifyAsync(LoginData data);
    }
}
