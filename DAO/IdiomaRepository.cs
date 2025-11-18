using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class IdiomaRepository : DAL
    {
        public List<(int Id, string Codigo, string Nombre, bool EsPorDefecto)> ListarIdiomasRaw()
        {
            var list = new List<(int, string, string, bool)>();

            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT IdIdioma, Codigo, Nombre, EsPorDefecto FROM Idioma ORDER BY Nombre;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add((
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.GetString(2),
                            rd.GetBoolean(3)
                        ));
                    }
                }
            }
            return list;
        }

        public (int Id, string Codigo, string Nombre) ObtenerIdiomaPorDefecto()
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 IdIdioma, Codigo, Nombre FROM Idioma WHERE EsPorDefecto = 1;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                        return (rd.GetInt32(0), rd.GetString(1), rd.GetString(2));
                }
            }
            return (0, null, null);
        }

        public int CrearIdioma(string codigo, string nombre, bool esPorDefecto)
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    if (esPorDefecto)
                    {
                        using (var cmdReset = new SqlCommand("UPDATE Idioma SET EsPorDefecto = 0;", cn, tx))
                            cmdReset.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(@"INSERT INTO Idioma (Codigo, Nombre, EsPorDefecto)
                                                  VALUES (@cod, @nom, @def);
                                                  SELECT CAST(SCOPE_IDENTITY() AS INT);", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@cod", codigo);
                        cmd.Parameters.AddWithValue("@nom", nombre);
                        cmd.Parameters.AddWithValue("@def", esPorDefecto);
                        var nuevoId = (int)cmd.ExecuteScalar();
                        tx.Commit();
                        return nuevoId;
                    }
                }
            }
        }

        public void ActualizarIdioma(int id, string nombre, bool esPorDefecto)
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    if (esPorDefecto)
                    {
                        using (var cmdReset = new SqlCommand("UPDATE Idioma SET EsPorDefecto = 0;", cn, tx))
                            cmdReset.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "UPDATE Idioma SET Nombre = @nom, EsPorDefecto = @def WHERE IdIdioma = @id;",
                        cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@nom", nombre);
                        cmd.Parameters.AddWithValue("@def", esPorDefecto);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
        }
    }
}
