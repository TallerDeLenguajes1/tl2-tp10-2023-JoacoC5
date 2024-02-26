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
                    ViewTableroLista viewTablero = new ViewTableroLista(_tableroRepository.GetAllTablero(), _usuarioRepository.GetAllUsuario());
                    return View(viewTablero);
                }
                else
                {
                    ViewTableroLista viewTablero = new ViewTableroLista(_tableroRepository.GetTableroByUsuario(HttpContext.Session.GetInt32("Id").GetValueOrDefault()), _tableroRepository.GetTableroByTarea(HttpContext.Session.GetInt32("Id").GetValueOrDefault()), _usuarioRepository.GetAllUsuario());
                    return View("ListarTableroOperador", viewTablero);
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
    public IActionResult CrearTablero()
    {
        try
        {
            if (isAdmin())
            {
                return View(new ViewTableroAgregar(_usuarioRepository.GetAllUsuario()));
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
    public IActionResult CrearTablero(ViewTableroAgregar viewtablero)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (isAdmin())
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
            else
            {
                return RedirectToAction("CrearTablero");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult ModificarTablero(int IdBuscado)
    {
        try
        {
            var tablero = _tableroRepository.GetTableroById(IdBuscado);

            if (HttpContext.Session.GetString("Rol") == null)
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            else
            {
                if (isAdmin())
                {
                    ViewTableroUpdate viewTablero = new ViewTableroUpdate(_tableroRepository.GetTableroById(IdBuscado));
                    viewTablero.Usuarios = _usuarioRepository.GetAllUsuario();
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

            return RedirectToAction("Error");
            //Se puede cambiar
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }


    }

    [HttpPost]
    public IActionResult Modificartablero(int idBuscado, ViewTableroUpdate viewtablero)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var tableroAux = _tableroRepository.GetTableroById(idBuscado);

                if (HttpContext.Session.GetString("Rol") == null)
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                else
                {
                    if (isAdmin())
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
                            //No tiene efecto
                        }
                    }
                }

                return RedirectToAction("ListarTablero");
            }
            else
            {
                return RedirectToAction("ModificarTablero");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }


    }

    [HttpGet]
    public IActionResult EliminarTablero(int idBuscado)
    {
        try
        {
            if (isAdmin())
            {
                ViewTablero viewTablero = new ViewTablero(_tableroRepository.GetTableroById(idBuscado));
                return View(viewTablero);
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
    public IActionResult EliminarTablero(ViewTablero tablero)
    {
        try
        {
            if (isAdmin())
            {
                _tableroRepository.DeleteTablero(tablero.Id);
                return RedirectToAction("ListarTablero");
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