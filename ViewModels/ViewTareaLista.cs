using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTareaLista
{
    private List<ViewTarea> viewTareas;
    public List<ViewTarea> ViewTareas { get => viewTareas; set => viewTareas = value; }
    private List<ViewTarea> viewTareasNoAsignadas;
    public List<ViewTarea> ViewTareasNoAsignadas { get => viewTareasNoAsignadas; set => viewTareasNoAsignadas = value; }


    public ViewTareaLista(List<Tarea> tareas, List<Tablero> tableros, List<Usuario> usuarios)
    {
        viewTareas = new List<ViewTarea>();
        foreach (var t in tareas)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioAsignado == u.Id)
                {
                    Tablero auxTablero = tableros.Find(x => x.Id == t.IdTablero);
                    var viewTarea = new ViewTarea(t);
                    viewTarea.NombreUsuario = u.NombreDeUsuario;
                    viewTarea.NombreTablero = auxTablero.Nombre;
                    /*if (t.IdUsuarioAsignado == 0)
                    {
                        viewTarea.NombreUsuario = "";
                    }*/
                    viewTareas.Add(viewTarea);
                }
            }

        }
    }

    public ViewTareaLista(List<Tarea> tareas, List<Tarea> tareaNoAsignadas, List<Tablero> tableros, List<Usuario> usuarios)
    {
        viewTareas = new List<ViewTarea>();
        foreach (var t in tareas)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioAsignado == u.Id)
                {
                    Tablero auxTablero = tableros.Find(x => x.Id == t.IdTablero);
                    var viewTarea = new ViewTarea(t);
                    viewTarea.NombreUsuario = u.NombreDeUsuario;
                    viewTarea.NombreTablero = auxTablero.Nombre;
                    viewTareas.Add(viewTarea);
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
                    Tablero auxTablero = tableros.Find(x => x.Id == t.IdTablero);
                    var viewTarea = new ViewTarea(t);
                    viewTarea.NombreUsuario = u.NombreDeUsuario;
                    viewTarea.NombreTablero = auxTablero.Nombre;
                    viewTareasNoAsignadas.Add(viewTarea);
                }
            }
        }
    }
}