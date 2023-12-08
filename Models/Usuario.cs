using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tl2_tp10_2023_JoacoC5.Models;

public enum Rol
{
    Administrador,
    Operador
}

public class Usuario
{
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private Rol rol;


    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public Rol Rol { get => rol; set => rol = value; }

    public Usuario()
    {

    }

    public Usuario(ViewUsuarioAgregar viewUsuario)
    {
        id = viewUsuario.Id;
        nombreDeUsuario = viewUsuario.NombreDeUsuario;
        rol = viewUsuario.Rol;
        contrasenia = viewUsuario.Contrasenia;
    }

    public Usuario(ViewUsuarioUpdate viewUsuario)
    {
        id = viewUsuario.Id;
        nombreDeUsuario = viewUsuario.NombreDeUsuario;
        rol = viewUsuario.Rol;
        contrasenia = viewUsuario.Contrasenia;
    }
}