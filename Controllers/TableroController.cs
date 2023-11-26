using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_JoacoC5.Models;
using tl2_tp10_2023_JoacoC5.Repository;

namespace tl2_tp10_2023_JoacoC5.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        tableroRepository.CreateTablero(tablero);
        return RedirectToAction("ListarTablero");
    }

    [HttpGet]
    public IActionResult Listartablero()
    {
        return View(tableroRepository.GetAllTablero());
    }

    [HttpGet]
    public IActionResult ModificarTablero(int IdBuscado)
    {
        return View(tableroRepository.GetTableroById(IdBuscado));
    }

    [HttpPost]
    public IActionResult Modificartablero(Tablero tablero)
    {
        tableroRepository.UpdateTablero(tablero.Id, tablero);
        return RedirectToAction("ListarTablero");
    }

    //FALTA DELETE
}