using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
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
                ViewUsuarioLista usuarios = new ViewUsuarioLista(_usuarioRepository.GetAllUsuario());
                return View(usuarios);
            }
            else
            {
                ViewUsuarioLista usuarios = new ViewUsuarioLista(_usuarioRepository.GetAllUsuario().FindAll(u => u.Id == HttpContext.Session.GetInt32("Id")));

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
            _usuarioRepository.CreateUsuario(nuevo);
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
                var update = new ViewUsuarioUpdate(_usuarioRepository.GetUsuarioById(idBuscado));
                return View(update);
            }
            else
            {
                if (HttpContext.Session.GetInt32("Id") == idBuscado)
                {
                    var update = new ViewUsuarioUpdate(_usuarioRepository.GetUsuarioById(idBuscado));
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
                _usuarioRepository.UpdateUsuario(update.Id, update);
            }
            return RedirectToAction("ListarUsuario");
        }

    }

    [HttpGet]
    public IActionResult EliminarUsuario(int idBuscado)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(_usuarioRepository.GetUsuarioById(idBuscado));
        }
        else
        {
            return View("Error");
        }


    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            _usuarioRepository.DeleteUsuario(usuario.Id);
            return RedirectToAction("ListarUsuario");
        }
        else
        {
            return View("Error");
        }

    }

    /*public IActionResult EliminarUsuario(int idBuscado)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                usuarioRepository.DeleteUsuario(idBuscado);
            }
        }
        return RedirectToAction("ListarUsuario");
    }*/

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}