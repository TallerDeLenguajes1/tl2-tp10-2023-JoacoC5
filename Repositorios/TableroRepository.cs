using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_JoacoC5.Models;

namespace tl2_tp10_2023_JoacoC5.Repository;

public class TableroRepository : ITableroRepository
{
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

    public void CreateTablero(Tablero nuevo)
    {
        var query = @"INSERT INTO Tablero(id_usuario_propietario, nombre, descripcion) VALUES(@id_usuario_propietario, @nombre, @descripcion);";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", nuevo.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", nuevo.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", nuevo.Descripcion));

            command.ExecuteNonQuery();

            connection.Close();

        }
    }
    public void UpdateTablero(int idBuscado, Tablero modificado)
    {
        var query = @"UPDATE Tablero SET WHERE id_tablero = @idBuscado;"; //REVISAR MAS ADELANTE
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", modificado.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", modificado.Nombre));
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
        var query = @"SELECT * FRO  Tablero WHERE id_usuario_propietario = @idUBuscado;";
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