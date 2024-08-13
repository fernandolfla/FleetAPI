﻿using Fleet.Controllers.Model.Request.Usuario;
using Fleet.Controllers.Model.Response.Usuario;
using Fleet.Models;

namespace Fleet.Interfaces.Service
{
    public interface IUsuarioService
    {
        Task Criar(UsuarioRequest user);
        Task Atualizar(string id, UsuarioRequest user);
        Task Deletar(string id);
        Task<List<UsuarioResponse>> Listar();
    }
}