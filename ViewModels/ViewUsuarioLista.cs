using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioLista
{
    private List<ViewUsuario> viewUsuarios;

    public List<ViewUsuario> ViewUsuarios { get => viewUsuarios; set => viewUsuarios = value; }

    public ViewUsuarioLista(List<Usuario> usuarios)
    {
        viewUsuarios = new List<ViewUsuario>();
        foreach (var u in usuarios)
        {
            var viewUsuario = new ViewUsuario(u);
            viewUsuarios.Add(viewUsuario);
        }
    }
}