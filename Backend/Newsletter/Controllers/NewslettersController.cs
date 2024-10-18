using Microsoft.AspNetCore.Authorization;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
    public class NewslettersController : BaseViewController<NewsletterData, SearchRequestBase> {
        private readonly INewsletterService _service;
        public NewslettersController(INewsletterService service)
            : base (service) {
            this._service = service;
        }
    }
}
