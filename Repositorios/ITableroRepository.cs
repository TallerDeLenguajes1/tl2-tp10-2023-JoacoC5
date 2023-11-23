using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public interface ITableroRepository
{
    public void CreateTablero(Tablero nuevo); //REVISAR
    public void UpdateTablero(int idBuscado, Tablero modificado);
    public List<Tablero> GetAllTablero();
    public List<Tablero> GetTableroByUsuario(int idUBuscado);
    public void DeleteTablero(int idBuscado);
}