using Fleet.Models;

namespace Fleet.Interfaces.Service
{
    public interface IUsuarioService
    {
        void Criar(Usuario user);
        string Logar(string email, string senha);
    }
}