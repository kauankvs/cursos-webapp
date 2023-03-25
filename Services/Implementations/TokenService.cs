using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CursosWebApp.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public string GerarToken(Usuario usuario)
        {
            byte[] chave = Encoding.ASCII.GetBytes(Configuracoes.Segredo);
            var processadorDoToken = new JwtSecurityTokenHandler();
            var gerenciarDoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Papel.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature),

            };
            var token = processadorDoToken.CreateToken(gerenciarDoToken);
            var tokenFinal = processadorDoToken.WriteToken(token);
            return tokenFinal;
        }
    }
}
