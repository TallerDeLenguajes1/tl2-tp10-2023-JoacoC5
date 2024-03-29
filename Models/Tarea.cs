using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_JoacoC5.Models;

public enum EstadoTarea
{
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
};

public class Tarea
{
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }

    public Tarea()
    {

    }

    public Tarea(ViewTareaAgregar viewTarea)
    {
        Id = viewTarea.Id;
        IdTablero = viewTarea.IdTablero;
        Nombre = viewTarea.Nombre;
        Descripcion = viewTarea.Descripcion;
        Color = viewTarea.Color;
        Estado = viewTarea.Estado;
        IdUsuarioAsignado = viewTarea.IdUsuarioAsignado;
    }

    public Tarea(ViewTareaUpdate viewTarea)
    {
        Id = viewTarea.Id;
        IdTablero = viewTarea.IdTablero;
        Nombre = viewTarea.Nombre;
        Descripcion = viewTarea.Descripcion;
        Color = viewTarea.Color;
        Estado = viewTarea.Estado;
        IdUsuarioAsignado = viewTarea.IdUsuarioAsignado;
    }

}