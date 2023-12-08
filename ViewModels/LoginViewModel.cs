using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre de Usuario")]
    public string NombreDeUsuario { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Contraseña")]
    [PasswordPropertyText]
    public string Contrasenia { get; set; }

    public LoginViewModel(Usuario usuario)
    {
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
    }

    public LoginViewModel()
    {

    }
}