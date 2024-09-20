using Newsletter.Data;
using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface IViewService<T, S>
        where T : class
        where S : SearchRequestBase {
        Task<Response<IEnumerable<T>>> SearchAsync(S searchRequest);
        Task<Response<T>> GetByIdAsync(Guid id);
    }
}
