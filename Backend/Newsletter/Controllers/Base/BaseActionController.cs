using Microsoft.AspNetCore.Mvc;
using Newsletter.Data.SearchRequests;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers.Base
{
    public abstract class BaseActionController<T, S> : BaseViewController<T, S>
        where T : class
        where S : SearchRequestBase
    {
        private readonly IService<T, S> _service;

        protected BaseActionController(IService<T, S> service)
            : base(service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public virtual async Task<IActionResult> Create([FromBody] T value)
        {
            var result = await this._service.CreateAsync(value);
            return Ok(result);
        }

        [HttpPost("Update")]
        public virtual async Task<IActionResult> Update([FromBody] T value)
        {
            var result = await this._service.CreateAsync(value);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var result = await this._service.DeleteAsync(id);
            return Ok(result);
        }
    }
}
