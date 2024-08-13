using System;
using Fleet.Controllers.Model.Request;
using Fleet.Controllers.Model.Response.Auth;
using Fleet.Controllers.Model.Response.Usuario;

namespace Fleet.Interfaces.Service;

public interface IAuthService
{
    Task<LoginResponse> Logar(LoginRequest login);
}
