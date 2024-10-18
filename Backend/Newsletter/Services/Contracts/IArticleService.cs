using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IArticleService : IService<ArticleData, ArticleSearchRequest> {
        Task<Response<ArticleData>> PublishAsync(Guid id);
        Task<Response<Article>> GetArticleWithSubscribersAsync(Guid id);
    }
}
