using Newsletter.Data;
using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface INewsletterService : IViewService<NewsletterData, SearchRequestBase> {
    }
}
