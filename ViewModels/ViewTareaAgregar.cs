using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_JoacoC5.Repository;

public class ViewTareaAgregar
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id tablero")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre tarea")]
    [StringLength(30, ErrorMessage = "30 caracteres máximos")]
    public string Nombre { get; set; }

    [StringLength(100, ErrorMessage = "100 caracteres máximos")]
    [Display(Name = "Descripcion")]
    public string Descripcion { get; set; }

    [Display(Name = "Color")]
    public string Color { get; set; }

    [Display(Name = "Estado")]
    public EstadoTarea Estado { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id usuario asignado")]
    public int IdUsuarioAsignado { get; set; }
    public List<Tablero>? Tableros { get; set; }
    public List<Usuario>? Usuarios { get; set; }

    public ViewTareaAgregar()
    {

    }

    public ViewTareaAgregar(List<Tablero> tableros, List<Usuario> usuarios)
    {
        Tableros = tableros;
        Usuarios = usuarios;
    }

    public ViewTareaAgregar(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;

        Tableros = tableros;
        Usuarios = usuarios;

    }
}