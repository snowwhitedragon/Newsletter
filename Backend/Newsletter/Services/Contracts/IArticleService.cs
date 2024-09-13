using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;

namespace Newsletter.Services.Contracts {
    public interface IArticleService : IService<Article, ArticleSearchRequest> {
        Task<Response<Article>> PublishAsync(int id);

    }
}
