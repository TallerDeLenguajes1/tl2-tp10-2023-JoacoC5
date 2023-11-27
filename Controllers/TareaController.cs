using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    [HttpGet]
    public IActionResult CrearTarea()
    {
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult CrearTarea(Tarea tarea)
    {
        tareaRepository.CreateTarea(1, tarea);
        return RedirectToAction("ListarTarea");
    }

    [HttpGet]
    public IActionResult ListarTarea(int idTablero)
    {
        idTablero = 1;
        return View(tareaRepository.GetAllTareaByTablero(idTablero));
    }

    [HttpGet]
    public IActionResult ModificarTarea(int IdBuscado)
    {
        return View(tareaRepository.GetTareaById(IdBuscado));
    }

    [HttpPost]
    public IActionResult ModificarTarea(Tarea Tarea)
    {
        tareaRepository.UpdateTarea(Tarea.Id, Tarea);
        return RedirectToAction("ListarTarea");
    }

    [HttpGet]
    public IActionResult EliminarTarea(int idBuscado)
    {
        return View(tareaRepository.GetTareaById(idBuscado));
    }

    [HttpPost]
    public IActionResult EliminarTarea(Tablero tablero)
    {
        tareaRepository.DeleteTarea(tablero.Id);
        return RedirectToAction("ListarTarea");
    }
}