using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public int IdTablero { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Nombre { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Descripcion { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Color { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public EstadoTarea Estado { get; set; }

    public int? IdUsuarioAsignado { get; set; }

    public List<Tablero> Tableros { get; set; }
    public List<Usuario> Usuarios { get; set; }

    public ViewTareaUpdate()
    {

    }

    public ViewTareaUpdate(Tarea tarea)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}