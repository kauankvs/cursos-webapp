using CursosWebApp.Models;

namespace CursosWebApp.Services.Interfaces
{
    public interface ITokenService
    {
        public string GerarToken(Usuario usuario);

    }
}
