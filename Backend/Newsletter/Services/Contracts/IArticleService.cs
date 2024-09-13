using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IArticleService {
        Task<Response<IEnumerable<Article>>> SearchAsync(ArticleSearchRequest searchRequest);
        Task<Response<Article>> GetByIdAsync(int id);
        Task<Response<Article>> CreateAsync(int id);
        Task<Response<Article>> UpdateAsync(int id);
        Task<Response<Article>> PublishAsync(Article article);
        Task<Response<Article>> DeleteAsync(int id);

    }
}
