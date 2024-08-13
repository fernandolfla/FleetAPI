﻿using Microsoft.AspNetCore.Authorization;
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
        public IActionResult EsqueceuSenha([FromBody] string email, string codigo)  //Recebe e-mail e código para resetar a senha
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
        public async Task<IActionResult> Criar([FromBody] UsuarioRequest usuarioRequest)
        {
            await _usuarioService.Criar(usuarioRequest);

            return Created();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Atualizar([FromBody] UsuarioRequest usuarioRequest, [FromRoute] int id)
        {
            await _usuarioService.Atualizar(id, usuarioRequest);

            return Ok();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Deletar([FromRoute] int id)
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