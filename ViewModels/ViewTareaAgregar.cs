using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaAgregar
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id tarea")]
    public int IdTablero { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre tablero")]
    [StringLength(30, ErrorMessage = "30 caracteres máximos")]
    public string Nombre { get; set; }

    [StringLength(100, ErrorMessage = "100 caracteres máximos")]
    public string Descripcion { get; set; }

    public string Color { get; set; }

    public EstadoTarea Estado { get; set; }

    public int IdUsuarioAsignado { get; set; }

    private List<Tablero> tableros;
    public List<Tablero> Tableros { get => tableros; set => tableros = value; }
    private List<Usuario> usuarios;
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public ViewTareaAgregar()
    {

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

        this.Tableros = tableros;
        this.Usuarios = usuarios;

    }
}