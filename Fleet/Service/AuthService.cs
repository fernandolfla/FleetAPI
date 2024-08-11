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
    public LoginResponse Logar(LoginRequest login)
    {
        var usuarioBD =  _usuarioRepository.Buscar()
                            .FirstOrDefault(
                                x => x.Ativo && x.Email == login.Email && 
                                CriptografiaHelper.DescriptografarAes(x.Senha, Secret) == login.Senha) ?? 
                                throw new UnauthorizedAccessException(Resource.usuario_emailSenhaInvalido);
        var token =  _tokenService.GenerateToken(usuarioBD);

        var usuarioResponse = _mapper.Map<UsuarioResponse>(usuarioBD);
        LoginResponse response = new(usuarioResponse, token);
        
        return response;
    }
}
