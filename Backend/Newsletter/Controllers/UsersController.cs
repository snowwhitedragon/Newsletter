using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Services;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly IAuthenticationService _authService;

        public UsersController(IAuthenticationService authService, IRoleService roleService, JwtTokenService tokenService) {
            this._authService = authService;
        }

        // TESTPURPOSE
        [HttpGet("me")]
        public async Task<IActionResult> GetMe([FromBody] RegistrationData registrationData) {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            
            return this.Ok();
        }
    }
}
