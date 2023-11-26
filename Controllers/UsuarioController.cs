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
    public IActionResult Crearusuario()
    {
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        usuarioRepository.CreateUsuario(usuario);
        return RedirectToAction("ListarUsuario");
    }

    [HttpGet]
    public IActionResult ListarUsuario()
    {
        return View(usuarioRepository.GetAllUsuario());
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int IdBuscado)
    {
        return View(usuarioRepository.GetUsuarioById(IdBuscado));
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {
        usuarioRepository.UpdateUsuario(usuario.Id, usuario);
        return RedirectToAction("ListarUsuario");
    }

    [HttpGet]
    public IActionResult EliminarUsuario(int idBuscado)
    {
        return View(usuarioRepository.GetUsuarioById(idBuscado));
    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        usuarioRepository.DeleteUsuario(usuario.Id);
        return RedirectToAction("ListarUsuario");
    }
}