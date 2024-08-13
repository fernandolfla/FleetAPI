using System;
using Fleet.Controllers.Model.Response.Usuario;

namespace Fleet.Controllers.Model.Response.Auth;

public class LoginResponse
{
    protected UsuarioResponse usuario {get; private set;}
    protected string token { get; private set;}
    public LoginResponse(UsuarioResponse usuario, string token)
    {
        this.usuario = usuario;
        this.token = token;
    }
   
}
