using tl2_tp10_2023_JoacoC5.Models;
using System.ComponentModel.DataAnnotations;

public class ViewUsuarioAgregar
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre de usuario")]
    [StringLength(25, ErrorMessage = "25 caracteres máximos")]
    public string NombreDeUsuario { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Rol")]
    public Rol Rol { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Contraseña")]
    public string Contrasenia { get; set; }

    public ViewUsuarioAgregar()
    {
    }

    public ViewUsuarioAgregar(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
        Contrasenia = usuario.Contrasenia;
    }
}