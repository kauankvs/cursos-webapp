using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CursosWebApp.Services.Implementations
{
    public class Criptografia : ICriptografia
    {
        private readonly CursosWebAppContext _context;
        public Criptografia(CursosWebAppContext context)
            => _context = context;

        public string TransformarSenhaEmHash(string senha)
        {
            var senhaHash = BCrypt.Net.BCrypt.EnhancedHashPassword(senha);
            return senhaHash;
        }

        public async Task<bool> VerificarValidadeDaSenha(string username, string senha)
        {
            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Username == username);
            var senhaValida = BCrypt.Net.BCrypt.EnhancedVerify(senha, usuario.Senha);
            return senhaValida;
        }
    }
}
