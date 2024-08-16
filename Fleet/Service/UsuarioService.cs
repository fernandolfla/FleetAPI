using Fleet.Helpers;
using Fleet.Interfaces.Repository;
using Fleet.Interfaces.Service;
using Fleet.Models;
using Fleet.Validators;
using Fleet.Resources;
using Fleet.Controllers.Model.Request.Usuario;
using AutoMapper;
using Fleet.Enums;
using Fleet.Controllers.Model.Response.Usuario;
using Fleet.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fleet.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private string Secret { get => _configuration.GetValue<string>("Crypto:Secret"); }

        public UsuarioService(IUsuarioRepository usuarioRepository,
                            IConfiguration configuration,
                            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task Criar(UsuarioRequest user)
        {
            Usuario usuario =_mapper.Map<Usuario>(user);
            await Validar(usuario, UsuarioRequestEnum.Criar);

            await _usuarioRepository.Criar(usuario);
        }

         public async Task Atualizar(string id, UsuarioRequest user)
        {
            var usuarioId = int.Parse(CriptografiaHelper.DescriptografarAes(id, Secret));
            var usuario = await _usuarioRepository.Buscar(x => x.Id == usuarioId);

            if(usuario == null) throw new BussinessException("Não foi possivel atualizar o usuário");
            //Validar o objeto que vindo da request

            usuario.Nome = user.Nome;
            usuario.Email = user.Email;

            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task Deletar(string id)
        {
            var usuario = new Usuario {
                Id = int.Parse(CriptografiaHelper.DescriptografarAes(id, Secret))
            };
            await Validar(usuario, UsuarioRequestEnum.Deletar);

            await _usuarioRepository.Deletar(usuario.Id);
        }

        public async Task<List<UsuarioResponse>> Listar()
        {
            var usuarios =  await _usuarioRepository.Listar();

            List<UsuarioResponse> usuariosResponse = [];

            foreach(var usuario in usuarios) {
                usuariosResponse.Add(new UsuarioResponse(
                                    CriptografiaHelper.CriptografarAes(usuario.Id.ToString(), Secret), 
                                    usuario.Nome, 
                                    usuario.CPF, 
                                    usuario.Email, 
                                    usuario.UrlImagem));
            }
            return usuariosResponse;
        }

        private async Task Validar(Usuario usuario, UsuarioRequestEnum request)
        {
            var validator = new UsuarioValidator(_usuarioRepository, request);
            var validationResult = await validator.ValidateAsync(usuario);
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