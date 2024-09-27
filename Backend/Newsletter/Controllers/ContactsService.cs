using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Controllers.Base;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ContactsController : BaseActionController<Contact, SearchRequestBase> {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
            : base(service) {
            this._service = service;
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
        public override Task<IActionResult> Create([FromBody] Contact value) {
            return base.Create(value);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
        public override Task<IActionResult> Delete(Guid id) {
            return base.Delete(id);
        }

        [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
        public override Task<IActionResult> Update([FromBody] Contact value) {
            return base.Update(value);
        }
    }
}
