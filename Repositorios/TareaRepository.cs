using System.Data.SQLite;
using tl2_tp10_2023_JoacoC5.Models;
using System.Data.SqlClient;

namespace tl2_tp10_2023_JoacoC5.Repository;

public class TareaRepository : ITareaRepository
{
    private string cadenaConexion;

    public TareaRepository(string _cadenaConexion)
    {
        cadenaConexion = _cadenaConexion;
    }

    public void CreateTarea(int idTablero, Tarea nuevo)
    {
        var query = @"INSERT INTO Tarea(id_tablero,nombre,estado,descripcion,color,id_usuario_asignado)
        VALUES(@id_tablero,@nombre,@estado,@descripcion,@color,@id_usuario_asignado);";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);

            command.Parameters.Add(new SQLiteParameter("@id_tablero", idTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", nuevo.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", nuevo.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", nuevo.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", nuevo.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", nuevo.IdUsuarioAsignado));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public void UpdateTarea(int idBuscado, Tarea modificado)
    {
        var query = @"UPDATE Tarea SET nombre=@nombre, estado=@estado,
         descripcion=@descripcion, color=@color, id_tablero=@idTablero, 
         id_usuario_asignado=@idUsuario
        WHERE id_tarea = @idBuscado;";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();

            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));
            command.Parameters.Add(new SQLiteParameter("@nombre", modificado.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", modificado.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", modificado.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", modificado.Color));
            command.Parameters.Add(new SQLiteParameter("@idTablero", modificado.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@idUsuario", modificado.IdUsuarioAsignado));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public Tarea GetTareaById(int idBuscado)
    {
        var query = @"SELECT * FROM Tarea WHERE id_tarea = @idBuscado;";
        Tarea aux = new Tarea();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    aux.Id = Convert.ToInt32(reader["id_tarea"]);
                    aux.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    aux.Descripcion = reader["descripcion"].ToString();
                    aux.Color = reader["color"].ToString();
                    if (reader["id_usuario_asignado"] == DBNull.Value)
                    {
                        aux.IdUsuarioAsignado = 0;
                    }
                    else
                    {
                        aux.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                }
            }
            connection.Close();
        }

        if (aux == null)
        {
            throw new Exception("Tarea no encontrada");
        }
        return aux;
    }
    public List<Tarea> GetAllTareaByUsuario(int idBuscado)
    {
        var query = @"SELECT * FROM Tarea WHERE id_usuario_asignado = @idBuscado;";
        List<Tarea> tareas = new List<Tarea>();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tarea aux = new Tarea();
                    aux.Id = Convert.ToInt32(reader["id_tarea"]);
                    aux.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    aux.Descripcion = reader["descripcion"].ToString();
                    aux.Color = reader["color"].ToString();
                    aux.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    tareas.Add(aux);
                }
            }
            connection.Close();
        }
        if (tareas == null)
        {
            throw new Exception("Tareas no encontradas");
        }
        return tareas;
    }
    public List<Tarea> GetAllTareaByTablero(int idTablero)
    {
        var query = @"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
        List<Tarea> tareas = new List<Tarea>();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tarea aux = new Tarea();
                    aux.Id = Convert.ToInt32(reader["id_tarea"]);
                    aux.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    aux.Descripcion = reader["descripcion"].ToString();
                    aux.Color = reader["color"].ToString();
                    if (reader["id_usuario_asignado"] == DBNull.Value)
                    {
                        aux.IdUsuarioAsignado = 0;
                    }
                    else
                    {
                        aux.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    tareas.Add(aux);
                }
            }
            connection.Close();
        }
        if (tareas == null)
        {
            throw new Exception("Tareas no encontradas");
        }
        return tareas;
    }
    public void SetUsuario(int idUsuario, int idBuscado)
    {
        var query = @"UPDATE Tarea SET id_usuario_asignado=@idUsuario WHERE id_tarea = @idBuscado;";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();

            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));
            command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public void DeleteTarea(int idBuscado)
    {
        var query = @"DELETE FROM Tarea WHERE id_tarea = @idBuscado;";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();

            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idBuscado", idBuscado));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Tarea> GetAllTareas()
    {
        var query = @"SELECT * FROM Tarea;";
        List<Tarea> tareas = new List<Tarea>();

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            var command = new SQLiteCommand(query, connection);
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Tarea aux = new Tarea();
                    aux.Id = Convert.ToInt32(reader["id_tarea"]);
                    aux.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    aux.Nombre = reader["nombre"].ToString();
                    aux.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    aux.Descripcion = reader["descripcion"].ToString();
                    aux.Color = reader["color"].ToString();
                    if (reader["id_usuario_asignado"] == DBNull.Value)
                    {
                        aux.IdUsuarioAsignado = 0;
                    }
                    else
                    {
                        aux.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    }
                    tareas.Add(aux);
                }
            }
            connection.Close();
        }
        if (tareas == null)
        {
            throw new Exception("Tareas no encontradas");
        }
        return tareas;
    }

}