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
        public Task<Curso> AdicionarCursoAsync(string email, CursoDTO cursoInput);
        public Task<List<Curso>> SelecionarTodosCursosAsync();
    }
}
