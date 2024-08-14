using Fleet.Controllers.Model.Request;
using Fleet.Controllers.Model.Request.Auth;
using Fleet.Controllers.Model.Response.Auth;
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
            LoginResponse response = await _authService.Logar(loginRequest);
            return Ok(response);
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public IActionResult EsqueceuSenha([FromBody] EsqueceuSenhaRequest request)  //Recebe e-mail e código para resetar a senha
        {

            return Ok(new
            {
                email = "Redefinir",
                codigo = "codigo",
            });
        }
    }
}
