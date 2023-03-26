using Microsoft.AspNetCore.Mvc;

namespace CursosWebApp.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult FormularioDeRegistro()
        {
            return View();
        }
    }
}
