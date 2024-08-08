﻿using Fleet.Models;

namespace Fleet.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        void Criar(Usuario user);
        IQueryable<Usuario> Buscar();
    }
}