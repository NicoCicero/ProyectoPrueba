using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TraduccionRepository : DAL
    {
        /// <summary>
        /// Devuelve diccionario {clave -> texto} para un idioma.
        /// Si alguna traducción está NULL en la tabla, NO la agrega.
        /// El fallback se hace en la capa BL.
        /// </summary>
        public Dictionary<string, string> ObtenerTraduccionesPorCodigo(string codigoIdioma)
        {
            var dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT e.Clave, t.Texto
                FROM Idioma i
                JOIN Traduccion t ON t.IdIdioma = i.IdIdioma
                JOIN Etiqueta e ON e.IdEtiqueta = t.IdEtiqueta
                WHERE i.Codigo = @cod
                  AND t.Texto IS NOT NULL;", cn))
            {
                cmd.Parameters.AddWithValue("@cod", codigoIdioma);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string clave = rd.GetString(0);
                        string texto = rd.GetString(1);
                        dic[clave] = texto;
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// Devuelve la lista de TODAS las claves conocidas (Etiqueta).
        /// La usamos para hacer fallback.
        /// </summary>
        public List<string> ListarClaves()
        {
            var list = new List<string>();
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT Clave FROM Etiqueta;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(rd.GetString(0));
                }
            }
            return list;
        }

        public List<(int EtiquetaId, string Clave, string Descripcion, string Texto)> ListarLeyendasPorIdioma(int idiomaId)
        {
            var list = new List<(int, string, string, string)>();
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT e.IdEtiqueta, e.Clave, ISNULL(e.Descripcion, ''), t.Texto
                FROM Etiqueta e
                LEFT JOIN Traduccion t ON t.IdEtiqueta = e.IdEtiqueta AND t.IdIdioma = @idioma
                ORDER BY e.Clave;", cn))
            {
                cmd.Parameters.AddWithValue("@idioma", idiomaId);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add((
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.GetString(2),
                            rd.IsDBNull(3) ? null : rd.GetString(3)
                        ));
                    }
                }
            }
            return list;
        }

        public int? ObtenerEtiquetaIdPorClave(string clave)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT IdEtiqueta FROM Etiqueta WHERE Clave = @clave;", cn))
            {
                cmd.Parameters.AddWithValue("@clave", clave);
                cn.Open();
                var result = cmd.ExecuteScalar();
                return result == null || result == DBNull.Value ? (int?)null : Convert.ToInt32(result);
            }
        }

        public int CrearEtiqueta(string clave, string descripcion)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"INSERT INTO Etiqueta (Clave, Descripcion)
                                           VALUES (@clave, @desc);
                                           SELECT CAST(SCOPE_IDENTITY() AS INT);", cn))
            {
                cmd.Parameters.AddWithValue("@clave", clave);
                cmd.Parameters.AddWithValue("@desc", (object)descripcion ?? DBNull.Value);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void ActualizarDescripcionEtiqueta(int etiquetaId, string descripcion)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "UPDATE Etiqueta SET Descripcion = @desc WHERE IdEtiqueta = @id;",
                cn))
            {
                cmd.Parameters.AddWithValue("@desc", (object)descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", etiquetaId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void GuardarTraduccion(int idiomaId, int etiquetaId, string texto)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                IF EXISTS (SELECT 1 FROM Traduccion WHERE IdIdioma = @idioma AND IdEtiqueta = @etiqueta)
                    UPDATE Traduccion SET Texto = @texto WHERE IdIdioma = @idioma AND IdEtiqueta = @etiqueta;
                ELSE
                    INSERT INTO Traduccion (IdIdioma, IdEtiqueta, Texto) VALUES (@idioma, @etiqueta, @texto);", cn))
            {
                cmd.Parameters.AddWithValue("@idioma", idiomaId);
                cmd.Parameters.AddWithValue("@etiqueta", etiquetaId);
                cmd.Parameters.AddWithValue("@texto", (object)texto ?? DBNull.Value);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
