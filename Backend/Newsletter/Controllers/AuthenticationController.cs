using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Services;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationService _authService;
        private readonly IRoleService _roleService;
        private readonly JwtTokenService _tokenService;

        public AuthenticationController(IAuthenticationService authService, IRoleService roleService, JwtTokenService tokenService) {
            this._authService = authService;
            this._roleService = roleService;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationData registrationData) {
            var response = await this._authService.RegisterAsync(registrationData);
            return this.Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData) {
            try {
                var userResponse = await this._authService.VerifyAsync(loginData);
                if (!userResponse.IsSuccess || userResponse.Result == null) {
                    return this.Unauthorized(userResponse);
                }

                var response = new Response<string>();
                var roles = userResponse.Result.Roles.Select(r => r.Code).ToList();
                response.Result = this._tokenService.GenerateToken(userResponse.Result.Id, userResponse.Result.Username, roles);
                return this.Ok(response);
            } catch (Exception ex) {
                return this.BadRequest(ex);
            }
        }
    }
}
