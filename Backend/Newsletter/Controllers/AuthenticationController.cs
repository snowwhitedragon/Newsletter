using Microsoft.AspNetCore.Mvc;
using Newsletter.Data;

namespace Newsletter.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger) {
            _logger = logger;
        }

        [HttpPost(Name = "Login")]
        public bool Login(LoginData loginData) {
            return true;
        }
    }
}
