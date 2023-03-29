using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CursosWebApp.Controllers
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
        public  IActionResult Login()
        {
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

            bool loginValido = await _usuarioService.ValidarLoginAsync(loginInput);
            if (loginValido == false)
                return BadRequest();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, loginInput.Email),
                new Claim(ClaimTypes.Role, "Aluno")
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Redirect("/");
        }

        [HttpGet]
        [Authorize(Policy = "Aluno")]
        public async Task<IActionResult> ContaDeUsuario()
        {
            string? userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
                return Unauthorized();


            Usuario? usuario = await _usuarioService.ReceberUsuarioAsync(userEmail);
            if(usuario == null)
                return NotFound();

            return View(usuario); 
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
