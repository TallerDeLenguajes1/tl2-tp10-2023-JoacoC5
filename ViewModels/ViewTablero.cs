using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTablero
{
    public int Id { get; set; }
    public int IdUsuarioPropietario { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public ViewTablero()
    {

    }

    public ViewTablero(Tablero tablero)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }

}