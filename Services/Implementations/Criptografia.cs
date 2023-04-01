using Mawe.Models;
using Mawe.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mawe.Services.Implementations
{
    public class Criptografia : ICriptografia
    {
        private readonly IUsuariosCollectionService _context;
        public Criptografia(IUsuariosCollectionService context)
            => _context = context;

        public string TransformarSenhaEmHash(string senha)
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            return senhaHash;
        }

        public async Task<bool> VerificarValidadeDaSenha(string email, string senha)
        {
            Usuario usuario = await _context.SelecionarUsuarioPorEmailAsync(email);
            var senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);
            return senhaValida;
        }
    }
}
