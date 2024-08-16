using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fleet.Interfaces.Service;
using Fleet.Controllers.Model.Request.Usuario;

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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Criar([FromBody] UsuarioRequest usuarioRequest)
        {
            await _usuarioService.Criar(usuarioRequest);

            return Created();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Atualizar([FromBody] UsurioPutRequest usuarioRequest)
        {
            var clain = User.Claims.FirstOrDefault(x => x.Type == "user");
            if (clain == null ||  string.IsNullOrEmpty(clain.Value))
                return Unauthorized();

            await _usuarioService.Atualizar(clain.Value, usuarioRequest);

            return Ok();
        }
    }
}