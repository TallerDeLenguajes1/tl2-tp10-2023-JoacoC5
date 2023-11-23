using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

    public void CreateUsuario(Usuario usuario)
    {
        var query = $"INSERT INTO Usuario(nombre_de_usuario) VALUES(@name)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void UpdateUsuario(int idBuscado, Usuario modificado)
    {
        var query = $"UPDATE Usuario SET nombre_de_usuario =(@name) WHERE id_usuario =(@id);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@name", modificado.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@id", idBuscado));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Usuario> GetAllUsuario()
    {
        var query = @"SELECT * FROM Usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = Convert.ToInt32(reader["id_usuario"]);
                    aux.NombreDeUsuario = reader["nombre_de_usuario"].ToString();

                    usuarios.Add(aux);
                }
            }
            connection.Close();
        }

        return usuarios;

    }
    public Usuario GetUsuarioById(int idBuscado)
    {
        var query = @"SELECT * FROM Usuario WHERE id_usuario = @idBuscado;";
        Usuario aux = new Usuario();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    aux.Id = Convert.ToInt32(reader["id_usuario"]);
                    aux.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                }
            }
            connection.Close();
        }

        return aux;
    }
    public void DeleteUsuario(int idBuscado)
    {
        var query = @"DELETE * FROM usuario WHERE id_usuario = @idBuscado;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
