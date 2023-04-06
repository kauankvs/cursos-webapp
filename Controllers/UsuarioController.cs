using Mawe.Models;
using Mawe.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Mawe.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registrar()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar(UsuarioDTO usuarioInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Usuario? resultadoDaCriacaoDaConta = await _usuarioService.CriarUsuarioAsync(usuarioInput);
            if(resultadoDaCriacaoDaConta == null)
                return BadRequest();

            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            bool userNotLogged = HttpContext.User.FindFirstValue(ClaimTypes.Email).IsNullOrEmpty();
            ViewBag.UsuarioLogado = !userNotLogged;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Usuario? usuario = await _usuarioService.ValidarLoginDeUsuarioAsync(loginInput);
            if (usuario == null)
                return BadRequest();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Papel)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Redirect("/");
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ContaDeUsuario()
        {
            string? userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                ViewBag.UsuarioLogado = false;
                return Unauthorized();
            }

            Usuario? usuario = await _usuarioService.ReceberUsuarioAsync(userEmail);
            ViewBag.UsuarioLogado = true;

            if(usuario.Papel == "Aluno")
                return View("ContaDeAluno", usuario);

            if (usuario.Papel == "Tutor")
                return View("ContaDeTutor", usuario);

            return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
