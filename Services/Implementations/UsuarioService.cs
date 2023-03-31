﻿using Azure;
using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CursosWebApp.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly CursosWebAppContext _context;
        private readonly ICriptografia _criptografia;
        public UsuarioService(CursosWebAppContext context, ICriptografia criptografia)
        {
            _context = context;
            _criptografia = criptografia;
        }

        public async Task<Usuario> CriarUsuarioAsync(UsuarioDTO usuarioInput)
        {
            string senhaHash = _criptografia.TransformarSenhaEmHash(usuarioInput.Senha);
            Usuario usuario = new Usuario
            {
                Nome = usuarioInput.Nome,
                Sobrenome = usuarioInput.Sobrenome,
                Username = usuarioInput.Username,
                Email = usuarioInput.Email,
                Senha = senhaHash,
                Idade = usuarioInput.Idade,
                Papel = usuarioInput.Papel,
            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> ValidarLoginDeUsuarioAsync(LoginDTO loginInput)
        {
            bool senhaValida = await _criptografia.VerificarValidadeDaSenha(loginInput.Email, loginInput.Senha);
            if (senhaValida == false)
                return null;

            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == loginInput.Email);
            return usuario;
        }

        public async Task<Usuario> ReceberUsuarioAsync(string email)
        {
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(user => user.Email == email);
            if (usuario == null)
                return null;

            return usuario;
        }
    }
}
