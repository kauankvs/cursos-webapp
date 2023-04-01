using Mawe.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mawe.Services.Interfaces
{
    public interface IUsuarioService
    {
        public Task<Usuario> CriarUsuarioAsync(UsuarioDTO usuarioInput);
        public Task<Usuario> ValidarLoginDeUsuarioAsync(LoginDTO loginInput);
        public Task<Usuario> ReceberUsuarioAsync(string email);
    }
}
