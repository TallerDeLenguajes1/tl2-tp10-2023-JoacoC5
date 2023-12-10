using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;

    }

    [HttpGet]
    public IActionResult ListarTablero()
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                ViewTableroLista viewTablero = new ViewTableroLista(_tableroRepository.GetAllTablero(), _usuarioRepository.GetAllUsuario());
                return View(viewTablero);
            }
            else
            {
                ViewTableroLista viewTablero = new ViewTableroLista(_tableroRepository.GetAllTablero().FindAll(t => t.IdUsuarioPropietario == HttpContext.Session.GetInt32("Id")), _usuarioRepository.GetAllUsuario());
                return View(viewTablero);
            }
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(new ViewTableroAgregar());
        }
        else
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }

    [HttpPost]
    public IActionResult CrearTablero(ViewTableroAgregar viewtablero)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            var tablero = new Tablero(viewtablero);
            _tableroRepository.CreateTablero(tablero);
            return RedirectToAction("ListarTablero");
        }
        else
        {
            return View("Error");
        }
    }


    [HttpGet]
    public IActionResult ModificarTablero(int IdBuscado)
    {
        var tablero = _tableroRepository.GetTableroById(IdBuscado);

        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                ViewTableroUpdate viewTablero = new ViewTableroUpdate(_tableroRepository.GetTableroById(IdBuscado));
                return View(viewTablero);
            }
            else
            {
                if (HttpContext.Session.GetInt32("Id") == tablero.IdUsuarioPropietario)
                {
                    ViewTableroUpdate viewTablero = new ViewTableroUpdate(tablero);
                    return View("ModificarTableroOperador", viewTablero);
                }
            }
        }

        return RedirectToAction("ListarTablero");

    }

    [HttpPost]
    public IActionResult Modificartablero(int idBuscado, ViewTableroUpdate viewtablero)
    {
        var tableroAux = _tableroRepository.GetTableroById(idBuscado);

        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                Tablero tablero = new Tablero(viewtablero);
                _tableroRepository.UpdateTablero(tablero.Id, tablero);
                return RedirectToAction("ListarTablero");
            }
            else
            {
                if (HttpContext.Session.GetInt32("Id") == tableroAux.IdUsuarioPropietario)
                {
                    Tablero tablero = new Tablero(viewtablero);
                    _tableroRepository.UpdateTablero(idBuscado, tablero);
                    return RedirectToAction("ListarTablero");
                }
            }
        }

        return RedirectToAction("ListarTablero");
    }

    [HttpGet]
    public IActionResult EliminarTablero(int idBuscado)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(_tableroRepository.GetTableroById(idBuscado));
        }
        else
        {
            return View("Error");
        }

    }

    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            _tableroRepository.DeleteTablero(tablero.Id);
            return RedirectToAction("ListarTablero");
        }
        else
        {
            return View("Error");
        }

    }

    /*public IActionResult EliminarTablero(int idBuscado)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                tableroRepository.DeleteTablero(idBuscado);
            }
        }
        return RedirectToAction("ListarTablero");
    }*/

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}