using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaLista
{
    private List<ViewTarea> viewTareas;
    public List<ViewTarea> ViewTareas { get => viewTareas; set => viewTareas = value; }
    private List<ViewTarea> viewTareasNoAsignadas;
    public List<ViewTarea> ViewTareasNoAsignadas { get => viewTareasNoAsignadas; set => viewTareasNoAsignadas = value; }


    public ViewTareaLista(List<Tarea> tareas/*, List<Tablero> tableros*/, List<Usuario> usuarios)
    {
        viewTareas = new List<ViewTarea>();
        foreach (var t in tareas)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioAsignado == u.Id)
                {
                    viewTareas.Add(new ViewTarea(t));
                }
            }

        }
    }

    public ViewTareaLista(List<Tarea> tareas, List<Tarea> tareaNoAsignadas/*, List<Tablero> tableros*/, List<Usuario> usuarios)
    {
        viewTareas = new List<ViewTarea>();
        foreach (var t in tareas)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioAsignado == u.Id)
                {
                    viewTareas.Add(new ViewTarea(t));
                }
            }

        }

        viewTareasNoAsignadas = new List<ViewTarea>();
        foreach (var t in tareaNoAsignadas)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioAsignado == u.Id)
                {
                    viewTareasNoAsignadas.Add(new ViewTarea(t));
                }
            }
        }
    }
}