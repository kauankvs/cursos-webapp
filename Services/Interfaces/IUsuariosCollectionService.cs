using CursosWebApp.Models;

namespace CursosWebApp.Services.Interfaces
{
    public interface IUsuariosCollectionService
    {
        public Task AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> SelecionarUsuarioPorEmailAsync(string email);
    }
}
