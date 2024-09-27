using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
    public class OrganizationsController : BaseViewController<Organization, SearchRequestBase> {
        private readonly IOrganizationService _service;
        public OrganizationsController(IOrganizationService service)
            : base (service) {
            this._service = service;
        }
    }
}
