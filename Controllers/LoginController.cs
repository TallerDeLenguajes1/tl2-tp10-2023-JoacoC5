using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;
using tl2_tp10_2023_JoacoC5.ViewModels;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository usuarioRepository;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    public IActionResult Login(Usuario usuario)
    {

        Usuario uLoggeado = usuarioRepository.GetAllUsuario().FirstOrDefault
        (u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia == usuario.Contrasenia);
        if (uLoggeado == null)
        {
            return RedirectToAction("Error");
        }
        else
        {
            LoggearUsuario(uLoggeado);
            return RedirectToRoute(new { controller = "Usuario", action = "ListarUsuario" });
        }
    }

    private void LoggearUsuario(Usuario uLog)
    {
        HttpContext.Session.SetString("NombreDeusuario", uLog.NombreDeUsuario);
        HttpContext.Session.SetString("Rol", (uLog.Rol).ToString());
        HttpContext.Session.SetInt32("Id", uLog.Id);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}