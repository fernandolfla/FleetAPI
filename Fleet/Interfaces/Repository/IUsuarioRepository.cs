using Fleet.Models;

namespace Fleet.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        Task Criar(Usuario user);
        Task<bool> ExisteEmail(string email, int? id = null);
        Task<bool> ExisteCpf(string cpf);
        Task Deletar(int id);
        Task Atualizar(int id, Usuario usuarioAtualizado);
        Task<Usuario> BuscarEmail(string email);
        Task<bool> Existe(int id);
        Task<List<Usuario>> Listar();
    }
}