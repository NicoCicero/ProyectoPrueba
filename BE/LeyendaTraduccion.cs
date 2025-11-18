using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// Representa la traducción de una leyenda (etiqueta) para un idioma determinado.
    /// </summary>
    public class LeyendaTraduccion
    {
        public int EtiquetaId { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
    }
}
