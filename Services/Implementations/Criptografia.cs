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
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            return senhaHash;
        }

        public async Task<bool> VerificarValidadeDaSenha(string email, string senha)
        {
            Usuario usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
            var senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
            return senhaValida;
        }
    }
}
