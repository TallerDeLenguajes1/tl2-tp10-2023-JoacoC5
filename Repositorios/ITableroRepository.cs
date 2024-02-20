using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public interface ITableroRepository
{
    public void CreateTablero(Tablero nuevo);
    public void UpdateTablero(int idBuscado, Tablero modificado);
    public List<Tablero> GetAllTablero();
    public List<Tablero> GetTableroByUsuario(int idUBuscado);
    public Tablero GetTableroById(int idBuscado);
    public void DeleteTablero(int idBuscado);
    public List<Tablero> GetTableroByTarea(int idBuscado);

}