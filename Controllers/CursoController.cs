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
        public IActionResult Criacao()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Tutor")]
        public async Task<IActionResult> Criacao(CursoDTO cursoInput)
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            };
            string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _service.AdicionarCursoTutorAsync(userEmail, cursoInput);
            return Redirect("/");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Cursos()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            List<Curso> cursos = await _service.SelecionarTodosCursosAsync();
            return View(cursos);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Curso/Categoria/{categoria}")]
        public async Task<IActionResult> Categoria(string categoria)
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
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

        [HttpGet]
        [AllowAnonymous]
        [Route("Curso/Detalhe/{nomeDoCurso}")]
        public async Task<IActionResult> Detalhes(string nomeDoCurso)
        {
            Curso? curso = await _service.SelecionarCursoPorNomeAsync(nomeDoCurso);
            if (curso == null)
                return NotFound();

            return View(curso);
        }

        [HttpPost]
        [Authorize(Policy = "Aluno")]
        [Route("Curso/Inscrever/{nomeDoCurso}")]
        public async Task<IActionResult> Inscrever(string nomeDoCurso)
        {
            string? userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                ViewBag.UsuarioLogado = false;
                return Unauthorized();
            }
            await _service.AdicionarCursoAlunoAsync(userEmail, nomeDoCurso);
            return View("Conta");
        }
    }
}
