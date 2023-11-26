using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_JoacoC5.Models;
using System.Data.SqlClient;

namespace tl2_tp10_2023_JoacoC5.Repository;

public class TableroRepository : ITableroRepository
{
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

    public void CreateTablero(Tablero nuevo)
    {
        var query = @"INSERT INTO Tablero(id_usuario_propietario, nombre, descripcion)
         VALUES(@idUsuarioPropietario, @nombre, @descripcion);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", nuevo.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", nuevo.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", nuevo.Descripcion));

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public void UpdateTablero(int idBuscado, Tablero modificado)
    {
        var query = $"UPDATE Tablero SET nombre=(@name), descripcion=(@descripcion) WHERE id_tablero = (@idBuscado);"; //REVISAR MAS ADELANTE
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));
            command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", modificado.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@name", modificado.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", modificado.Descripcion));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Tablero> GetAllTablero()
    {
        var query = @"SELECT * FROM Tablero;";
        List<Tablero> tableros = new List<Tablero>();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tablero aux = new Tablero();
                    aux.Id = Convert.ToInt32(reader["id_tablero"]);
                    aux.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Descripcion = reader["descripcion"].ToString();
                    tableros.Add(aux);
                }
            }
            connection.Close();
        }

        return tableros;
    }
    public List<Tablero> GetTableroByUsuario(int idUBuscado)
    {
        var query = @"SELECT * FROM Tablero WHERE id_usuario_propietario = @idUBuscado;";
        List<Tablero> tableros = new List<Tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idUsuarioPropietario", idUBuscado));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tablero aux = new Tablero();
                    aux.Id = Convert.ToInt32(reader["id_tablero"]);
                    aux.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Descripcion = reader["descripcion"].ToString();
                    tableros.Add(aux);
                }
            }
            connection.Close();
        }
        return tableros;
    }

    public Tablero GetTableroById(int idBuscado)
    {
        var query = @"SELECT * FROM Tablero WHERE id_tablero = @idBuscado;";
        Tablero tablero = new Tablero();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }

            connection.Close();
        }

        return tablero;


    }
    public void DeleteTablero(int idBuscado)
    {
        var query = @"DELETE * FROM Tablero WHERE id_tablero = @idBuscado;";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("id_tablero", idBuscado));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}