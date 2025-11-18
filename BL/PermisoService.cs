using BE;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PermisoService
    {
        private static readonly Lazy<PermisoService> _inst = new Lazy<PermisoService>(() => new PermisoService());
        public static PermisoService Instancia => _inst.Value;

        private readonly PermisoDAO _permisoDao = new PermisoDAO();
        private readonly UsuarioRepository _usuarioRepo = new UsuarioRepository();

        private PermisoService() { }

        /// <summary>
        /// Construye el árbol de permisos comenzando desde el identificador indicado.
        /// </summary>
        public PermisoComponent ConstruirArbolDesde(int permisoId)
        {
            var (permisos, hijosPorPadre) = CargarEstructuras();
            if (!permisos.ContainsKey(permisoId))
                throw new ArgumentException($"No existe el permiso con Id={permisoId}.", nameof(permisoId));

            return ConstruirNodoComposite(permisoId, permisos, hijosPorPadre, new HashSet<int>());
        }

        /// <summary>
        /// Devuelve los árboles raíz que tiene asignados un rol determinado.
        /// </summary>
        public List<PermisoComponent> ObtenerPermisosDeRol(int rolId)
        {
            var resultado = new List<PermisoComponent>();
            var (permisos, hijosPorPadre) = CargarEstructuras();
            var ids = _permisoDao.GetPermisosDeRol(rolId);

            foreach (var permisoId in ids.Distinct())
            {
                if (!permisos.ContainsKey(permisoId))
                    continue;

                var componente = ConstruirNodoComposite(permisoId, permisos, hijosPorPadre, new HashSet<int>());
                resultado.Add(componente);
            }

            return resultado;
        }

        /// <summary>
        /// Combina todos los permisos de todos los roles de un usuario en un único composite raíz.
        /// </summary>
        public PermisoComponent ObtenerPermisosDeUsuario(int usuarioId)
        {
            var roles = _usuarioRepo.GetRoles(usuarioId);
            var raiz = new PermisoCompuesto(0, "PermisosDeUsuario");

            var (permisos, hijosPorPadre) = CargarEstructuras();

            // permisos heredados por rol
            foreach (var rol in roles)
            {
                var idsRol = _permisoDao.GetPermisosDeRol(rol.Id);
                foreach (var id in idsRol.Distinct())
                {
                    if (permisos.ContainsKey(id))
                        raiz.Agregar(ConstruirNodoComposite(id, permisos, hijosPorPadre, new HashSet<int>()));
                }
            }

            // permisos directos de usuario
            var permisoRepo = new UsuarioPermisoRepository();
            var permisosUsuario = permisoRepo.GetPermisosDeUsuario(usuarioId);
            foreach (var id in permisosUsuario.Distinct())
            {
                if (permisos.ContainsKey(id))
                    raiz.Agregar(ConstruirNodoComposite(id, permisos, hijosPorPadre, new HashSet<int>()));
            }

            return raiz;
        }


        /// <summary>
        /// Devuelve el árbol completo de permisos listo para enlazar a un TreeView.
        /// </summary>
        public List<PermisoNodo> ObtenerArbolPermisos()
        {
            var (permisos, hijosPorPadre) = CargarEstructuras();
            var hijosAsignados = new HashSet<int>(hijosPorPadre.SelectMany(kvp => kvp.Value));
            var raices = permisos.Values
                .Where(p => !hijosAsignados.Contains(p.Id))
                .OrderBy(p => p.Nombre)
                .ToList();

            var resultado = new List<PermisoNodo>();
            foreach (var raiz in raices)
            {
                resultado.Add(ConstruirNodoDTO(raiz.Id, permisos, hijosPorPadre, new HashSet<int>()));
            }
            return resultado;
        }

        /// <summary>
        /// Devuelve solo los permisos asignados a un rol en formato jerárquico.
        /// </summary>
        public PermisoNodo ObtenerArbolDeRol(int rolId)
        {
            var raiz = new PermisoNodo
            {
                Id = 0,
                Nombre = $"Permisos del Rol {rolId}"
            };

            foreach (var componente in ObtenerPermisosDeRol(rolId))
            {
                raiz.Hijos.Add(ConvertirApermisoNodo(componente));
            }

            return raiz;
        }

        /// <summary>
        /// Asigna un permiso raíz a un rol.
        /// </summary>
        public void AsignarPermisoARol(int rolId, int permisoId)
        {
            _permisoDao.AsignarPermisoARol(rolId, permisoId);
        }

        /// <summary>
        /// Quita un permiso raíz de un rol.
        /// </summary>
        public void QuitarPermisoARol(int rolId, int permisoId)
        {
            _permisoDao.QuitarPermisoARol(rolId, permisoId);
        }

        /// <summary>
        /// Lista plana de permisos para interfaces administrativas.
        /// </summary>
        public List<PermisoComposite> ListarPermisos()
        {
            return _permisoDao.GetAllPermisos();
        }

        public int CrearPermiso(string nombre, bool esCompuesto)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            return _permisoDao.CrearPermiso(nombre.Trim(), esCompuesto);
        }

        public void ActualizarPermiso(int id, string nombre, bool esCompuesto)
        {
            if (id <= 0) throw new ArgumentException("Id inválido", nameof(id));
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.", nameof(nombre));

            _permisoDao.ActualizarPermiso(id, nombre.Trim(), esCompuesto);
        }

        public void AsignarRelacion(int padreId, int hijoId)
        {
            if (padreId <= 0 || hijoId <= 0)
                throw new ArgumentException("Los permisos deben ser válidos.");
            if (padreId == hijoId)
                throw new InvalidOperationException("Un permiso no puede ser padre de sí mismo.");

            _permisoDao.AsignarHijo(padreId, hijoId);
        }

        public void QuitarRelacion(int padreId, int hijoId)
        {
            if (padreId <= 0 || hijoId <= 0)
                throw new ArgumentException("Los permisos deben ser válidos.");
            if (padreId == hijoId)
                throw new InvalidOperationException("Los permisos deben ser distintos.");

            _permisoDao.QuitarHijo(padreId, hijoId);
        }

        #region Helpers

        private (Dictionary<int, PermisoComposite> Permisos, Dictionary<int, List<int>> HijosPorPadre) CargarEstructuras()
        {
            var permisos = _permisoDao.GetAllPermisos();
            var relaciones = _permisoDao.GetRelaciones();

            var permisosDict = permisos.ToDictionary(p => p.Id);
            var hijosPorPadre = new Dictionary<int, List<int>>();

            foreach (var relacion in relaciones)
            {
                if (!hijosPorPadre.TryGetValue(relacion.PadreId, out var hijos))
                {
                    hijos = new List<int>();
                    hijosPorPadre[relacion.PadreId] = hijos;
                }
                hijos.Add(relacion.HijoId);
            }

            return (permisosDict, hijosPorPadre);
        }

        private PermisoComponent ConstruirNodoComposite(
            int permisoId,
            Dictionary<int, PermisoComposite> permisos,
            Dictionary<int, List<int>> hijosPorPadre,
            HashSet<int> visitados)
        {
            if (!visitados.Add(permisoId))
                throw new InvalidOperationException($"Se detectó una relación cíclica en el permiso {permisoId}.");

            var dto = permisos[permisoId];
            PermisoComponent componente = dto.EsCompuesto
                ? (PermisoComponent)new PermisoCompuesto(dto.Id, dto.Nombre)
                : new PermisoSimple(dto.Id, dto.Nombre);

            if (dto.EsCompuesto && hijosPorPadre.TryGetValue(permisoId, out var hijos))
            {
                foreach (var hijoId in hijos)
                {
                    var hijo = ConstruirNodoComposite(hijoId, permisos, hijosPorPadre, visitados);
                    ((PermisoCompuesto)componente).Agregar(hijo);
                }
            }

            visitados.Remove(permisoId);
            return componente;
        }

        private PermisoNodo ConstruirNodoDTO(
            int permisoId,
            Dictionary<int, PermisoComposite> permisos,
            Dictionary<int, List<int>> hijosPorPadre,
            HashSet<int> visitados)
        {
            if (!visitados.Add(permisoId))
                throw new InvalidOperationException($"Se detectó una relación cíclica en el permiso {permisoId}.");

            var dto = permisos[permisoId];
            var nodo = new PermisoNodo
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };

            if (hijosPorPadre.TryGetValue(permisoId, out var hijos))
            {
                foreach (var hijoId in hijos)
                {
                    nodo.Hijos.Add(ConstruirNodoDTO(hijoId, permisos, hijosPorPadre, visitados));
                }
            }

            visitados.Remove(permisoId);
            return nodo;
        }

        private PermisoNodo ConvertirApermisoNodo(PermisoComponent componente)
        {
            var nodo = new PermisoNodo
            {
                Id = componente.Id,
                Nombre = componente.Nombre
            };

            if (componente is PermisoCompuesto compuesto)
            {
                foreach (var hijo in compuesto.Hijos)
                {
                    nodo.Hijos.Add(ConvertirApermisoNodo(hijo));
                }
            }

            return nodo;
        }

        public List<PermisoComponent> ObtenerPermisosDirectosDeUsuario(int usuarioId)
        {
            var dao = new DAO.UsuarioPermisoRepository();
            return dao.GetPermisosDirectos(usuarioId);
        }

        public void AsignarPermisoDirectoUsuario(int usuarioId, int permisoId)
        {
            var repo = new DAO.UsuarioPermisoRepository();
            repo.AsignarPermisoAUsuario(usuarioId, permisoId);

            // DVH de la nueva fila y luego DVV de la tabla
            var raw = new DAO.DvRawRepository();
            var dvhUP = BL.HashHelper.Sha256($"{usuarioId}|{permisoId}");
            raw.UpdateUsuarioPermisoDVH(usuarioId, permisoId, dvhUP);

            VerificadorIntegridadService.Instancia.RecalcularDVV_UsuarioPermiso();
        }

        public void QuitarPermisoDirectoUsuario(int usuarioId, int permisoId)
        {
            var repo = new DAO.UsuarioPermisoRepository();
            repo.QuitarPermisoAUsuario(usuarioId, permisoId);

            // al borrar no hay DVH que setear; solo DVV de la tabla
            VerificadorIntegridadService.Instancia.RecalcularDVV_UsuarioPermiso();
        }




        #endregion
    }
}

