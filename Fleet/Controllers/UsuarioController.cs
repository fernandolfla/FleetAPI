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


        [HttpPost("[Action]")]
        [AllowAnonymous]
        public IActionResult Recuperar([FromBody] string email)  //recebe um e-mail, verifica se ele existe na base, manda código para validar
        {
            
            return Ok(new
            {
                email = "Recuperado",  //Iniciar
                
            });
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public IActionResult ConfirmarCodigo([FromBody] string email, string codigo)  //Recebe e-mail e código para resetar a senha
        {

            return Ok(new
            {
                email = "Redefinir",
                codigo = "codigo",

            });
        }

        [HttpPost("[Action]")]
        [AllowAnonymous]
        public IActionResult AlterarSenha([FromBody] string email , string senha, string codigo)  //Recebe o email e senha , enviar o código junto para alterar
        {
            return Ok(new
            {
                email = "Redefinir",
                codigo = "codigo",
                senha = "senha",

            });
        }

        [HttpPost("")]
        [AllowAnonymous]
        public IActionResult Criar([FromBody] UsuarioRequest usuarioRequest)
        {
            _usuarioService.Criar(usuarioRequest);

            return Created();
        }
    }
}