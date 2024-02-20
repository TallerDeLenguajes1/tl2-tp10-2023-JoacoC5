using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _tareaRepository;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository, ITareaRepository tareaRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }


    [HttpGet]
    public IActionResult ListarTarea()
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
                    ViewTareaLista tareas = new ViewTareaLista(_tareaRepository.GetAllTareas(), _usuarioRepository.GetAllUsuario());
                    return View(tareas);
                }
                else
                {
                    ViewTareaLista tareas = new ViewTareaLista(_tareaRepository.GetAllTareaByUsuario(HttpContext.Session.GetInt32("Id").GetValueOrDefault()), _tareaRepository.GetAllTareaByUsuario(0), _usuarioRepository.GetAllUsuario());
                    return View("ListarTareaOperador", tareas);
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
    public IActionResult CrearTarea()
    {
        try
        {
            if (isAdmin())
            {
                return View(new ViewTareaAgregar());
            }
            else
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult CrearTarea(ViewTareaAgregar viewTarea)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (isAdmin())
                {
                    var tarea = new Tarea(viewTarea);
                    _tareaRepository.CreateTarea(tarea.IdTablero, tarea);
                    return RedirectToAction("ListarTarea");
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("CrearTarea");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult ModificarTarea(int IdBuscado)
    {
        try
        {
            var tarea = _tareaRepository.GetTareaById(IdBuscado);
            var tablero = _tableroRepository.GetTableroById(tarea.IdTablero);

            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if (isAdmin())
                {
                    ViewTareaUpdate viewTarea = new ViewTareaUpdate(tarea);
                    return View(viewTarea);
                }
                else
                {
                    if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado && HttpContext.Session.GetInt32("Id") == tablero.IdUsuarioPropietario)
                    {
                        ViewTareaUpdate viewTarea = new ViewTareaUpdate(tarea);
                        return View("ModificarTareaOperador", viewTarea);
                    }
                    else
                    {
                        if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado)
                        {
                            ViewTareaUpdate viewTarea = new ViewTareaUpdate(tarea);
                            return View("ModificarEstadoTarea", viewTarea);
                        }
                        else
                        {
                            ViewTareaUpdate viewTarea = new ViewTareaUpdate(tarea);
                            return View("AsignarTarea", viewTarea);
                        }

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
    public IActionResult ModificarTarea(int idBuscado, ViewTareaUpdate viewTarea)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var tareaAux = _tareaRepository.GetTareaById(idBuscado);
                if (HttpContext.Session.GetString("Rol") == null)
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                else
                {
                    if (isAdmin())
                    {
                        Tarea tarea = new Tarea(viewTarea);
                        _tareaRepository.UpdateTarea(tarea.Id, tarea);
                        return RedirectToAction("ListarTarea");
                    }
                    else
                    {
                        if (HttpContext.Session.GetInt32("Id") == tareaAux.IdUsuarioAsignado || tareaAux.IdUsuarioAsignado == 0)
                        {
                            Tarea tarea = new Tarea(viewTarea);
                            _tareaRepository.UpdateTarea(idBuscado, tarea);
                            return RedirectToAction("ListarTarea");
                        }
                    }

                    return RedirectToAction("ListarTarea");
                }
            }
            else
            {
                return RedirectToAction("ModificarTarea");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpPost]
    public IActionResult ModificarTareaOperador(int idBuscado, ViewTareaUpdate viewTarea)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var tareaAux = _tareaRepository.GetTareaById(idBuscado);
                if (HttpContext.Session.GetString("Rol") == null)
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                else
                {
                    if (isAdmin())
                    {
                        Tarea tarea = new Tarea(viewTarea);
                        _tareaRepository.UpdateTarea(tarea.Id, tarea);
                        return RedirectToAction("ListarTarea");
                    }
                    else
                    {
                        if (HttpContext.Session.GetInt32("Id") == tareaAux.IdUsuarioAsignado || tareaAux.IdUsuarioAsignado == 0)
                        {
                            Tarea tarea = new Tarea(viewTarea);
                            _tareaRepository.UpdateTarea(idBuscado, tarea);
                            return RedirectToAction("ListarTarea");
                        }
                    }

                    return RedirectToAction("ListarTarea");
                }
            }
            else
            {
                return RedirectToAction("ModificarTarea");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult EliminarTarea(int idBuscado)
    {
        try
        {
            if (isAdmin())
            {
                return View(_tareaRepository.GetTareaById(idBuscado));
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
    public IActionResult EliminarTarea(Tablero tablero)
    {
        try
        {
            if (isAdmin())
            {
                _tareaRepository.DeleteTarea(tablero.Id);
                return RedirectToAction("ListarTarea");
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