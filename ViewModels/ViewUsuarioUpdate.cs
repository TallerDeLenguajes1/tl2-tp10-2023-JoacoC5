using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string NombreDeUsuario { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public Rol Rol { get; set; }
    [Required(ErrorMessage = "Campo requerido")]

    public string Contrasenia { get; set; }

    public ViewUsuarioUpdate()
    {
    }

    public ViewUsuarioUpdate(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
        Contrasenia = usuario.Contrasenia;
    }
}