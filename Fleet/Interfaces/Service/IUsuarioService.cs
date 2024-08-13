using Fleet.Controllers.Model.Request.Usuario;
using Fleet.Models;

namespace Fleet.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task Criar(UsuarioRequest user);
        Task Atualizar(int id, UsuarioRequest user);
        Task Deletar(int id);
        Task<List<Usuario>> Listar();
    }
}