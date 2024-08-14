
using Fleet.Controllers.Model.Request;
using Fleet.Controllers.Model.Request.Auth;
using Fleet.Controllers.Model.Response.Auth;
using Fleet.Controllers.Model.Response.Usuario;
using Fleet.Helpers;
using Fleet.Interfaces.Repository;
using Fleet.Interfaces.Service;
using Fleet.Models;
using Fleet.Resources;
using Fleet.Validators;

namespace Fleet.Service;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private string Secret { get => _configuration.GetValue<string>("Crypto:Secret"); }
    private readonly IEmailService _emailService;

    public AuthService(IUsuarioRepository usuarioRepository,
                        ITokenService tokenService,
                        IConfiguration configuration,
                        IEmailService emailService)
    {
        _configuration = configuration;
        _tokenService = tokenService;
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
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

    public async Task<string> EsqueceuSenha(EsqueceuSenhaRequest request)
    {
        await Validar(request);
        Random random = new();
        int randomNumber = random.Next(0, 10000); 
        string codigo = randomNumber.ToString("D4");
        var usuario = await _usuarioRepository.BuscarEmail(request.Email);
        await _emailService.EnviarEmail(request.Email, usuario.Nome, "Recuperação de senha", codigo);
        return codigo;
    }

    private async Task Validar(EsqueceuSenhaRequest request)
    {
        var validator = new AuthValidator(_usuarioRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new BussinessException(errors);
        }
    }
}
