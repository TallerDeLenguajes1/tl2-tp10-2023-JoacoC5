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

    private List<Tablero> tableros;
    public List<Tablero> Tableros { get => tableros; set => tableros = value; }
    private List<Usuario> usuarios;
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public ViewTareaUpdate()
    {

    }

    public ViewTareaUpdate(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios)
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