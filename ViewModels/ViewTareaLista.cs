using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaLista
{
    private List<ViewTarea> viewTareas;

    public List<ViewTarea> ViewTareas { get => viewTareas; set => viewTareas = value; }

    public ViewTareaLista(List<Tarea> tareas, List<Tablero> tableros, List<Usuario> usuarios)
    {
        viewTareas = new List<ViewTarea>();
        foreach (var ta in tareas)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == ta.IdUsuarioAsignado);
            var tablero = tableros.FirstOrDefault(t => t.Id == ta.IdTablero);
            viewTareas.Add(new ViewTarea(ta, usuario, tablero));
        }
    }
}