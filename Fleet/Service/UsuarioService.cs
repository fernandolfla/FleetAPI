using Fleet.Helpers;
using Fleet.Interfaces.Repository;
using Fleet.Interfaces.Service;
using Fleet.Models;
using Fleet.Validators;
using Fleet.Resources;

namespace Fleet.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public string Secret { get => _configuration.GetValue<string>("Crypto:Secret"); }

        public UsuarioService(IUsuarioRepository usuarioRepository, ITokenService tokenService, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public string Logar(string email, string senha)
        {
            var usuario = new Usuario { Email = email, Senha = senha };
            Validar(usuario, true);

            var usuarioBD = _usuarioRepository.Buscar().FirstOrDefault(x => x.Ativo && x.Email == usuario.Email && x.Senha == usuario.Senha);
            if (usuarioBD == null)
                throw new UnauthorizedAccessException(Resource.usuario_emailSenhaInvalido);

            return _tokenService.GenerateToken(usuarioBD);
        }

        public void Criar(Usuario user)
        {
            Validar(user, false);

            _usuarioRepository.Criar(user);
        }

        private void Validar(Usuario usuario, bool eLogin)
        {
            var validator = new UsuarioValidator(_usuarioRepository, eLogin);
            var validationResult = validator.Validate(usuario);
            if (validationResult.IsValid)
            {
                usuario.Senha = CriptografiaHelper.CriptografarAes(usuario.Senha, Secret) ?? throw new BussinessException(Resource.usuario_falhaCriptografia);
            }
            else
            {
                var errors = string.Join(";", validationResult.Errors.Select(x => x.ErrorMessage));
                throw new BussinessException(errors);
            }
        }
    }
}