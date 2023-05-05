using Azure;
using Mawe.Models;
using Mawe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Mawe.Services.Implementations
{
    public interface ICursoService
    {
        public Task<Curso> AdicionarCursoTutorAsync(string email, CursoDTO cursoInput);
        public Task<List<Curso>> SelecionarTodosCursosAsync();
        public Task<Curso> SelecionarCursoPorNomeAsync(string nomeUnico);
        public Task AdicionarCursoAlunoAsync(string email, string nomeDoCurso);
    }
}
