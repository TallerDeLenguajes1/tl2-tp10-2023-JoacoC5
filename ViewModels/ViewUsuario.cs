using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuario
{
    private int id;
    private string nombreDeUsuario;
    private Rol rol;
    private string contrasenia;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public Rol Rol { get => rol; set => rol = value; }
    //public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public ViewUsuario(Usuario u)
    {
        id = u.Id;
        nombreDeUsuario = u.NombreDeUsuario;
        rol = u.Rol;
        //contrasenia = us.Contrasenia;
    }

}