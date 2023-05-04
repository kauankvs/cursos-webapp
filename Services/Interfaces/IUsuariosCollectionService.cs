using Mawe.Models;

namespace Mawe.Services.Interfaces
{
    public interface IUsuariosCollectionService
    {
        public Task AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> SelecionarUsuarioPorEmailAsync(string email);
        public Task AdicionarCursoAsync(string email, Curso curso);
        public Task<List<Usuario>> SelecionarTodosUsuariosTutoresAsync();
        public Task<Usuario> SelecionarUsuariosPorNomeDoCurso(string nomeUnico);
    }
}
