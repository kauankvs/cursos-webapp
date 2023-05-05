using Azure;
using Mawe.Models;
using Mawe.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Mawe.Services.Implementations
{
    public class CursoService: ICursoService
    {
        private readonly IUsuariosCollectionService _context;
        public CursoService(IUsuariosCollectionService context)
            => _context = context;

        public async Task<Curso> AdicionarCursoAsync(string email, CursoDTO cursoInput)
        {
            Curso curso = new()
            {
                Nome = cursoInput.Nome,
                Descricao = cursoInput.Descricao,
                NomeUnico = cursoInput.NomeUnico,
                Preco = cursoInput.Preco,
                DataDeCriacao = DateTime.UtcNow,
                CapaUrl= cursoInput.CapaUrl,
                DuracaoEmMinutos = cursoInput.DuracaoEmMinutos,
                Categoria = cursoInput.Categoria.ToString(),
            };
            await _context.AdicionarCursoAsync(email, curso);
            return curso;
        }

        public async Task<List<Curso>> SelecionarTodosCursosAsync()
        { 
            List<Usuario> usuarios = await _context.SelecionarTodosUsuariosTutoresAsync();
            List<Curso> cursos = new List<Curso>();
            foreach (var usuario in usuarios)
                foreach (var curso in usuario.CursosLecionados)
                    cursos.Add(curso);

            return cursos;
        }
        
        public async Task<Curso>? SelecionarCursoPorNomeAsync(string nomeUnico)
        {
            Usuario? usuario = await _context.SelecionarUsuariosPorNomeDoCurso(nomeUnico);
            if (usuario == null)
                return null;

            Curso curso = usuario.CursosLecionados.FirstOrDefault(c => c.NomeUnico.Equals(nomeUnico));
            return curso;
        }

    }
}
