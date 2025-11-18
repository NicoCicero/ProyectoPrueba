using BE;
using BL;
using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormPrueba : Form, IIdiomaObserver
    {
        private readonly IdiomaService _idiomaSrv = new IdiomaService();
        private readonly List<BE.Idioma> _idiomasAdmin = new List<BE.Idioma>();
        private List<BE.LeyendaTraduccion> _leyendasActuales = new List<BE.LeyendaTraduccion>();
        private List<BE.PermisoComposite> _permisosPlanos = new List<BE.PermisoComposite>();

        private bool _registroVisible = false;   // estado del bloque de registro
        private bool _regRolesCargados = false;  // ya lo tenés: lo dejamos

        private bool _puedeGestionarUsuarios;
        private bool _puedeVerBitacora;
        private bool _puedeVerCambios;
        private bool _puedeGestionarPermisos;
        private bool _puedeGestionarIdiomas;
        private bool? _ultimoEstadoIntegridadOk;
        private bool _esAdministrador;

        private TabPage _tabRegistrar;
        private TabPage _tabBitacora;
        private TabPage _tabCambios;
        public FormPrueba()
        {
            InitializeComponent();
            IdiomaManager.Instancia.Suscribir(this);
        }

        private void FormPrueba_Load(object sender, EventArgs e)
        {
            ConfigurarDgvCambios();
            IdiomaManager.Instancia.Suscribir(this);
            if (SessionManager.Instancia.UsuarioActual == null)
            {
                MessageBox.Show("La sesión no está activa. Volviendo al login.");
                Application.Restart();
                return;
            }

            var usuario = SessionManager.Instancia.UsuarioActual;
            _puedeGestionarUsuarios = SessionManager.Instancia.TienePermiso("Usuario.Modificar");
            _puedeVerBitacora = SessionManager.Instancia.TienePermiso("Bitacora.Ver");
            _puedeVerCambios = SessionManager.Instancia.TienePermiso("ControlCambios.Ver");
            _puedeGestionarPermisos = SessionManager.Instancia.TienePermiso("Permiso.Gestionar");
            _puedeGestionarIdiomas = SessionManager.Instancia.TienePermiso("Idioma.Gestionar");
            _esAdministrador = SessionManager.Instancia.TieneRol("Administrador");
            bool puedeCrearTicket = usuario.TienePermiso("Ticket.Crear");

            this.Text = $"FormPrueba - {usuario.Email} (Ticket.Crear={(puedeCrearTicket ? "Sí" : "No")})";

            _tabRegistrar = tabUsuarios;
            _tabBitacora = tabBitacora;
            _tabCambios = tabControlCambios;

            if (!_puedeGestionarPermisos && tabGeneral.TabPages.Contains(tabPermisos))
                tabGeneral.TabPages.Remove(tabPermisos);
            else if (_puedeGestionarPermisos)
                InicializarGestionPermisos();

            if (!_puedeGestionarIdiomas && tabGeneral.TabPages.Contains(tabIdiomas))
                tabGeneral.TabPages.Remove(tabIdiomas);
            else if (_puedeGestionarIdiomas)
            {
                CargarIdiomasAdmin();
                AplicarRestriccionIdiomas();
            }

            if (_puedeGestionarUsuarios)
            {
                SetRegistrarVisible(true);
                CargarGrillaGestionUsuarios();
            }
            else
            {
                if (_tabRegistrar != null && tabGeneral.TabPages.Contains(_tabRegistrar))
                    tabGeneral.TabPages.Remove(_tabRegistrar);
                SetRegistrarVisible(false);
            }

            if (!_puedeVerBitacora && _tabBitacora != null && tabGeneral.TabPages.Contains(_tabBitacora))
                tabGeneral.TabPages.Remove(_tabBitacora);
            else if (_puedeVerBitacora)
            {
                CargarEventosBitacoraHardcoded();
                CargarBitacoraInicial();
            }

            if (!_puedeVerCambios && _tabCambios != null && tabGeneral.TabPages.Contains(_tabCambios))
                tabGeneral.TabPages.Remove(_tabCambios);
            else if (_puedeVerCambios)
            {
                CargarCambiosInicial();
            }

            CargarSelectorIdiomas();

            dgvGestionUsuario.AllowUserToAddRows = false;
            dgvGestionUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGestionUsuario.MultiSelect = false;
            dgvGestionUsuario.ReadOnly = true;  // si no editás inline

            ActualizarEstadoIntegridadVisual();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }

        // este es el MÉTODO que llama el observer
        public void ActualizarIdioma(Dictionary<string, string> t)
        {
            if (tabGeneral.TabPages.Contains(tabMenuPrincipal))
                tabMenuPrincipal.Text = Texto(t, "TAB_MENU");
            if (tabGeneral.TabPages.Contains(tabUsuarios))
                tabUsuarios.Text = Texto(t, "TAB_USUARIOS");
            if (tabGeneral.TabPages.Contains(tabBitacora))
                tabBitacora.Text = Texto(t, "TAB_BITACORA");
            if (tabGeneral.TabPages.Contains(tabControlCambios))
                tabControlCambios.Text = Texto(t, "TAB_CONTROL_CAMBIOS");
            if (tabGeneral.TabPages.Contains(tabPermisos))
                tabPermisos.Text = Texto(t, "TAB_PERMISOS");
            if (tabGeneral.TabPages.Contains(tabIdiomas))
                tabIdiomas.Text = Texto(t, "TAB_IDIOMAS");

            btnCerrarSesion.Text = Texto(t, "BTN_CERRAR_SESION");
            btnActualizar.Text = Texto(t, "BTN_ACTUALIZAR");
            btnNuevoRegistro.Text = Texto(t, "BTN_NUEVO_REGISTRO");
            btnEliminar.Text = Texto(t, "BTN_ELIMINAR_USUARIO");

            lblUsuarioId.Text = Texto(t, "LBL_USUARIO_ID");
            lblAuditoriaId.Text = Texto(t, "LBL_AUDITORIA_ID");
            lblEvento.Text = Texto(t, "LBL_EVENTO");
            lblDetalle.Text = Texto(t, "LBL_DETALLE");
            lblFecha.Text = Texto(t, "LBL_FECHA");
            btnFiltrarBitacora.Text = Texto(t, "BTN_FILTRAR");
            btnLimpiar.Text = Texto(t, "BTN_LIMPIAR");

            lblCambioId.Text = Texto(t, "LBL_CAMBIO_ID");
            lblCambioUsuarioId.Text = Texto(t, "LBL_CAMBIO_USUARIO_ID");
            lblCambioEntidad.Text = Texto(t, "LBL_CAMBIO_ENTIDAD");
            lblCambioEntidadId.Text = Texto(t, "LBL_CAMBIO_ENTIDAD_ID");
            lblCambioCampo.Text = Texto(t, "LBL_CAMBIO_CAMPO");
            btnFiltrarCambios.Text = Texto(t, "BTN_FILTRAR");
            btnLimpiarCambios.Text = Texto(t, "BTN_LIMPIAR");

            if (tabGeneral.TabPages.Contains(tabPermisos))
            {
                btnAsignar.Text = Texto(t, "BTN_PERMISO_ASIGNAR");
                btnQuitar.Text = Texto(t, "BTN_PERMISO_QUITAR");
                lblUsuarioSel.Text = Texto(t, "LBL_USUARIO_SELECCIONADO");
                lblPermisoExistente.Text = Texto(t, "LBL_PERMISO_EXISTENTE");
                lblPermisoNombre.Text = Texto(t, "LBL_PERMISO_NOMBRE");
                lblNuevoPermiso.Text = Texto(t, "LBL_PERMISO_NUEVO");
                lblPermisoPadre.Text = Texto(t, "LBL_PERMISO_PADRE");
                lblPermisoHijo.Text = Texto(t, "LBL_PERMISO_HIJO");
                btnActualizarPermiso.Text = Texto(t, "BTN_PERMISO_GUARDAR");
                btnCrearPermiso.Text = Texto(t, "BTN_PERMISO_CREAR");
                btnAgregarRelacionPermiso.Text = Texto(t, "BTN_PERMISO_RELACION_AGREGAR");
                btnQuitarRelacionPermiso.Text = Texto(t, "BTN_PERMISO_RELACION_QUITAR");
                lblPermisoSimpleRapido.Text = Texto(t, "LBL_PERMISO_SIMPLE", "Permiso simple");
                lblPermisoAsignarUsuario.Text = Texto(t, "LBL_PERMISO_ASIGNAR_USUARIO", "Asignar a usuario");
                lblPermisoAsignarRol.Text = Texto(t, "LBL_PERMISO_ASIGNAR_ROL", "Asignar a rol");
                btnAsignarPermisoAUsuario.Text = Texto(t, "BTN_PERMISO_ASIGNAR_USUARIO", "Asignar al usuario");
                btnAsignarPermisoARol.Text = Texto(t, "BTN_PERMISO_ASIGNAR_ROL", "Asignar al rol");
            }

            if (tabGeneral.TabPages.Contains(tabIdiomas))
            {
                lblIdiomaCodigo.Text = Texto(t, "LBL_IDIOMA_CODIGO");
                lblIdiomaNombre.Text = Texto(t, "LBL_IDIOMA_NOMBRE");
                lblIdiomaPorDefecto.Text = Texto(t, "LBL_IDIOMA_POR_DEFECTO");
                btnCrearIdioma.Text = Texto(t, "BTN_IDIOMA_CREAR");
                btnActualizarIdioma.Text = Texto(t, "BTN_IDIOMA_ACTUALIZAR");
                btnNuevoIdioma.Text = Texto(t, "BTN_IDIOMA_NUEVO");
                lblLeyendaClave.Text = Texto(t, "LBL_LEYENDA_CLAVE");
                lblLeyendaDescripcion.Text = Texto(t, "LBL_LEYENDA_DESCRIPCION");
                lblLeyendaTexto.Text = Texto(t, "LBL_LEYENDA_TEXTO");
                btnGuardarLeyenda.Text = Texto(t, "BTN_LEYENDA_GUARDAR");
                btnNuevaLeyenda.Text = Texto(t, "BTN_LEYENDA_NUEVA");
                lblIdiomaSeleccionadoAdmin.Text = Texto(t, "LBL_IDIOMA_SELECCIONADO");
            }

            btnVerificarIntegridad.Text = Texto(t, "BTN_VERIFICAR_INTEGRIDAD");
            btnRecalcularIntegridad.Text = Texto(t, "BTN_RECALCULAR_INTEGRIDAD");
        }

        private string Texto(Dictionary<string, string> dic, string clave, string fallback = null)
        {
            if (dic != null && dic.TryGetValue(clave, out var valor))
                return valor;
            return fallback ?? $"[{clave}]";
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cierra la sesión en DB (marca FinUtc y escribe auditoría)
            AuthService.Instancia.Logout();

            // Volvemos limpio al login (lo más simple para WinForms)
            Application.Restart();
        }

        private void TabGeneral_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pagina = tabGeneral.SelectedTab;
            if (pagina == null)
                return;

            if (pagina == tabUsuarios)
            {
                if (!_puedeGestionarUsuarios)
                {
                    MessageBox.Show("No contás con permiso para gestionar usuarios.");
                    tabGeneral.SelectedTab = tabMenuPrincipal;
                    return;
                }

                SetRegistrarVisible(true);
                CargarGrillaGestionUsuarios();
            }
            else if (pagina == tabBitacora)
            {
                if (!_puedeVerBitacora)
                {
                    MessageBox.Show("No contás con permiso para ver la Bitácora.");
                    tabGeneral.SelectedTab = tabMenuPrincipal;
                    return;
                }

                if (!_regRolesCargados)
                    CargarEventosBitacoraHardcoded();
            }
            else if (pagina == tabControlCambios)
            {
                if (!_puedeVerCambios)
                {
                    MessageBox.Show("No contás con permiso para ver el Control de Cambios.");
                    tabGeneral.SelectedTab = tabMenuPrincipal;
                    return;
                }
            }
            else if (pagina == tabPermisos)
            {
                if (!_puedeGestionarPermisos)
                {
                    MessageBox.Show("No contás con permiso para administrar permisos.");
                    tabGeneral.SelectedTab = tabMenuPrincipal;
                    return;
                }
            }
            else if (pagina == tabIdiomas)
            {
                if (!_puedeGestionarIdiomas)
                {
                    MessageBox.Show("No contás con permiso para administrar idiomas.");
                    tabGeneral.SelectedTab = tabMenuPrincipal;
                    return;
                }

                CargarIdiomasAdmin();
                AplicarRestriccionIdiomas();
            }
        }
        private void CargarCambiosInicial()
        {
            var repo = new ControlCambiosRepository();

            dtpCambiosDesde.Value = new DateTime(2000, 1, 1);
            dtpCambiosHasta.Value = DateTime.Today.AddDays(1);

            var datos = repo.FiltrarCambios(
                id: null,
                usuarioId: null,
                entidad: null,
                entidadId: null,
                campo: null,
                desdeUtc: DateTime.SpecifyKind(dtpCambiosDesde.Value.Date, DateTimeKind.Local).ToUniversalTime(),
                hastaUtcExcl: DateTime.SpecifyKind(dtpCambiosHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime()
            );

            // 👇 esto es clave: ya definimos las columnas nosotros
            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.DataSource = datos;
        }

        /// <summary>
        /// Ejemplo de cómo poblar un TreeView con el árbol completo de permisos.
        /// El Tag de cada nodo se mapea al Permiso_Id correspondiente.
        /// </summary>
        private void CargarPermisosEnTreeView(TreeView tree)
        {
            if (tree == null) return;

            tree.BeginUpdate();
            tree.Nodes.Clear();

            var servicio = PermisoService.Instancia;
            var arbol = servicio.ObtenerArbolPermisos();

            foreach (var permiso in arbol)
            {
                tree.Nodes.Add(CrearNodoTree(permiso));
            }

            tree.EndUpdate();
            tree.ExpandAll();
        }

        private TreeNode CrearNodoTree(PermisoNodo permiso)
        {
            var nodo = new TreeNode(permiso.Nombre)
            {
                Tag = permiso.Id   // Guardamos el Permiso_Id aquí
            };

            foreach (var hijo in permiso.Hijos)
            {
                nodo.Nodes.Add(CrearNodoTree(hijo));
            }

            return nodo;
        }

        private void CargarBitacoraInicial()
        {
            var repoInit = new AuditoriaRepository();

            // por defecto: desde 2000 hasta mañana
            dtpDesde.Value = new DateTime(2000, 1, 1);
            dtpHasta.Value = DateTime.Today.AddDays(1);

            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = repoInit.FiltrarAuditoria(
                id: null,
                usuarioId: null,
                evento: null,
                texto: null,
                desdeUtc: DateTime.SpecifyKind(dtpDesde.Value.Date, DateTimeKind.Local).ToUniversalTime(),
                hastaUtcExcl: DateTime.SpecifyKind(dtpHasta.Value.Date.AddDays(1), DateTimeKind.Local).ToUniversalTime()
            );

            // formateo de columnas
            if (dgvBitacora.Columns["FechaUtc"] != null)
                dgvBitacora.Columns["FechaUtc"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            if (dgvBitacora.Columns["Detalle"] != null)
                dgvBitacora.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void SetRegistrarVisible(bool v)
        {
            dgvGestionUsuario.Visible = v;
            btnActualizar.Visible = v;
            btnNuevoRegistro.Visible = v;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltrarBitacora_Click(object sender, EventArgs e)
        {
            int? id = int.TryParse(txtAuditoriaId.Text, out var vId) ? vId : (int?)null;
            int? usuarioId = int.TryParse(txtId.Text, out var vUid) ? vUid : (int?)null;

            string evento = string.IsNullOrWhiteSpace(cmbEvento.Text)
                ? null
                : cmbEvento.Text.Trim().ToUpperInvariant();

            string texto = string.IsNullOrWhiteSpace(txtTexto.Text)
                ? null
                : txtTexto.Text.Trim();

            DateTime desdeLocal = dtpDesde.Value.Date;
            DateTime hastaLocal = dtpHasta.Value.Date;
            if (!ValidarRangoFechas(desdeLocal, hastaLocal, "la bitácora"))
                return;

            var desdeUtc = DateTime.SpecifyKind(desdeLocal, DateTimeKind.Local).ToUniversalTime();
            var hastaUtcExcl = DateTime.SpecifyKind(hastaLocal.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            var repo = new AuditoriaRepository();
            var datos = repo.FiltrarAuditoria(id, usuarioId, evento, texto, desdeUtc, hastaUtcExcl);

            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.DataSource = datos;

            if (dgvBitacora.Columns["FechaUtc"] != null)
                dgvBitacora.Columns["FechaUtc"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            if (dgvBitacora.Columns["Detalle"] != null)
                dgvBitacora.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtAuditoriaId.Clear();
            txtId.Clear();
            if (cmbEvento.Items.Count > 0)
                cmbEvento.SelectedIndex = 0;
            else
                cmbEvento.SelectedIndex = -1;
            txtTexto.Clear();

            CargarBitacoraInicial();
        }

        private bool ValidarRangoFechas(DateTime desde, DateTime hasta, string contexto)
        {
            if (desde > hasta)
            {
                MessageBox.Show($"La fecha 'desde' no puede ser posterior a la fecha 'hasta' en {contexto}.");
                return false;
            }

            return true;
        }
        private bool _eventosCargados = false;

        private void CargarEventosBitacoraHardcoded()
        {
            if (_eventosCargados) return;

            cmbEvento.Items.Clear();
            cmbEvento.Items.Add("");
            cmbEvento.Items.AddRange(new object[] {
        "APP_START","APP_EXIT","LOGIN_OK","LOGIN_FAIL","LOGIN_BLOQUEADO",
        "LOGOUT","PERMISO_DENEGADO","CAMBIO_PASSWORD",
        "ALTA_USUARIO","BAJA_USUARIO","MODIFICACION_USUARIO"
    });
            cmbEvento.SelectedIndex = 0;
            _eventosCargados = true;
        }

        private void tabRegistrar_Click(object sender, EventArgs e)
        {

        }

        public void CargarGrillaGestionUsuarios()
        {
            var usuarios = BL.UserAdminService.Instancia.ListarUsuarios();

            // agrego una columna calculada "Rol"
            var dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Rol", typeof(string));
            dt.Columns.Add("Activo", typeof(bool));
            dt.Columns.Add("IntentosFallidos", typeof(int));

            foreach (var u in usuarios)
            {
                string rolNombre = u.Roles != null && u.Roles.Count > 0
                    ? string.Join(", ", u.Roles.Select(r => r.Nombre))
                    : "(sin rol)";

                dt.Rows.Add(u.Id, u.Email, u.Nombre, rolNombre, u.Activo, u.IntentosFallidos);
            }

            dgvGestionUsuario.AutoGenerateColumns = true;
            dgvGestionUsuario.DataSource = dt;

            // renombro encabezados
            dgvGestionUsuario.Columns["Id"].HeaderText = "Id";
            dgvGestionUsuario.Columns["Email"].HeaderText = "Email";
            dgvGestionUsuario.Columns["Nombre"].HeaderText = "Nombre";
            dgvGestionUsuario.Columns["Rol"].HeaderText = "Rol";
            dgvGestionUsuario.Columns["Activo"].HeaderText = "Activo";
            dgvGestionUsuario.Columns["IntentosFallidos"].HeaderText = "Intentos Fallidos";

            // ajuste visual
            dgvGestionUsuario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dgvGestionUsuario.CurrentRow == null || dgvGestionUsuario.CurrentRow.Index < 0)
            {
                MessageBox.Show("Seleccioná un usuario primero.");
                return;
            }

            // Asegurate de tener una columna "Id" visible u oculta
            var cell = dgvGestionUsuario.CurrentRow.Cells["Id"];
            if (cell == null || cell.Value == null || !int.TryParse(cell.Value.ToString(), out int usuarioId))
            {
                MessageBox.Show("No se pudo obtener el usuario seleccionado.");
                return;
            }

            // Leé el usuario desde la BL (si querés validar existencia)
            var u = BL.UserAdminService.Instancia.ObtenerUsuarioCompleto(usuarioId);
            if (u == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                return;
            }

            var f = new FormGestionDeUsuario(
                FormGestionDeUsuario.ModoFormulario.Edicion,
                usuarioId: usuarioId);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            var f = new FormGestionDeUsuario(
               FormGestionDeUsuario.ModoFormulario.Alta,
               usuarioId: null);

            f.FormClosed += (_, __) => CargarGrillaGestionUsuarios();
            f.ShowDialog(this);
        }

        private void btnFiltrarCambios_Click(object sender, EventArgs e)
        {
            int? id = int.TryParse(txtCambioId.Text, out var vId) ? vId : (int?)null;
            int? usuarioId = int.TryParse(txtCambioUsuarioId.Text, out var vUid) ? vUid : (int?)null;
            string entidad = string.IsNullOrWhiteSpace(txtCambioEntidad.Text) ? null : txtCambioEntidad.Text.Trim();
            int? entidadId = int.TryParse(txtCambioEntidadId.Text, out var vEid) ? vEid : (int?)null;
            string campo = string.IsNullOrWhiteSpace(txtCambioCampo.Text) ? null : txtCambioCampo.Text.Trim();

            DateTime desdeLocal = dtpCambiosDesde.Value.Date;
            DateTime hastaLocal = dtpCambiosHasta.Value.Date;
            if (!ValidarRangoFechas(desdeLocal, hastaLocal, "el control de cambios"))
                return;

            var desdeUtc = DateTime.SpecifyKind(desdeLocal, DateTimeKind.Local).ToUniversalTime();
            var hastaUtc = DateTime.SpecifyKind(hastaLocal.AddDays(1), DateTimeKind.Local).ToUniversalTime();

            var repo = new ControlCambiosRepository();
            var datos = repo.FiltrarCambios(
                id: id,
                usuarioId: usuarioId,
                entidad: entidad,
                entidadId: entidadId,
                campo: campo,
                desdeUtc: desdeUtc,
                hastaUtcExcl: hastaUtc
            );

            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.DataSource = datos;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiarCambios_Click(object sender, EventArgs e)
        {
            txtCambioUsuarioId.Clear();
            txtCambioId.Clear();
            txtCambioEntidad.Clear();
            txtCambioEntidadId.Clear();
            txtCambioCampo.Clear();

            dtpCambiosDesde.Value = new DateTime(2000, 1, 1);
            dtpCambiosHasta.Value = DateTime.Today.AddDays(1);

            CargarCambiosInicial();
        }
        private void ConfigurarDgvCambios()
        {
            // NO dejamos que se autogenere nada
            dgvCambios.AutoGenerateColumns = false;
            dgvCambios.Columns.Clear();

            // queremos scroll horizontal
            dgvCambios.ScrollBars = ScrollBars.Both;
            dgvCambios.RowHeadersVisible = false;
            dgvCambios.AllowUserToAddRows = false;

            // ahora definimos TODAS las columnas que queremos ver

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 50
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaUtc",
                HeaderText = "Fecha (UTC)",
                DataPropertyName = "FechaUtc",
                Width = 140,
                DefaultCellStyle = { Format = "yyyy-MM-dd HH:mm:ss" }
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UsuarioId",
                HeaderText = "Usuario",
                DataPropertyName = "UsuarioId",
                Width = 70
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Entidad",
                HeaderText = "Entidad",
                DataPropertyName = "Entidad",
                Width = 90
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EntidadId",
                HeaderText = "EntidadId",
                DataPropertyName = "EntidadId",
                Width = 80
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Accion",
                HeaderText = "Acción",
                DataPropertyName = "Accion",
                Width = 100
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Campo",
                HeaderText = "Campo",
                DataPropertyName = "Campo",
                Width = 110
            });

            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorAnterior",
                HeaderText = "Valor anterior",
                DataPropertyName = "ValorAnterior",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // 👇 ESTA es la que te falta ver
            dgvCambios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ValorNuevo",
                HeaderText = "Valor nuevo",
                DataPropertyName = "ValorNuevo",
                Width = 220,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            });

            // por seguridad, que el grid NO vuelva a autoajustar
            dgvCambios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void cmbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdiomas.SelectedValue is string cod && !string.IsNullOrWhiteSpace(cod))
            {
                _idiomaSrv.SeleccionarIdioma(cod);
            }
        }

        private void CargarSelectorIdiomas()
        {
            var idiomas = _idiomaSrv.ListarIdiomas();
            cmbIdiomas.DataSource = idiomas;
            cmbIdiomas.DisplayMember = "Nombre";
            cmbIdiomas.ValueMember = "Codigo";

            if (idiomas.Count == 0)
                return;

            var codActual = IdiomaManager.Instancia.CodigoActual;
            if (!string.IsNullOrWhiteSpace(codActual) && idiomas.Any(i => i.Codigo == codActual))
                cmbIdiomas.SelectedValue = codActual;
            else
                cmbIdiomas.SelectedValue = idiomas.FirstOrDefault(i => i.EsPorDefecto)?.Codigo ?? idiomas.First().Codigo;
        }

        private void FormPrueba_FormClosed(object sender, FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
        }

        private void ActualizarEstadoIntegridadVisual()
        {
            if (lblEstadoIntegridad == null)
                return;

            try
            {
                var (ok, detalle) = BL.VerificadorIntegridadService.Instancia.ValidarTodo();
                if (ok)
                {
                    lblEstadoIntegridad.Text = "Integridad: OK";
                    lblEstadoIntegridad.ForeColor = Color.DarkGreen;
                    lblEstadoIntegridad.Tag = null;
                    _ultimoEstadoIntegridadOk = true;
                }
                else
                {
                    lblEstadoIntegridad.Text = "Integridad: Inconsistencias detectadas";
                    lblEstadoIntegridad.ForeColor = Color.DarkRed;
                    lblEstadoIntegridad.Tag = detalle;

                    if (_ultimoEstadoIntegridadOk != false)
                        RegistrarFallaIntegridad(detalle);
                    _ultimoEstadoIntegridadOk = false;

                    if (_puedeGestionarUsuarios || _puedeGestionarPermisos)
                    {
                        MessageBox.Show("Se detectaron inconsistencias de integridad:\n\n" + detalle,
                            "Integridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                lblEstadoIntegridad.Text = "Integridad: error al verificar";
                lblEstadoIntegridad.ForeColor = Color.DarkOrange;
                lblEstadoIntegridad.Tag = ex.Message;
                _ultimoEstadoIntegridadOk = null;
            }
        }

        private void RegistrarFallaIntegridad(string detalle)
        {
            try
            {
                int? usuarioId = SessionManager.Instancia?.UsuarioActual?.Id;
                new AuditoriaRepository().Registrar("INTEGRIDAD_FALLA", usuarioId, detalle);
            }
            catch
            {
                // No detener la experiencia del usuario si no se puede registrar la bitácora.
            }
        }

        private void InicializarGestionPermisos()
        {
            CargarUsuariosConPermisos();
            CargarRolesYPermisosDisponibles();
            RefrescarPermisosAdministrables();
        }

        private void RefrescarPermisosAdministrables()
        {
            if (cmbPermisosExistentes == null)
                return;

            _permisosPlanos = BL.PermisoService.Instancia.ListarPermisos()
                .OrderBy(p => p.Nombre)
                .ToList();

            var lista = _permisosPlanos
                .Select(p => new BE.PermisoComposite { Id = p.Id, Nombre = p.Nombre, EsCompuesto = p.EsCompuesto })
                .ToList();

            cmbPermisosExistentes.DisplayMember = "Nombre";
            cmbPermisosExistentes.ValueMember = "Id";
            cmbPermisosExistentes.DataSource = lista;

            var padres = _permisosPlanos
                .Where(p => p.EsCompuesto)
                .Select(p => new BE.PermisoComposite { Id = p.Id, Nombre = p.Nombre, EsCompuesto = p.EsCompuesto })
                .ToList();

            cmbPermisoPadre.DisplayMember = "Nombre";
            cmbPermisoPadre.ValueMember = "Id";
            cmbPermisoPadre.DataSource = padres;

            cmbPermisoHijo.DisplayMember = "Nombre";
            cmbPermisoHijo.ValueMember = "Id";
            cmbPermisoHijo.DataSource = lista
                .Select(p => new BE.PermisoComposite { Id = p.Id, Nombre = p.Nombre, EsCompuesto = p.EsCompuesto })
                .ToList();

            if (lista.Count > 0)
                MostrarPermisoSeleccionado(lista.First().Id);
            else
            {
                txtPermisoNombre.Clear();
                chkPermisoEsCompuesto.Checked = false;
            }

            CargarAsignacionPermisosRapidos();
        }

        private void CargarAsignacionPermisosRapidos()
        {
            if (cmbPermisoSimpleRapido == null)
                return;

            var simples = _permisosPlanos
                .Where(p => !p.EsCompuesto)
                .OrderBy(p => p.Nombre)
                .Select(p => new BE.PermisoComposite { Id = p.Id, Nombre = p.Nombre, EsCompuesto = p.EsCompuesto })
                .ToList();

            cmbPermisoSimpleRapido.DisplayMember = "Nombre";
            cmbPermisoSimpleRapido.ValueMember = "Id";
            cmbPermisoSimpleRapido.DataSource = simples;

            if (cmbUsuarioAsignarPermiso != null && btnAsignarPermisoAUsuario != null)
            {
                try
                {
                    var usuarios = BL.UserAdminService.Instancia.ListarUsuarios()
                        .Select(u => new { u.Id, Descripcion = $"{u.Nombre} ({u.Email})" })
                        .OrderBy(u => u.Descripcion)
                        .ToList();
                    cmbUsuarioAsignarPermiso.DisplayMember = "Descripcion";
                    cmbUsuarioAsignarPermiso.ValueMember = "Id";
                    cmbUsuarioAsignarPermiso.DataSource = usuarios;
                    cmbUsuarioAsignarPermiso.Enabled = usuarios.Count > 0;
                    btnAsignarPermisoAUsuario.Enabled = usuarios.Count > 0;
                }
                catch (UnauthorizedAccessException)
                {
                    cmbUsuarioAsignarPermiso.DataSource = null;
                    cmbUsuarioAsignarPermiso.Enabled = false;
                    btnAsignarPermisoAUsuario.Enabled = false;
                }
            }

            if (cmbRolAsignarPermiso != null && btnAsignarPermisoARol != null)
            {
                try
                {
                    var roles = BL.UserAdminService.Instancia.ListarRoles()
                        .Select(r => new { r.Id, Descripcion = r.Nombre })
                        .OrderBy(r => r.Descripcion)
                        .ToList();
                    cmbRolAsignarPermiso.DisplayMember = "Descripcion";
                    cmbRolAsignarPermiso.ValueMember = "Id";
                    cmbRolAsignarPermiso.DataSource = roles;
                    cmbRolAsignarPermiso.Enabled = roles.Count > 0;
                    btnAsignarPermisoARol.Enabled = roles.Count > 0;
                }
                catch (UnauthorizedAccessException)
                {
                    cmbRolAsignarPermiso.DataSource = null;
                    cmbRolAsignarPermiso.Enabled = false;
                    btnAsignarPermisoARol.Enabled = false;
                }
            }
        }

        private void MostrarPermisoSeleccionado(int permisoId)
        {
            var permiso = _permisosPlanos.FirstOrDefault(p => p.Id == permisoId);
            if (permiso == null)
                return;

            txtPermisoNombre.Text = permiso.Nombre;
            chkPermisoEsCompuesto.Checked = permiso.EsCompuesto;
        }

        private void CargarUsuariosConPermisos()
        {
            treeUsuarios.Nodes.Clear();
            var usuarios = BL.UserAdminService.Instancia.ListarUsuarios();

            foreach (var u in usuarios)
            {
                var nodoUsuario = new TreeNode($"{u.Nombre} ({u.Email})") { Tag = u.Id };

                // ===== ROLES =====
                var nodoRoles = new TreeNode("Roles");
                var roles = u.Roles;    // los que vienen de UsuarioRepository / UserAdminService

                foreach (var rol in roles)
                {
                    var nodoRol = new TreeNode(rol.Nombre) { Tag = "ROL_" + rol.Id };

                    // 👇 traigo los permisos que tiene ese rol
                    var permisosDelRol = BL.PermisoService.Instancia.ObtenerPermisosDeRol(rol.Id);
                    // permisosDelRol es List<PermisoComponent>

                    foreach (var comp in permisosDelRol)    // puede haber 1 o varios "árboles" raíz
                    {
                        nodoRol.Nodes.Add(CrearNodoTreeDesdePermiso(comp));
                    }

                    nodoRoles.Nodes.Add(nodoRol);
                }

                nodoUsuario.Nodes.Add(nodoRoles);

                // ===== PERMISOS SUELTOS =====
                var nodoPermisos = new TreeNode("Permisos");

                // esto tiene que estar en PermisoService, no en el form
                var permisosSueltos = BL.PermisoService.Instancia.ObtenerPermisosDirectosDeUsuario(u.Id);

                if (permisosSueltos != null)
                {
                    foreach (var p in permisosSueltos)
                    {
                        nodoPermisos.Nodes.Add(CrearNodoTreeDesdePermiso(p));
                    }
                }

                nodoUsuario.Nodes.Add(nodoPermisos);

                treeUsuarios.Nodes.Add(nodoUsuario);
            }

            treeUsuarios.ExpandAll();
        }
        

        private TreeNode CrearNodoTreeDesdePermiso(BE.PermisoComponent permiso)
        {
            var nodo = new TreeNode(permiso.Nombre)
            {
                Tag = "PERM_" + permiso.Id
            };
            if (permiso is BE.PermisoCompuesto compuesto)
            {
                foreach (var hijo in compuesto.Hijos)
                    nodo.Nodes.Add(CrearNodoTreeDesdePermiso(hijo));
            }
            return nodo;
        }

        private void CargarRolesYPermisosDisponibles()
        {
            treeDisponibles.Nodes.Clear();

            // roles
            var roles = BL.UserAdminService.Instancia.ListarRoles();
            var nodoRoles = new TreeNode("Roles disponibles");
            foreach (var rol in roles)
                nodoRoles.Nodes.Add(new TreeNode(rol.Nombre) { Tag = "ROL_" + rol.Id });

            treeDisponibles.Nodes.Add(nodoRoles);

            // permisos
            var permisos = BL.PermisoService.Instancia.ObtenerArbolPermisos();
            var nodoPermisos = new TreeNode("Permisos disponibles");
            foreach (var p in permisos)
                nodoPermisos.Nodes.Add(CrearNodoTreeDesdeNodo(p));

            treeDisponibles.Nodes.Add(nodoPermisos);
            treeDisponibles.ExpandAll();
        }

        private TreeNode CrearNodoTreeDesdeNodo(BE.PermisoNodo nodo)
        {
            var tn = new TreeNode(nodo.Nombre) { Tag = "PERM_" + nodo.Id };
            foreach (var hijo in nodo.Hijos)
                tn.Nodes.Add(CrearNodoTreeDesdeNodo(hijo));
            return tn;
        }

        private void cmbPermisosExistentes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPermisosExistentes.SelectedValue is int permisoId)
            {
                MostrarPermisoSeleccionado(permisoId);
            }
        }

        private void btnCrearPermiso_Click(object sender, EventArgs e)
        {
            string nombre = txtNuevoPermisoNombre.Text.Trim();
            bool esCompuesto = chkNuevoPermisoCompuesto.Checked;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresá un nombre de permiso.");
                return;
            }

            try
            {
                int id = BL.PermisoService.Instancia.CrearPermiso(nombre, esCompuesto);
                MessageBox.Show($"Permiso creado (Id={id}).");
                txtNuevoPermisoNombre.Clear();
                chkNuevoPermisoCompuesto.Checked = false;
                RefrescarPermisosAdministrables();
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
                if (!esCompuesto && cmbPermisoSimpleRapido != null)
                    cmbPermisoSimpleRapido.SelectedValue = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear permiso: " + ex.Message);
            }
        }

        private void btnAsignarPermisoAUsuario_Click(object sender, EventArgs e)
        {
            if (!(cmbPermisoSimpleRapido?.SelectedValue is int permisoId))
            {
                MessageBox.Show("Seleccioná un permiso simple.");
                return;
            }

            if (!(cmbUsuarioAsignarPermiso?.SelectedValue is int usuarioId))
            {
                MessageBox.Show("Seleccioná un usuario.");
                return;
            }

            try
            {
                BL.PermisoService.Instancia.AsignarPermisoDirectoUsuario(usuarioId, permisoId);
                MessageBox.Show("Permiso asignado al usuario.");
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo asignar el permiso: " + ex.Message);
            }
        }

        private void btnAsignarPermisoARol_Click(object sender, EventArgs e)
        {
            if (!(cmbPermisoSimpleRapido?.SelectedValue is int permisoId))
            {
                MessageBox.Show("Seleccioná un permiso simple.");
                return;
            }

            if (!(cmbRolAsignarPermiso?.SelectedValue is int rolId))
            {
                MessageBox.Show("Seleccioná un rol.");
                return;
            }

            try
            {
                BL.PermisoService.Instancia.AsignarPermisoARol(rolId, permisoId);
                MessageBox.Show("Permiso asignado al rol.");
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo asignar el permiso al rol: " + ex.Message);
            }
        }

        private void btnActualizarPermiso_Click(object sender, EventArgs e)
        {
            if (!(cmbPermisosExistentes.SelectedValue is int permisoId))
            {
                MessageBox.Show("Seleccioná un permiso.");
                return;
            }

            string nombre = txtPermisoNombre.Text.Trim();
            bool esCompuesto = chkPermisoEsCompuesto.Checked;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresá un nombre válido.");
                return;
            }

            try
            {
                BL.PermisoService.Instancia.ActualizarPermiso(permisoId, nombre, esCompuesto);
                MessageBox.Show("Permiso actualizado.");
                RefrescarPermisosAdministrables();
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar permiso: " + ex.Message);
            }
        }

        private void btnAgregarRelacionPermiso_Click(object sender, EventArgs e)
        {
            if (!(cmbPermisoPadre.SelectedValue is int padreId) || !(cmbPermisoHijo.SelectedValue is int hijoId))
            {
                MessageBox.Show("Seleccioná un permiso padre e hijo.");
                return;
            }

            try
            {
                BL.PermisoService.Instancia.AsignarRelacion(padreId, hijoId);
                MessageBox.Show("Relación creada.");
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al relacionar permisos: " + ex.Message);
            }
        }

        private void btnQuitarRelacionPermiso_Click(object sender, EventArgs e)
        {
            if (!(cmbPermisoPadre.SelectedValue is int padreId) || !(cmbPermisoHijo.SelectedValue is int hijoId))
            {
                MessageBox.Show("Seleccioná un permiso padre e hijo.");
                return;
            }

            try
            {
                BL.PermisoService.Instancia.QuitarRelacion(padreId, hijoId);
                MessageBox.Show("Relación eliminada.");
                CargarRolesYPermisosDisponibles();
                CargarUsuariosConPermisos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al quitar la relación: " + ex.Message);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (treeUsuarios.SelectedNode == null || treeDisponibles.SelectedNode == null)
            {
                MessageBox.Show("Seleccioná un usuario y un rol o permiso para asignar.");
                return;
            }

            // usuario seleccionado
            var nodoUsuario = treeUsuarios.SelectedNode;
            while (nodoUsuario.Parent != null)
                nodoUsuario = nodoUsuario.Parent;

            int usuarioId = (int)nodoUsuario.Tag;

            // item seleccionado
            string tag = treeDisponibles.SelectedNode.Tag?.ToString();
            if (tag == null) return;

            if (tag.StartsWith("ROL_"))
            {
                int rolId = int.Parse(tag.Substring(4));
                new DAO.UsuarioAdminRepository().ReemplazarRolesUsuario(usuarioId, new[] { rolId });
            }
            else if (tag.StartsWith("PERM_"))
            {
                int permisoId = int.Parse(tag.Substring(5));

                // verificar si ya está incluido en un rol del usuario
                var permisosUsuario = BL.PermisoService.Instancia.ObtenerPermisosDeUsuario(usuarioId);
                if (permisosUsuario.TienePermiso(treeDisponibles.SelectedNode.Text))
                {
                    MessageBox.Show("Ese permiso ya está incluido en un rol asignado.");
                    return;
                }

                BL.PermisoService.Instancia.AsignarPermisoDirectoUsuario(usuarioId, permisoId);

            }

            MessageBox.Show("Asignación realizada con éxito.");
            CargarUsuariosConPermisos();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (treeUsuarios.SelectedNode == null)
            {
                MessageBox.Show("Seleccioná un rol o permiso del usuario para quitar.");
                return;
            }

            TreeNode nodo = treeUsuarios.SelectedNode;
            TreeNode nodoUsuario = nodo;
            while (nodoUsuario.Parent != null)
                nodoUsuario = nodoUsuario.Parent;

            int usuarioId = (int)nodoUsuario.Tag;
            string tag = nodo.Tag?.ToString();
            if (tag == null) return;

            if (tag.StartsWith("ROL_"))
            {
                int rolId = int.Parse(tag.Substring(4));

                // obtenemos los roles actuales y removemos este
                var usuarioRepo = new DAO.UsuarioRepository();
                var rolesActuales = usuarioRepo.GetRoles(usuarioId);
                var nuevosRoles = rolesActuales.Where(r => r.Id != rolId).Select(r => r.Id).ToList();

                // actualizamos la relación
                BL.UserAdminService.Instancia.ActualizarRolesUsuario(usuarioId, nuevosRoles);

            }
            else if (tag.StartsWith("PERM_"))
            {
                int permisoId = int.Parse(tag.Substring(5));
                BL.PermisoService.Instancia.QuitarPermisoDirectoUsuario(usuarioId, permisoId);

            }

            MessageBox.Show("Eliminación realizada.");
            CargarUsuariosConPermisos();

        }

        private void ConfigurarGrillaIdiomas()
        {
            if (dgvIdiomasAdmin == null || dgvIdiomasAdmin.Columns.Count > 0)
                return;

            dgvIdiomasAdmin.AutoGenerateColumns = false;
            dgvIdiomasAdmin.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                Name = "Id",
                Visible = false
            });
            dgvIdiomasAdmin.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Codigo",
                Name = "Codigo",
                HeaderText = "Código",
                Width = 80
            });
            dgvIdiomasAdmin.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Nombre",
                Name = "Nombre",
                HeaderText = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvIdiomasAdmin.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "EsPorDefecto",
                Name = "EsPorDefecto",
                HeaderText = "Por defecto",
                Width = 80
            });
        }

        private void ConfigurarGrillaLeyendas()
        {
            if (dgvLeyendas == null || dgvLeyendas.Columns.Count > 0)
                return;

            dgvLeyendas.AutoGenerateColumns = false;
            dgvLeyendas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EtiquetaId",
                Name = "EtiquetaId",
                Visible = false
            });
            dgvLeyendas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Clave",
                Name = "Clave",
                HeaderText = "Clave",
                Width = 120
            });
            dgvLeyendas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Descripcion",
                Name = "Descripcion",
                HeaderText = "Descripción",
                Width = 180
            });
            dgvLeyendas.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Texto",
                Name = "Texto",
                HeaderText = "Texto",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void CargarIdiomasAdmin()
        {
            if (dgvIdiomasAdmin == null)
                return;

            ConfigurarGrillaIdiomas();
            ConfigurarGrillaLeyendas();

            _idiomasAdmin.Clear();
            _idiomasAdmin.AddRange(_idiomaSrv.ListarIdiomas());

            dgvIdiomasAdmin.DataSource = null;
            dgvIdiomasAdmin.DataSource = _idiomasAdmin
                .Select(i => new BE.Idioma { Id = i.Id, Codigo = i.Codigo, Nombre = i.Nombre, EsPorDefecto = i.EsPorDefecto })
                .ToList();

            if (dgvIdiomasAdmin.Rows.Count > 0)
            {
                dgvIdiomasAdmin.Rows[0].Selected = true;
                MostrarIdiomaSeleccionado();
            }
            else
            {
                lblIdiomaSeleccionadoAdmin.Tag = null;
                txtIdiomaCodigo.ReadOnly = false;
                txtIdiomaCodigo.Clear();
                txtIdiomaNombre.Clear();
                chkIdiomaPorDefecto.Checked = false;
                dgvLeyendas.DataSource = null;
            }
        }

        private void AplicarRestriccionIdiomas()
        {
            bool habilitado = _esAdministrador;
            if (btnCrearIdioma != null)
                btnCrearIdioma.Enabled = habilitado;
            if (btnNuevoIdioma != null)
                btnNuevoIdioma.Enabled = habilitado;
            if (txtIdiomaCodigo != null)
                txtIdiomaCodigo.Enabled = habilitado;
        }

        private void MostrarIdiomaSeleccionado()
        {
            if (dgvIdiomasAdmin?.CurrentRow == null)
                return;

            var cell = dgvIdiomasAdmin.CurrentRow.Cells["Id"];
            if (cell == null || cell.Value == null)
                return;

            int idiomaId = Convert.ToInt32(cell.Value);
            var idioma = _idiomasAdmin.FirstOrDefault(i => i.Id == idiomaId);
            if (idioma == null)
                return;

            lblIdiomaSeleccionadoAdmin.Text = $"Idioma seleccionado: {idioma.Nombre}";
            lblIdiomaSeleccionadoAdmin.Tag = idioma.Id;
            txtIdiomaCodigo.Text = idioma.Codigo;
            txtIdiomaCodigo.ReadOnly = true;
            txtIdiomaNombre.Text = idioma.Nombre;
            chkIdiomaPorDefecto.Checked = idioma.EsPorDefecto;

            CargarLeyendasIdioma(idioma.Id);
        }

        private void CargarLeyendasIdioma(int idiomaId)
        {
            if (dgvLeyendas == null)
                return;

            ConfigurarGrillaLeyendas();
            _leyendasActuales = _idiomaSrv.ListarLeyendas(idiomaId);

            dgvLeyendas.DataSource = null;
            dgvLeyendas.DataSource = _leyendasActuales
                .Select(l => new
                {
                    l.EtiquetaId,
                    l.Clave,
                    l.Descripcion,
                    l.Texto
                }).ToList();

            if (dgvLeyendas.Rows.Count > 0)
            {
                dgvLeyendas.Rows[0].Selected = true;
                MostrarLeyendaSeleccionada();
            }
            else
            {
                txtLeyendaClave.Clear();
                txtLeyendaDescripcion.Clear();
                txtLeyendaTexto.Clear();
            }
        }

        private void MostrarLeyendaSeleccionada()
        {
            if (dgvLeyendas?.CurrentRow == null)
                return;

            var row = dgvLeyendas.CurrentRow;
            txtLeyendaClave.Text = row.Cells["Clave"].Value?.ToString() ?? string.Empty;
            txtLeyendaDescripcion.Text = row.Cells["Descripcion"].Value?.ToString() ?? string.Empty;
            txtLeyendaTexto.Text = row.Cells["Texto"].Value?.ToString() ?? string.Empty;
        }

        private void dgvIdiomasAdmin_SelectionChanged(object sender, EventArgs e)
        {
            MostrarIdiomaSeleccionado();
        }

        private void dgvLeyendas_SelectionChanged(object sender, EventArgs e)
        {
            MostrarLeyendaSeleccionada();
        }

        private void btnCrearIdioma_Click(object sender, EventArgs e)
        {
            if (!_esAdministrador)
            {
                MessageBox.Show("Solo un administrador puede crear idiomas.");
                return;
            }

            string codigo = txtIdiomaCodigo.Text.Trim();
            string nombre = txtIdiomaNombre.Text.Trim();
            bool esPorDefecto = chkIdiomaPorDefecto.Checked;

            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Completá código y nombre del idioma.");
                return;
            }

            try
            {
                int nuevoId = _idiomaSrv.CrearIdioma(codigo, nombre, esPorDefecto);
                MessageBox.Show($"Idioma creado (Id={nuevoId}).");
                CargarIdiomasAdmin();
                CargarSelectorIdiomas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el idioma: " + ex.Message);
            }
        }

        private void btnActualizarIdioma_Click(object sender, EventArgs e)
        {
            if (!(lblIdiomaSeleccionadoAdmin.Tag is int idiomaId))
            {
                MessageBox.Show("Seleccioná un idioma de la lista.");
                return;
            }

            string nombre = txtIdiomaNombre.Text.Trim();
            bool esPorDefecto = chkIdiomaPorDefecto.Checked;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresá un nombre válido.");
                return;
            }

            try
            {
                _idiomaSrv.ActualizarIdioma(idiomaId, nombre, esPorDefecto);
                MessageBox.Show("Idioma actualizado.");
                CargarIdiomasAdmin();
                CargarSelectorIdiomas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el idioma: " + ex.Message);
            }
        }

        private void btnNuevoIdioma_Click(object sender, EventArgs e)
        {
            if (!_esAdministrador)
            {
                MessageBox.Show("Solo un administrador puede crear idiomas.");
                return;
            }

            dgvIdiomasAdmin?.ClearSelection();
            lblIdiomaSeleccionadoAdmin.Tag = null;
            var ultima = IdiomaManager.Instancia.ObtenerUltimaTraduccion();
            lblIdiomaSeleccionadoAdmin.Text = Texto(ultima, "LBL_IDIOMA_SELECCIONADO");
            txtIdiomaCodigo.ReadOnly = false;
            txtIdiomaCodigo.Clear();
            txtIdiomaNombre.Clear();
            chkIdiomaPorDefecto.Checked = false;
            dgvLeyendas.DataSource = null;
        }

        private void btnGuardarLeyenda_Click(object sender, EventArgs e)
        {
            if (!(lblIdiomaSeleccionadoAdmin.Tag is int idiomaId))
            {
                MessageBox.Show("Seleccioná un idioma primero.");
                return;
            }

            string clave = txtLeyendaClave.Text.Trim();
            string descripcion = txtLeyendaDescripcion.Text.Trim();
            string texto = txtLeyendaTexto.Text.Trim();

            if (string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("Ingresá la clave de la leyenda.");
                return;
            }

            try
            {
                _idiomaSrv.GuardarLeyenda(idiomaId, clave, descripcion, texto);
                MessageBox.Show("Leyenda guardada.");
                CargarLeyendasIdioma(idiomaId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la leyenda: " + ex.Message);
            }
        }

        private void btnNuevaLeyenda_Click(object sender, EventArgs e)
        {
            txtLeyendaClave.Clear();
            txtLeyendaDescripcion.Clear();
            txtLeyendaTexto.Clear();
            txtLeyendaClave.Focus();
        }

       

        private void treeUsuarios_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            // Busco el nodo raíz (el usuario)
            TreeNode nodo = e.Node;
            while (nodo.Parent != null)
                nodo = nodo.Parent;

            if (nodo.Tag is int usuarioId)
            {
                lblUsuarioSel.Text = $"Usuario seleccionado: {nodo.Text}";
                lblUsuarioSel.Tag = usuarioId; // guardo el id para reutilizar
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvGestionUsuario.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un usuario primero.");
                return;
            }

            var fila = dgvGestionUsuario.CurrentRow;
            int usuarioId = Convert.ToInt32(fila.Cells["Id"].Value);
            string email = fila.Cells["Email"].Value.ToString();

            var confirmar = MessageBox.Show(
                $"¿Seguro que querés eliminar al usuario '{email}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmar != DialogResult.Yes)
                return;

            try
            {
                BL.UserAdminService.Instancia.BajaLogicaUsuario(usuarioId);
                MessageBox.Show("Usuario eliminado correctamente.");
                CargarGrillaGestionUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el usuario: " + ex.Message);
            }
        }

        private void btnVerificarIntegridad_Click(object sender, EventArgs e)
        {
            ActualizarEstadoIntegridadVisual();
        }

        private void btnRecalcularIntegridad_Click(object sender, EventArgs e)
        {
            try
            {
                BL.VerificadorIntegridadService.Instancia.RecalcularTodo();
                MessageBox.Show("Recalibrado completo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recalcular los dígitos: " + ex.Message);
            }

            ActualizarEstadoIntegridadVisual();
        }

        private void lblEstadoIntegridad_Click(object sender, EventArgs e)
        {
            if (lblEstadoIntegridad?.Tag is string detalle && !string.IsNullOrWhiteSpace(detalle))
            {
                MessageBox.Show(detalle, "Detalle de integridad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void treeDisponibles_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}


