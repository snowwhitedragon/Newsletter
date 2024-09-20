using Newsletter.Data;
using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface IService<T, S> : IViewService<T, S>
        where T : class
        where S : SearchRequestBase {
        Task<Response<T>> CreateAsync(T newEntry);
        Task<Response<T>> UpdateAsync(T updatedEntry);
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
