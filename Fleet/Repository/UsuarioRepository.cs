using Fleet.Interfaces.Repository;
using Fleet.Repository;
using Fleet.Models;

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
    }
}