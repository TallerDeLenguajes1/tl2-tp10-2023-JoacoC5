using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }


    [HttpGet]
    public IActionResult ListarTarea(int idTablero)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                ViewTareaLista tareas = new ViewTareaLista(tareaRepository.GetAllTareas(), _tableroRepository.GetAllTablero(), _usuarioRepository.GetAllUsuario());
                return View(tareas);
            }
            else
            {
                ViewTareaLista tareas = new ViewTareaLista(tareaRepository.GetAllTareas().FindAll(t => t.IdUsuarioAsignado == HttpContext.Session.GetInt32("Id")), _tableroRepository.GetAllTablero(), _usuarioRepository.GetAllUsuario());
                return View(tareas);
            }
        }
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(new ViewTareaAgregar(new Tarea(), _tableroRepository.GetAllTablero(), _usuarioRepository.GetAllUsuario())); ;
        }
        else
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }

    [HttpPost]
    public IActionResult CrearTarea(ViewTareaAgregar viewTarea)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            var tarea = new Tarea(viewTarea);
            tareaRepository.CreateTarea(tarea.IdTablero, tarea);
            return RedirectToAction("ListarTarea");
        }
        else
        {
            return View("Error");
        }
    }


    [HttpGet]
    public IActionResult ModificarTarea(int IdBuscado)
    {
        var tarea = tareaRepository.GetTareaById(IdBuscado);
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                ViewTareaUpdate viewTarea = new ViewTareaUpdate(tareaRepository.GetTareaById(IdBuscado));
                return View(viewTarea);
            }
            else
            {
                if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado)
                {
                    ViewTareaUpdate viewTarea = new ViewTareaUpdate(tarea);
                    return View("ModificarTareaOperador", viewTarea);
                }
            }

            return RedirectToAction("ListarTarea");
        }
    }

    [HttpPost]
    public IActionResult ModificarTarea(int idBuscado, ViewTareaUpdate viewTarea)
    {
        var tareaAux = tareaRepository.GetTareaById(idBuscado);
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                Tarea tarea = new Tarea(viewTarea);
                tareaRepository.UpdateTarea(tarea.Id, tarea);
                return RedirectToAction("ListarTarea");
            }
            else
            {
                if (HttpContext.Session.GetInt32("Id") == tareaAux.Id)
                {
                    Tarea tarea = new Tarea(viewTarea);
                    tareaRepository.UpdateTarea(idBuscado, tarea);
                    return RedirectToAction("ListarTarea");
                }
            }

            return RedirectToAction("ListarTarea");
        }
    }

    [HttpGet]
    public IActionResult EliminarTarea(int idBuscado)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            return View(tareaRepository.GetTareaById(idBuscado));
        }
        else
        {
            return View("Error");
        }

    }

    [HttpPost]
    public IActionResult EliminarTarea(Tablero tablero)
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
        {
            tareaRepository.DeleteTarea(tablero.Id);
            return RedirectToAction("ListarTarea");
        }
        else
        {
            return View("Error");
        }


    }

    /*public IActionResult EliminarTarea(int idBuscado)
    {
        if (HttpContext.Session.GetString("Rol") == null)
        {
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        else
        {
            if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Administrador")
            {
                tareaRepository.DeleteTarea(idBuscado);
            }
        }
        return RedirectToAction("ListarTarea");
    }*/

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}