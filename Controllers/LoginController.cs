using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;
using tl2_tp10_2023_JoacoC5.ViewModels;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    public IActionResult Login(Usuario usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Usuario uLoggeado = _usuarioRepository.GetAllUsuario().FirstOrDefault
                (u => u.NombreDeUsuario == usuario.NombreDeUsuario && u.Contrasenia == usuario.Contrasenia);
                if (uLoggeado == null)
                {
                    _logger.LogWarning("Intento de acceso invalido - Usuario o Contraseña incorrectos");
                    return RedirectToAction("Index");
                }
                else
                {
                    LoggearUsuario(uLoggeado);
                    _logger.LogInformation("El usuario " + uLoggeado.NombreDeUsuario + " ingreso correctamente");
                    return RedirectToRoute(new { controller = "Usuario", action = "ListarUsuario" });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al intentar ingresar: " + ex);
            return RedirectToAction("Index");
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