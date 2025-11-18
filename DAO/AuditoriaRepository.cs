using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class AuditoriaRepository : DAL
    {
        public void Registrar(string evento, int? usuarioId, string detalle)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO Auditoria (FechaUtc, UsuarioId, Evento, Detalle)
                VALUES (SYSUTCDATETIME(), @uid, @ev, @det);", cn))
            {
                cmd.Parameters.AddWithValue("@uid", (object)usuarioId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ev", evento);
                cmd.Parameters.AddWithValue("@det", (object)detalle ?? DBNull.Value);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //public List<AuditoriaEntry> Listar(int top = 100)
        //{
        //    var list = new List<AuditoriaEntry>();
        //    using (var cn = GetConnection())
        //    using (var cmd = new SqlCommand(@"SELECT TOP(@top) Id , FechaUtc , UsuarioId, Evento, Detalle FROM Auditoria ORDER BY FechaUtc DESC", cn))
        //    {
        //        cmd.Parameters.AddWithValue("@top", top);
        //        cn.Open();

        //        using(var rd  = cmd.ExecuteReader())
        //        {
        //            while (rd.Read())
        //            {
        //                list.Add(new AuditoriaEntry { Id = rd.GetInt32(0), FechaUtc = rd.GetDateTime(1), UsuarioId = rd.IsDBNull(2) ? (int?)null: rd.GetInt32(2), Evento = ParseEvento(rd.GetString(3)), Detalle = rd.IsDBNull(4) ? "" : rd.GetString(4)});
        //            }
        //        }
        //        return list;
        //    }

        //}
        public List<AuditoriaEntry> FiltrarAuditoria(
            int? id = null,
            int? usuarioId = null,
            string evento = null,           // "LOGIN_OK", etc.
            string texto = null,            // busca en Detalle
            DateTime? desdeUtc = null,
            DateTime? hastaUtcExcl = null,  // rango mitad-abierto [desde, hasta)
            int top = 500                   // límite por si no hay filtros
        )
        {
            var list = new List<AuditoriaEntry>();
            using (var cn = GetConnection())
            {
                var sb = new System.Text.StringBuilder(@"
            SELECT {TOP} Id, FechaUtc, UsuarioId, Evento, Detalle
            FROM Auditoria
            WHERE 1=1");

                // armamos el WHERE según lo que venga
                if (id.HasValue) sb.Append(" AND Id = @id");
                if (usuarioId.HasValue) sb.Append(" AND UsuarioId = @uid");
                if (!string.IsNullOrWhiteSpace(evento)) sb.Append(" AND Evento = @ev");
                if (!string.IsNullOrWhiteSpace(texto)) sb.Append(" AND Detalle LIKE @txt");
                if (desdeUtc.HasValue) sb.Append(" AND FechaUtc >= @desde");
                if (hastaUtcExcl.HasValue) sb.Append(" AND FechaUtc <  @hasta");

                sb.Append(" ORDER BY FechaUtc DESC;");

                // si no se pasó ningún filtro, aplico TOP para no traer todo
                string sql = sb.ToString().Replace("{TOP}",
                    (id.HasValue || usuarioId.HasValue || !string.IsNullOrWhiteSpace(evento)
                      || !string.IsNullOrWhiteSpace(texto) || desdeUtc.HasValue ||hastaUtcExcl.HasValue) ? "" : $"TOP ({top})");
                using (var cmd = new SqlCommand(sql, cn))
                {
                    if (id.HasValue) cmd.Parameters.AddWithValue("@id", id.Value);
                    if (usuarioId.HasValue) cmd.Parameters.AddWithValue("@uid", usuarioId.Value);
                    if (!string.IsNullOrWhiteSpace(evento)) cmd.Parameters.AddWithValue("@ev", evento);
                    if (!string.IsNullOrWhiteSpace(texto)) cmd.Parameters.AddWithValue("@txt", "%" + texto + "%");
                    if (desdeUtc.HasValue) cmd.Parameters.AddWithValue("@desde", desdeUtc.Value);
                    if (hastaUtcExcl.HasValue) cmd.Parameters.AddWithValue("@hasta", hastaUtcExcl.Value);

                    cn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new AuditoriaEntry
                            {
                                Id = rd.GetInt32(0),
                                FechaUtc = rd.GetDateTime(1),
                                UsuarioId = rd.IsDBNull(2) ? (int?)null : rd.GetInt32(2),
                                Evento = ParseEvento(rd.GetString(3)),
                                Detalle = rd.IsDBNull(4) ? "" : rd.GetString(4)
                            });
                        }
                    }
                }
            }
            return list;
        }
        private static AuditoriaEvento ParseEvento(string evento)
        {
            switch (evento?.ToUpperInvariant())
            {
                case "APP_START": return AuditoriaEvento.AppStart;
                case "APP_EXIT": return AuditoriaEvento.AppExit;
                case "LOGIN_OK": return AuditoriaEvento.LoginOk;
                case "LOGIN_FAIL": return AuditoriaEvento.LoginFail;
                case "LOGIN_BLOQUEADO": return AuditoriaEvento.LoginBloqueado;
                case "LOGOUT": return AuditoriaEvento.Logout;
                case "PERMISO_DENEGADO": return AuditoriaEvento.PermisoDenegado;
                case "CAMBIO_PASSWORD": return AuditoriaEvento.CambioPassword;
                case "ALTA_USUARIO": return AuditoriaEvento.AltaUsuario;
                case "BAJA_USUARIO": return AuditoriaEvento.BajaUsuario;
                case "MODIFICACION_USUARIO": return AuditoriaEvento.ModificacionUsuario;
                case "INTEGRIDAD_FALLA": return AuditoriaEvento.IntegridadInconsistente;
                default: return AuditoriaEvento.LoginFail;
            }
        }
    }
}

