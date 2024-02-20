/*using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaUpdateOperador
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre tarea")]
    [StringLength(30, ErrorMessage = "30 caracteres máximos")]
    public string Nombre { get; set; }

    [Display(Name = "Id Usuario asignado")]
    public int IdUsuarioAsignado { get; set; }

    public List<Usuario> Usuarios;

    public ViewTareaUpdateOperador()
    {

    }

    public ViewTareaUpdateOperador(Tarea tarea, List<Usuario> usuarios)
    {
        Id = tarea.Id;
        //IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        //Descripcion = tarea.Descripcion;
        //Color = tarea.Color;
        //Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}*/