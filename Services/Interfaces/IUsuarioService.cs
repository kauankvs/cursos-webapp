﻿using CursosWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CursosWebApp.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Usuario> CriarUsuarioAsync(UsuarioDTO usuarioInput);
        public Task<Usuario> ValidarLoginAsync(LoginDTO loginInput);
        public Task<Usuario> ReceberUsuarioAsync(string email);
    }
}
