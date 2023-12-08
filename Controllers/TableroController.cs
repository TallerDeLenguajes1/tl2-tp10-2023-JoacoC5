using System.Diagnostics;
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

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
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

    [HttpGet]
    public IActionResult EliminarTablero(int idBuscado)
    {
        return View(tableroRepository.GetTableroById(idBuscado));
    }

    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        tableroRepository.DeleteTablero(tablero.Id);
        return RedirectToAction("ListarTablero");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}