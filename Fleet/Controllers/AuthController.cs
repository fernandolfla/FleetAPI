using Fleet.Controllers.Model.Request;
using Fleet.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Logar([FromBody] LoginRequest loginRequest)
        {
            var response = await _authService.Logar(loginRequest); 

            return Ok(response);
        }
    }
}
