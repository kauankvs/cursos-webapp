using Mawe.Models;
using Mawe.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace Mawe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICursoService _cursoService;

        public HomeController(ILogger<HomeController> logger, ICursoService cursoService)
        {
            _logger = logger; 
            _cursoService = cursoService;
        }

        public async Task<IActionResult> Index()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            List<Curso> cursos = await _cursoService.SelecionarTodosCursosAsync();
            return View(cursos);
        }

        public IActionResult Privacy()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}