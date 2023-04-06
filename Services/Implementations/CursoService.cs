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
        public CursoService(IUsuariosCollectionService context, ICriptografia criptografia)
        {
            _context = context;
        }

        public async Task<Curso> AdicionarCursoAsync(string email, CursoDTO cursoInput)
        {
            Curso curso = new()
            {
                Nome = cursoInput.Nome,
                Descricao = cursoInput.Descricao,
                Preco = cursoInput.Preco,
                DataDeCriacao = DateTime.UtcNow,
                CapaUrl= cursoInput.CapaUrl,
                DuracaoEmMinutos = cursoInput.DuracaoEmMinutos,
            };
            await _context.AdicionarCursoAsync(email, curso);
            return curso;
        }
    }
}
