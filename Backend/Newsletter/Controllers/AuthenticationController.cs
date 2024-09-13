using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;
using Newsletter.Entities;
using Newsletter.Services;
using Newsletter.Services.Contracts;

namespace Newsletter.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationService _authService;
        private readonly IRolesService _roleService;
        private readonly JwtTokenService _tokenService;

        public AuthenticationController(IAuthenticationService authService, IRolesService roleService, JwtTokenService tokenService) {
            this._authService = authService;
            this._roleService = roleService;
            this._tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationData registrationData) {
            try {
                var response = await this._authService.RegisterAsync(registrationData);
                return this.Ok(response);
            } catch (UnauthorizedAccessException ex) {
                return this.Unauthorized();
            } catch (Exception ex) {
                return this.BadRequest(ex);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData) {
            try {
                var userResponse = await this._authService.VerifyAsync(loginData);
                var token = this.ValidateAndGenerateToken(userResponse);
                return this.Ok(new { Token = token });
            } catch (UnauthorizedAccessException ex) {
                return this.Unauthorized();
            } catch (Exception ex) {
                return this.BadRequest(ex);
            }
        }

        private async Task<string> ValidateAndGenerateToken(Response<User> userResponse) {
            if (!userResponse.IsSuccess || userResponse.Result == null) {
                throw new UnauthorizedAccessException();
            }

            var roles = await this._authService.GetUserRolesAsync(userResponse.Result.Id);
            if (roles.IsSuccess || roles.Result == null || !roles.Result.Any()) {
                throw new UnauthorizedAccessException();
            }

            return _tokenService.GenerateToken(userResponse.Result, userResponse.Result.Roles.Select(x => x.Code).ToList());
        }
    }
}
