using System;
using Fleet.Enums;

namespace Fleet.Controllers.Model.Response.Usuario;

public class UsuarioResponse
{
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UrlImagem { get; set; } = string.Empty;
    public PapelEnum Papel { get; set; }
}
