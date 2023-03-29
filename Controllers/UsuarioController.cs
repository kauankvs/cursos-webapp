using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursosWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
            => _usuarioService = usuarioService;

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
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
        public  IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool loginValido = await _usuarioService.ValidarLoginAsync(loginInput);
            if (loginValido == false)
                return BadRequest();

            HttpContext.Session.SetString("Usuario", loginInput.Email);
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> ContaDeUsuario()
        {
            string? userEmail = HttpContext.Session.GetString("Usuario");
            if (userEmail == null)
                return Unauthorized();


            Usuario? usuario = await _usuarioService.ReceberUsuarioAsync(userEmail);
            if(usuario == null)
                return NotFound();

            return View(usuario); 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
