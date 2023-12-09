using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTableroUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public int IdUsuarioPropietario { get; set; }
    [Required(ErrorMessage = "Campo requerido")]
    public string Nombre { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Descripcion { get; set; }

    public List<Usuario> Usuarios { get; set; }

    public ViewTableroUpdate()
    {

    }

    public ViewTableroUpdate(Tablero tablero, List<Usuario> usuarios)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        Usuarios = usuarios;
    }
}