using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;
public class ViewTableroAgregar
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id usuario propietario")]
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre tablero")]
    [StringLength(30, ErrorMessage = "30 caracateres máximos")]
    public string Nombre { get; set; }

    [StringLength(100, ErrorMessage = "100 caracateres máximos")]
    public string Descripcion { get; set; }

    //public List<Usuario> Usuarios { get; set; }

    public ViewTableroAgregar()
    {

    }

    public ViewTableroAgregar(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }

}