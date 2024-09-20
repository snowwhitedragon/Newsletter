using Newsletter.Data;

namespace Newsletter.Services.Contracts {
    public interface ISubscriptionService {
        Task<Response<bool>> SubscribeAsync(Guid contactId, Guid newsId);
        Task<Response<bool>> UnsubscribeAsync(Guid contactId, Guid newsId);
    }
}
