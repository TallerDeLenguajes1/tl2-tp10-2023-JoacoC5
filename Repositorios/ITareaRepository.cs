using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public interface ITareaRepository
{
    public void CreateTarea(int idTablero, Tarea nuevo);
    public void UpdateTarea(int idBuscado, Tarea modificado);
    public Tarea GetTareaById(int idBuscado);
    public List<Tarea> GetAllTareaByUsuario(int idUsuario);
    public List<Tarea> GetAllTareaByTableros(int idTablero);
    //public void SetUsuario(int idusuario, int idTarea);
    public void DeleteTarea(int idBuscado);
    public List<Tarea> GetAllTareas();
    public void AnularTareas(int idBuscado);
    public void QuitarUsuario(int idBuscado);

}