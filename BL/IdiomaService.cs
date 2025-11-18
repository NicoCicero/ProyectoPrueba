using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public class IdiomaService
    {
        private readonly IdiomaRepository _idiomaRepo = new IdiomaRepository();
        private readonly TraduccionRepository _tradRepo = new TraduccionRepository();

        public void SeleccionarIdioma(string codigo)
        {
            var traducciones = _tradRepo.ObtenerTraduccionesPorCodigo(codigo);
            var todasLasClaves = _tradRepo.ListarClaves();

            // fallback
            foreach (var clave in todasLasClaves)
                if (!traducciones.ContainsKey(clave))
                    traducciones[clave] = $"[{clave}]";

            // ✅ guardar el código actual antes o después de Notificar (da igual)
            IdiomaManager.Instancia.SetCodigoActual(codigo);

            // notificar a todos los forms suscriptos
            IdiomaManager.Instancia.Notificar(traducciones);
        }

        public void SeleccionarIdiomaPorDefecto()
        {
            var def = _idiomaRepo.ObtenerIdiomaPorDefecto();
            if (def.Codigo != null)
                SeleccionarIdioma(def.Codigo);
        }

        //  ESTA es la que usa el ComboBox
        public List<Idioma> ListarIdiomas()
        {
            var raws = _idiomaRepo.ListarIdiomasRaw();
            return raws.Select(r => new Idioma
            {
                Id = r.Id,
                Codigo = r.Codigo,
                Nombre = r.Nombre,
                EsPorDefecto = r.EsPorDefecto
            }).ToList();
        }

        public int CrearIdioma(string codigo, string nombre, bool esPorDefecto)
        {
            if (!SessionManager.Instancia.TieneRol("Administrador"))
                throw new UnauthorizedAccessException("Solo un administrador puede crear idiomas.");

            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código es obligatorio.", nameof(codigo));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            return _idiomaRepo.CrearIdioma(codigo.Trim(), nombre.Trim(), esPorDefecto);
        }

        public void ActualizarIdioma(int id, string nombre, bool esPorDefecto)
        {
            if (id <= 0) throw new ArgumentException("Id inválido", nameof(id));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            _idiomaRepo.ActualizarIdioma(id, nombre.Trim(), esPorDefecto);
        }

        public List<LeyendaTraduccion> ListarLeyendas(int idiomaId)
        {
            if (idiomaId <= 0)
                return new List<LeyendaTraduccion>();

            var raws = _tradRepo.ListarLeyendasPorIdioma(idiomaId);
            return raws.Select(r => new LeyendaTraduccion
            {
                EtiquetaId = r.EtiquetaId,
                Clave = r.Clave,
                Descripcion = r.Descripcion,
                Texto = r.Texto ?? string.Empty
            }).ToList();
        }

        public void GuardarLeyenda(int idiomaId, string clave, string descripcion, string texto)
        {
            if (idiomaId <= 0)
                throw new ArgumentException("Idioma inválido", nameof(idiomaId));
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave es obligatoria.", nameof(clave));

            clave = clave.Trim();
            descripcion = descripcion?.Trim();
            texto = texto?.Trim();

            var etiquetaId = _tradRepo.ObtenerEtiquetaIdPorClave(clave);
            if (!etiquetaId.HasValue)
            {
                etiquetaId = _tradRepo.CrearEtiqueta(clave, descripcion ?? clave);
            }
            else if (!string.IsNullOrWhiteSpace(descripcion))
            {
                _tradRepo.ActualizarDescripcionEtiqueta(etiquetaId.Value, descripcion);
            }

            _tradRepo.GuardarTraduccion(idiomaId, etiquetaId.Value, texto);
        }
    }
}
