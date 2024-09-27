using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
    public class NewslettersController : BaseViewController<Entities.Newsletter, SearchRequestBase> {
        private readonly INewsletterService _service;
        public NewslettersController(INewsletterService service)
            : base (service) {
            this._service = service;
        }
    }
}
