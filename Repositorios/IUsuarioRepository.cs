using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public interface IUsuarioRepository
{
    public void CreateUsuario(Usuario nuevo);
    public void UpdateUsuario(int idBuscado, Usuario modificado);
    public List<Usuario> GetAllUsuario();
    public Usuario GetUsuarioById(int idBuscado);
    public void DeleteUsuario(int idBuscado);
}