using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum AuditoriaEvento
    {
        AppStart,
        AppExit,
        LoginOk,
        LoginFail,
        LoginBloqueado,
        Logout,
        PermisoDenegado,
        CambioPassword,
        AltaUsuario,
        BajaUsuario,
        ModificacionUsuario,
        IntegridadInconsistente
    }
    /// <summary>Entrada simple de auditoría del sistema.</summary>
    public class AuditoriaEntry
    {
        public int Id { get; set; }
        public DateTime FechaUtc { get; set; }
        public int? UsuarioId { get; set; }   // puede ser null si el evento es anónimo (antes de login)
        public AuditoriaEvento Evento { get; set; }
        public string Detalle { get; set; } = string.Empty;
    }
}
