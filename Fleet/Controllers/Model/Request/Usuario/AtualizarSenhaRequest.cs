using System;

namespace Fleet.Controllers.Model.Request.Usuario;

public class AtualizarSenhaRequest
{
    public string email { get; set; } = string.Empty;
    public string novaSenha { get; set; } = string.Empty;

}
