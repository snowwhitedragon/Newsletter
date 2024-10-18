using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        public IActionResult ReturnJsonResult(object obj) {
            var options = new JsonSerializerOptions {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };

            var jsonString = JsonSerializer.Serialize(obj, options);
            return this.Ok(jsonString);
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
