using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newsletter.Controllers.Base;
using Newsletter.Data;
using Newsletter.Data.SearchRequests;
using Newsletter.Entities;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : BaseActionController<User, SearchRequestBase> {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
            : base(userService) {
            this._userService = userService;
        }

        [HttpGet("GetMe")]
        public async Task<IActionResult> GetMe() {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrWhiteSpace(userId)) {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            if (string.IsNullOrWhiteSpace(userId)) {
                return NotFound();
            }

            var me = await this._userService.GetUserProfileAsync(Guid.Parse(userId));
            return this.Ok(me);
        }
    }
}
