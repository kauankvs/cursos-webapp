using Mawe.Models;

namespace Mawe.Services.Interfaces
{
    public interface IUsuariosCollectionService
    {
        public Task AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> SelecionarUsuarioPorEmailAsync(string email);
        public Task AdicionarCursoTutorAsync(string email, Curso curso);
        public Task<List<Usuario>> SelecionarTodosUsuariosTutoresAsync();
        public Task<Usuario> SelecionarUsuariosPorNomeDoCurso(string nomeUnico);
        public Task AdicionarCursoAlunoAsync(string email, string nomeDoCurso);
    }
}
