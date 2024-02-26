using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewTableroLista
{
    private List<ViewTablero> viewTableros;
    public List<ViewTablero> ViewTableros { get => viewTableros; set => viewTableros = value; }

    private List<ViewTablero> viewTablerosAjenos;
    public List<ViewTablero> ViewTablerosAjenos { get => viewTablerosAjenos; set => viewTablerosAjenos = value; }

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
                    viewTablero.NombrePropietario = u.NombreDeUsuario;
                    viewTableros.Add(viewTablero);
                }
            }
        }
    }

    public ViewTableroLista(List<Tablero> tableros, List<Tablero> tablerosAjenos, List<Usuario> usuarios)
    {
        viewTableros = new List<ViewTablero>();
        foreach (var t in tableros)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioPropietario == u.Id)
                {
                    var viewTablero = new ViewTablero(t);
                    viewTablero.NombrePropietario = u.NombreDeUsuario;
                    viewTableros.Add(viewTablero);
                }
            }
        }
        viewTablerosAjenos = new List<ViewTablero>();
        foreach (var t in tablerosAjenos)
        {
            foreach (var u in usuarios)
            {
                if (t.IdUsuarioPropietario == u.Id)
                {
                    var viewTablero = new ViewTablero(t);
                    viewTablero.NombrePropietario = u.NombreDeUsuario;
                    viewTablerosAjenos.Add(viewTablero);
                }
            }
        }
    }
}