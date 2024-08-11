using Fleet.Controllers.Model.Request;
using Fleet.Interfaces.Service;
using Fleet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Logar([FromBody] LoginRequest loginRequest)
        {
            var response =  _authService.Logar(loginRequest); 

            return Ok(response);
        }
    }
}
