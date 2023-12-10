using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;
public class ViewTableroAgregar
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public int IdUsuarioPropietario { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Nombre { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

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