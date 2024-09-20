using Microsoft.AspNetCore.Mvc;
using Newsletter.Data.SearchRequests;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseViewController<T, S> : ControllerBase 
        where T : class
        where S : SearchRequestBase {
        private readonly IViewService<T, S> _service;

        protected BaseViewController(IViewService<T, S> service) {
            this._service = service;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] S searchRequest) {
            var result = await this._service.SearchAsync(searchRequest);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var result = await this._service.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
