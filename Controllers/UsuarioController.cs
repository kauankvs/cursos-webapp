using CursosWebApp.Models;
using CursosWebApp.Services.Interfaces;
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
            bool loginValido = await _usuarioService.ValidarLoginAsync(loginInput);
            if (loginValido == false)
                return BadRequest();

            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.UtcNow.AddHours(8),
                HttpOnly = true,
                Secure = true,
            };
            Response.Cookies.Append("User", loginInput.Email, options);
            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> ContaDeUsuario()
        {
            string? cookieUsuario = Request.Cookies["User"];
            if(cookieUsuario == null)
                return Unauthorized();

            Usuario? usuario = await _usuarioService.ReceberUsuarioAsync(cookieUsuario);
            if(usuario == null)
                return NotFound();

            return View(usuario); 
        }
    }
}
