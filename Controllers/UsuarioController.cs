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
        try
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if (isAdmin())
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
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult Crearusuario()
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if (isAdmin())
                {
                    return View(new ViewUsuarioAgregar());
                }
                else
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
            }
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpPost]
    public IActionResult CrearUsuario(ViewUsuarioAgregar viewUsuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (isAdmin())
                {
                    var nuevo = new Usuario(viewUsuario);
                    _usuarioRepository.CreateUsuario(nuevo);
                    return RedirectToAction("ListarUsuario");
                }
                else
                {
                    return RedirectToAction("ListarUsuario");
                }
            }
            else
            {
                return RedirectToAction("CrearUsuario");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idBuscado)
    {
        try
        {
            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if (isAdmin())
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
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpPost]
    public IActionResult ModificarUsuario(ViewUsuarioUpdate usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("Rol") == null)
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                else
                {
                    if (isAdmin())
                    {
                        var update = new Usuario(usuario);
                        _usuarioRepository.UpdateUsuario(update.Id, update);
                    }
                    return RedirectToAction("ListarUsuario");
                }
            }
            else
            {
                return RedirectToAction("ModificarUsuario");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }


    }

    [HttpGet]
    public IActionResult EliminarUsuario(int idBuscado)
    {
        try
        {
            if (isAdmin())
            {
                return View(_usuarioRepository.GetUsuarioById(idBuscado));
            }
            else
            {
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }



    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        try
        {
            if (isAdmin())
            {
                _usuarioRepository.DeleteUsuario(usuario.Id);
                return RedirectToAction("ListarUsuario");
            }
            else
            {
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }


    }

    private bool isAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}