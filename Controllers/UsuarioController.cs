using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public IActionResult ListarUsuario()
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                ViewUsuarioLista usuarios = new ViewUsuarioLista(usuarioRepository.GetAllUsuario());
                return View(usuarios);
            }
            else
            {
                ViewUsuarioLista usuarios = new ViewUsuarioLista(usuarioRepository.GetAllUsuario().FindAll(u => u.Id == HttpContext.Session.GetInt32("id")));

                return View(usuarios);
            }
        }
    }

    [HttpGet]
    public IActionResult Crearusuario()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(new ViewUsuarioAgregar());
        }
        else
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }

    [HttpPost]
    public IActionResult CrearUsuario(ViewUsuarioAgregar viewUsuario)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            var nuevo = new Usuario(viewUsuario);
            usuarioRepository.CreateUsuario(nuevo);
            return RedirectToAction("ListarUsuario");
        }
        else
        {
            return View("Index");
        }
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idBuscado)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                var update = new ViewUsuarioUpdate(usuarioRepository.GetUsuarioById(idBuscado));
                return View(update);
            }
            else
            {
                if (HttpContext.Session.GetInt32("id") == idBuscado)
                {
                    var update = new ViewUsuarioUpdate(usuarioRepository.GetUsuarioById(idBuscado));
                    return View("ModificarUsuarioOperador", update);
                }
                else
                {
                    return RedirectToAction("ListarUsuario");
                }
            }
        }
    }

    [HttpPost]
    public IActionResult ModificarUsuario(ViewUsuarioUpdate usuario)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                var update = new Usuario(usuario);
                usuarioRepository.UpdateUsuario(update.Id, update);
            }
            return RedirectToAction("ListarUsuario");
        }

    }

    /*[HttpGet]
    public IActionResult EliminarUsuario(int idBuscado)
    {
        return View(usuarioRepository.GetUsuarioById(idBuscado));
    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        usuarioRepository.DeleteUsuario(usuario.Id);
        return RedirectToAction("ListarUsuario");
    }*/

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}