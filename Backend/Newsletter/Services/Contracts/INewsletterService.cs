using Newsletter.Data.SearchRequests;

namespace Newsletter.Services.Contracts {
    public interface INewsletterService : IViewService<Entities.Newsletter, SearchRequestBase> {
    }
}
