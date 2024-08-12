using Fleet.Helpers;
using Fleet.Interfaces.Repository;
using Fleet.Interfaces.Service;
using Fleet.Models;
using Fleet.Validators;
using Fleet.Resources;
using Fleet.Controllers.Model.Request.Usuario;
using AutoMapper;
using Fleet.Enums;

namespace Fleet.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private string Secret { get => _configuration.GetValue<string>("Crypto:Secret"); }

        public UsuarioService(IUsuarioRepository usuarioRepository,
                            ITokenService tokenService,
                            IConfiguration configuration,
                            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task Criar(UsuarioRequest user)
        {
            Usuario usuario =_mapper.Map<Usuario>(user);
            Validar(usuario, UsuarioRequestEnum.Criar);

            await _usuarioRepository.Criar(usuario);
        }

         public async Task Atualizar(int id, UsuarioRequest user)
        {
            Usuario usuario =_mapper.Map<Usuario>(user);
            usuario.Id = id;
            Validar(usuario, UsuarioRequestEnum.Atualizar);
            await _usuarioRepository.Atualizar(id, usuario);
        }

        public async Task Deletar(int id)
        {
            Usuario usuario =_mapper.Map<Usuario>(id);
            Validar(usuario, UsuarioRequestEnum.Deletar);

            await _usuarioRepository.Deletar(id);
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _usuarioRepository.Listar();
        }

        private void Validar(Usuario usuario, UsuarioRequestEnum request)
        {
            var validator = new UsuarioValidator(_usuarioRepository, request);
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