using Newsletter.Data;

namespace Newsletter.Services.Contracts {
    public interface IService<T, S> {
        Task<Response<IEnumerable<T>>> SearchAsync(S searchRequest);
        Task<Response<T>> GetByIdAsync(int id);
        Task<Response<T>> CreateAsync(T newEntry);
        Task<Response<T>> UpdateAsync(T updatedEntry);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
