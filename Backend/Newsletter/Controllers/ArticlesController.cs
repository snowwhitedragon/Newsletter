using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Controllers.Base;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    public class ArticlesController: BaseActionController<Article, ArticleSearchRequest> {
        private readonly IArticleService _service;
        private readonly IMailService _mailService;

        public ArticlesController(IArticleService service, IMailService mailService)
            : base (service) {
            this._service = service;
            this._mailService = mailService;
        }

        [Authorize( Roles = Roles.Employee)]
        public override Task<IActionResult> Create([FromBody] Article value) {
            return base.Create(value);
        }

        [Authorize(Roles = Roles.Employee)]
        public override Task<IActionResult> Update([FromBody] Article value) {
            return base.Update(value);
        }

        [Authorize(Roles = Roles.Employee)]
        public override Task<IActionResult> Delete(Guid id) {
            return base.Delete(id);
        }

        [HttpPost("publish")]
        [Authorize(Roles = Roles.Employee)]
        public async Task<IActionResult> Publish(Guid id) {
            var response = await this._service.PublishAsync(id);
            if (response.IsSuccess && response.Result != null) {
                var mailResponse = await this._mailService.SendNewsletterToSubscibersAsync(response.Result.Id);
                mailResponse.Errors.ForEach(response.AddError);
            }
            
            return Ok(response);
        }
    }
}
