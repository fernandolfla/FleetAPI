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

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Criar([FromBody] UsuarioRequest usuarioRequest)
        {
            await _usuarioService.Criar(usuarioRequest);

            return Created();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioRequest usuarioRequest, [FromRoute] string id)
        {
            await _usuarioService.Atualizar(id, usuarioRequest);

            return Ok();
        }

        [HttpPut("[Action]")]
        [AllowAnonymous]
        public async Task<IActionResult> AlterarSenha([FromBody] AtualizarSenhaRequest request)
        {
            await _usuarioService.AlterarSenha(request.email, request.novaSenha);

            return Ok();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Deletar([FromRoute] string id)
        {
            await _usuarioService.Deletar(id);

            return Ok();
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuarioService.Listar();

            return Ok(usuarios);
        }
    }
}