using Mawe.Models;
using Mawe.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Mawe.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoService _service;
        public CursoController(ICursoService service)
            => _service = service;

        [HttpGet]
        [Authorize(Policy = "Tutor")]
        public IActionResult CriacaoDeCurso()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Tutor")]
        public async Task<IActionResult> Criacao(CursoDTO cursoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            };
            string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _service.AdicionarCursoAsync(userEmail, cursoInput);
            return Redirect("/");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Cursos()
        {
            List<Curso> cursos = await _service.SelecionarTodosCursosAsync();
            return View(cursos);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Curso/Categoria/{categoria}")]
        public async Task<IActionResult> Categoria(string categoria)
        {
            List<Curso> cursoCategoria = new();
            List<Curso> cursos = await _service.SelecionarTodosCursosAsync();
            foreach(Curso curso in cursos) 
            {
                if (curso.Categoria.ToString() == categoria)
                    cursoCategoria.Add(curso);
            }
            ViewData["Title"] = categoria;
            return View(cursoCategoria);
        }
    }
}
