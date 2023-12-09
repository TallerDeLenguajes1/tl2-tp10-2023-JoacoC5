using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTableroLista
{
    private List<ViewTablero> viewTableros;

    public List<ViewTablero> ViewTableros { get => viewTableros; set => viewTableros = value; }

    public ViewTableroLista(List<Tablero> tableros, List<Usuario> usuarios)
    {
        viewTableros = new List<ViewTablero>();
        foreach (var t in tableros)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioPropietario == u.Id)
                {
                    var viewTablero = new ViewTablero(t);
                    viewTableros.Add(viewTablero);
                }
            }
        }
    }
}