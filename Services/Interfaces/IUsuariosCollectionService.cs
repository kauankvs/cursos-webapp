using Mawe.Models;

namespace Mawe.Services.Interfaces
{
    public interface IUsuariosCollectionService
    {
        public Task AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> SelecionarUsuarioPorEmailAsync(string email);
    }
}
