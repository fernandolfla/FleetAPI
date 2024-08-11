using AutoMapper;
using Fleet.Controllers.Model.Request.Usuario;
using Fleet.Controllers.Model.Response.Usuario;
using Fleet.Models;

namespace Fleet.Mapper;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<UsuarioRequest, Usuario>();
        CreateMap<Usuario, UsuarioResponse>();
    }
}
