using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Fleet.Controllers.Model.Request;
using Fleet.Interfaces.Service;

namespace Fleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public IActionResult Logar([FromBody] LoginRequest loginRequest)
        {
            var token = _usuarioService.Logar(loginRequest.Email, loginRequest.Password); 

            return Ok(new
            {
                Token = token,
                Email = loginRequest.Email,
            });
        }

        //[HttpPost("[Action]")]
        //[Authorize(Policy = "admin")]
        //public IActionResult Criar([FromBody] UsuarioRequest usuarioRequest)
        //{
        //    _usuarioService.Criar(usuarioRequest);
        //    return Ok();
        //}

    }
}