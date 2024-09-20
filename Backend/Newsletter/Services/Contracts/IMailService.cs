using Newsletter.Data;

namespace Newsletter.Services.Contracts {
    public interface IMailService {
        Task<Response<bool>> SendNewsletterToSubscibersAsync(Guid articleId);
        Task<Response<bool>> SendAsync(IEnumerable<string> recipients, string subject, string body);
    }
}
