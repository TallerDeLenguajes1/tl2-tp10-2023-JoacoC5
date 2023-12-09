using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_JoacoC5.Models;

public class Tablero
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero()
    {

    }

    public Tablero(ViewTableroAgregar viewTablero)
    {
        id = viewTablero.Id;
        idUsuarioPropietario = viewTablero.IdUsuarioPropietario;
        nombre = viewTablero.Nombre;
        descripcion = viewTablero.Descripcion;
    }

    public Tablero(ViewTableroUpdate viewTablero)
    {
        id = viewTablero.Id;
        idUsuarioPropietario = viewTablero.IdUsuarioPropietario;
        nombre = viewTablero.Nombre;
        descripcion = viewTablero.Descripcion;
    }
}