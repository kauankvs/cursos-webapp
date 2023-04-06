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
        public async Task<IActionResult> CriacaoDeCurso(CursoDTO cursoInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            };
            string userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            await _service.AdicionarCursoAsync(userEmail, cursoInput);
            return Redirect("/");
        }
    }
}
