using Fleet.Controllers.Model.Request.Usuario;
using Fleet.Models;

namespace Fleet.Interfaces.Service
{
    public interface IUsuarioService
    {
        void Criar(UsuarioRequest user);
        string Logar(string email, string senha);
    }
}