using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [Authorize(Roles = $"{Roles.Admin},{Roles.Systemadmin}")]
    public class RolesController: BaseViewController<HeaderData, SearchRequestBase> {
        private readonly IRoleService _service;
        public RolesController(IRoleService service)
            : base (service) {
            this._service = service;
        }

        [HttpGet("user/{userId}/role/{roleId}")]
        public async Task<IActionResult> AssignRole(Guid userId, Guid roleId) {
            var result = await this._service.AssignRoleToUserAsync(userId, roleId);
            return ReturnJsonResult(result);
        }
    }
}
