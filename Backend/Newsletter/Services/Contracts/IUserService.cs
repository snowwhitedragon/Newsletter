using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IUserService : IService<User, SearchRequestBase> {
        Task<Response<MyUserData>> GetUserProfileAsync(Guid id);
    }
}
