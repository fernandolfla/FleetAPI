using Fleet.Interfaces.Repository;
using Fleet.Repository;
using Fleet.Models;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Criar(Usuario user)
        {
            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }

        public IQueryable<Usuario> Buscar()
        {
            return _context.Usuarios;
        }

        public void Deletar(int id)
        {
            _context.Usuarios.Remove(_context.Usuarios.Find(id));
            _context.SaveChanges();
        }

        public void Atualizar(int id, Usuario usuarioAtualizado)
        {
            var usuario = _context.Usuarios.Find(id);
            usuario.CPF = usuarioAtualizado.CPF;
            usuario.Name = usuarioAtualizado.Name;
            usuario.Email = usuarioAtualizado.Email;
            _context.SaveChanges();
        }
    }
}