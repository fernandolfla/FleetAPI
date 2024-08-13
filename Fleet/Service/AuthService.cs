using System;
using AutoMapper;
using Fleet.Controllers.Model.Request;
using Fleet.Controllers.Model.Response.Auth;
using Fleet.Controllers.Model.Response.Usuario;
using Fleet.Helpers;
using Fleet.Interfaces.Repository;
using Fleet.Interfaces.Service;
using Fleet.Resources;

namespace Fleet.Service;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private string Secret { get => _configuration.GetValue<string>("Crypto:Secret"); }

    public AuthService(IUsuarioRepository usuarioRepository,
                        ITokenService tokenService,
                        IMapper mapper,
                         IConfiguration configuration)
    {
        _configuration = configuration;
        _mapper = mapper;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
    }
    public async Task<LoginResponse> Logar(LoginRequest login)
    {
        var usuario =  await _usuarioRepository.BuscarEmail(login.Email) ?? 
                                throw new UnauthorizedAccessException(Resource.usuario_emailSenhaInvalido);
                                
        if(!(usuario.Email == login.Email) || !(CriptografiaHelper.DescriptografarAes(usuario.Senha, Secret) == login.Senha))
            throw new UnauthorizedAccessException(Resource.usuario_emailSenhaInvalido);
            
        var token =  _tokenService.GenerateToken(usuario);

        var usuarioResponse = new UsuarioResponse(
                                    CriptografiaHelper.CriptografarAes(usuario.Id.ToString(), Secret), 
                                    usuario.Nome, 
                                    usuario.CPF, 
                                    usuario.Email, 
                                    usuario.UrlImagem, 
                                    usuario.Papel);
        
        return new LoginResponse(usuarioResponse, token);
    }
}
