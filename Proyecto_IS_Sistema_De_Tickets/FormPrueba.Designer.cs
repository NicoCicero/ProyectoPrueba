using System;
using System.Windows.Forms;

namespace Proyecto_IS_Sistema_De_Tickets
{
    partial class FormPrueba
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabGeneral = new System.Windows.Forms.TabControl();
            this.tabMenuPrincipal = new System.Windows.Forms.TabPage();
            this.cmbIdiomas = new System.Windows.Forms.ComboBox();
            this.tabUsuarios = new System.Windows.Forms.TabPage();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevoRegistro = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.dgvGestionUsuario = new System.Windows.Forms.DataGridView();
            this.tabBitacora = new System.Windows.Forms.TabPage();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblAuditoriaId = new System.Windows.Forms.Label();
            this.lblUsuarioId = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtAuditoriaId = new System.Windows.Forms.TextBox();
            this.btnFiltrarBitacora = new System.Windows.Forms.Button();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.dgvBitacora = new System.Windows.Forms.DataGridView();
            this.tabControlCambios = new System.Windows.Forms.TabPage();
            this.lblCambioCampo = new System.Windows.Forms.Label();
            this.txtCambioCampo = new System.Windows.Forms.TextBox();
            this.txtCambioEntidad = new System.Windows.Forms.TextBox();
            this.btnLimpiarCambios = new System.Windows.Forms.Button();
            this.lblCambioFecha = new System.Windows.Forms.Label();
            this.lblCambioEntidadId = new System.Windows.Forms.Label();
            this.lblCambioEntidad = new System.Windows.Forms.Label();
            this.lblCambioId = new System.Windows.Forms.Label();
            this.lblCambioUsuarioId = new System.Windows.Forms.Label();
            this.txtCambioId = new System.Windows.Forms.TextBox();
            this.btnFiltrarCambios = new System.Windows.Forms.Button();
            this.dtpCambiosHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpCambiosDesde = new System.Windows.Forms.DateTimePicker();
            this.txtCambioEntidadId = new System.Windows.Forms.TextBox();
            this.txtCambioUsuarioId = new System.Windows.Forms.TextBox();
            this.dgvCambios = new System.Windows.Forms.DataGridView();
            this.tabPermisos = new System.Windows.Forms.TabPage();
            this.grpPermisosAdmin = new System.Windows.Forms.GroupBox();
            this.btnQuitarRelacionPermiso = new System.Windows.Forms.Button();
            this.btnAgregarRelacionPermiso = new System.Windows.Forms.Button();
            this.btnCrearPermiso = new System.Windows.Forms.Button();
            this.btnActualizarPermiso = new System.Windows.Forms.Button();
            this.chkNuevoPermisoCompuesto = new System.Windows.Forms.CheckBox();
            this.chkPermisoEsCompuesto = new System.Windows.Forms.CheckBox();
            this.cmbPermisoHijo = new System.Windows.Forms.ComboBox();
            this.cmbPermisoPadre = new System.Windows.Forms.ComboBox();
            this.txtNuevoPermisoNombre = new System.Windows.Forms.TextBox();
            this.txtPermisoNombre = new System.Windows.Forms.TextBox();
            this.lblPermisoHijo = new System.Windows.Forms.Label();
            this.lblPermisoPadre = new System.Windows.Forms.Label();
            this.lblNuevoPermiso = new System.Windows.Forms.Label();
            this.lblPermisoNombre = new System.Windows.Forms.Label();
            this.lblPermisoExistente = new System.Windows.Forms.Label();
            this.cmbPermisosExistentes = new System.Windows.Forms.ComboBox();
            this.lblUsuarioSel = new System.Windows.Forms.Label();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.treeDisponibles = new System.Windows.Forms.TreeView();
            this.treeUsuarios = new System.Windows.Forms.TreeView();
            this.tabIdiomas = new System.Windows.Forms.TabPage();
            this.btnNuevaLeyenda = new System.Windows.Forms.Button();
            this.btnGuardarLeyenda = new System.Windows.Forms.Button();
            this.txtLeyendaTexto = new System.Windows.Forms.TextBox();
            this.lblLeyendaTexto = new System.Windows.Forms.Label();
            this.txtLeyendaDescripcion = new System.Windows.Forms.TextBox();
            this.lblLeyendaDescripcion = new System.Windows.Forms.Label();
            this.txtLeyendaClave = new System.Windows.Forms.TextBox();
            this.lblLeyendaClave = new System.Windows.Forms.Label();
            this.dgvLeyendas = new System.Windows.Forms.DataGridView();
            this.btnNuevoIdioma = new System.Windows.Forms.Button();
            this.btnActualizarIdioma = new System.Windows.Forms.Button();
            this.btnCrearIdioma = new System.Windows.Forms.Button();
            this.chkIdiomaPorDefecto = new System.Windows.Forms.CheckBox();
            this.lblIdiomaPorDefecto = new System.Windows.Forms.Label();
            this.txtIdiomaNombre = new System.Windows.Forms.TextBox();
            this.lblIdiomaNombre = new System.Windows.Forms.Label();
            this.txtIdiomaCodigo = new System.Windows.Forms.TextBox();
            this.lblIdiomaCodigo = new System.Windows.Forms.Label();
            this.dgvIdiomasAdmin = new System.Windows.Forms.DataGridView();
            this.lblIdiomaSeleccionadoAdmin = new System.Windows.Forms.Label();
            this.lblEstadoIntegridad = new System.Windows.Forms.Label();
            this.btnVerificarIntegridad = new System.Windows.Forms.Button();
            this.btnRecalcularIntegridad = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tabGeneral.SuspendLayout();
            this.tabMenuPrincipal.SuspendLayout();
            this.tabUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionUsuario)).BeginInit();
            this.tabBitacora.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).BeginInit();
            this.tabControlCambios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).BeginInit();
            this.tabPermisos.SuspendLayout();
            this.grpPermisosAdmin.SuspendLayout();
            this.tabIdiomas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeyendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomasAdmin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(12, 417);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(108, 23);
            this.btnCerrarSesion.TabIndex = 0;
            this.btnCerrarSesion.Text = "Cerrar Sesion";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabGeneral.Controls.Add(this.tabMenuPrincipal);
            this.tabGeneral.Controls.Add(this.tabUsuarios);
            this.tabGeneral.Controls.Add(this.tabBitacora);
            this.tabGeneral.Controls.Add(this.tabControlCambios);
            this.tabGeneral.Controls.Add(this.tabPermisos);
            this.tabGeneral.Controls.Add(this.tabIdiomas);
            this.tabGeneral.Location = new System.Drawing.Point(9, 2);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.SelectedIndex = 0;
            this.tabGeneral.Size = new System.Drawing.Size(768, 409);
            this.tabGeneral.TabIndex = 17;
            this.tabGeneral.SelectedIndexChanged += new System.EventHandler(this.TabGeneral_SelectedIndexChanged);
            // 
            // tabMenuPrincipal
            //
            this.tabMenuPrincipal.Controls.Add(this.btnRecalcularIntegridad);
            this.tabMenuPrincipal.Controls.Add(this.btnVerificarIntegridad);
            this.tabMenuPrincipal.Controls.Add(this.lblEstadoIntegridad);
            this.tabMenuPrincipal.Controls.Add(this.cmbIdiomas);
            this.tabMenuPrincipal.Location = new System.Drawing.Point(4, 22);
            this.tabMenuPrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.tabMenuPrincipal.Name = "tabMenuPrincipal";
            this.tabMenuPrincipal.Padding = new System.Windows.Forms.Padding(2);
            this.tabMenuPrincipal.Size = new System.Drawing.Size(760, 383);
            this.tabMenuPrincipal.TabIndex = 2;
            this.tabMenuPrincipal.Text = "Menu Principal";
            this.tabMenuPrincipal.UseVisualStyleBackColor = true;
            // 
            // cmbIdiomas
            // 
            this.cmbIdiomas.FormattingEnabled = true;
            this.cmbIdiomas.Location = new System.Drawing.Point(616, 5);
            this.cmbIdiomas.Name = "cmbIdiomas";
            this.cmbIdiomas.Size = new System.Drawing.Size(139, 21);
            this.cmbIdiomas.TabIndex = 0;
            this.cmbIdiomas.SelectedIndexChanged += new System.EventHandler(this.cmbIdiomas_SelectedIndexChanged);
            //
            // lblEstadoIntegridad
            //
            this.lblEstadoIntegridad.AutoSize = true;
            this.lblEstadoIntegridad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEstadoIntegridad.Location = new System.Drawing.Point(18, 10);
            this.lblEstadoIntegridad.Name = "lblEstadoIntegridad";
            this.lblEstadoIntegridad.Size = new System.Drawing.Size(125, 13);
            this.lblEstadoIntegridad.TabIndex = 1;
            this.lblEstadoIntegridad.Text = "Integridad: pendiente...";
            this.lblEstadoIntegridad.Click += new System.EventHandler(this.lblEstadoIntegridad_Click);
            //
            // btnVerificarIntegridad
            //
            this.btnVerificarIntegridad.Location = new System.Drawing.Point(21, 35);
            this.btnVerificarIntegridad.Name = "btnVerificarIntegridad";
            this.btnVerificarIntegridad.Size = new System.Drawing.Size(120, 23);
            this.btnVerificarIntegridad.TabIndex = 2;
            this.btnVerificarIntegridad.Text = "Verificar integridad";
            this.btnVerificarIntegridad.UseVisualStyleBackColor = true;
            this.btnVerificarIntegridad.Click += new System.EventHandler(this.btnVerificarIntegridad_Click);
            //
            // btnRecalcularIntegridad
            //
            this.btnRecalcularIntegridad.Location = new System.Drawing.Point(147, 35);
            this.btnRecalcularIntegridad.Name = "btnRecalcularIntegridad";
            this.btnRecalcularIntegridad.Size = new System.Drawing.Size(148, 23);
            this.btnRecalcularIntegridad.TabIndex = 3;
            this.btnRecalcularIntegridad.Text = "Recalcular verificadores";
            this.btnRecalcularIntegridad.UseVisualStyleBackColor = true;
            this.btnRecalcularIntegridad.Click += new System.EventHandler(this.btnRecalcularIntegridad_Click);
            // 
            // tabUsuarios
            // 
            this.tabUsuarios.Controls.Add(this.btnEliminar);
            this.tabUsuarios.Controls.Add(this.btnNuevoRegistro);
            this.tabUsuarios.Controls.Add(this.btnActualizar);
            this.tabUsuarios.Controls.Add(this.dgvGestionUsuario);
            this.tabUsuarios.Location = new System.Drawing.Point(4, 22);
            this.tabUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.tabUsuarios.Name = "tabUsuarios";
            this.tabUsuarios.Padding = new System.Windows.Forms.Padding(2);
            this.tabUsuarios.Size = new System.Drawing.Size(760, 383);
            this.tabUsuarios.TabIndex = 0;
            this.tabUsuarios.Text = "Usuarios";
            this.tabUsuarios.UseVisualStyleBackColor = true;
            this.tabUsuarios.Click += new System.EventHandler(this.tabRegistrar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(578, 310);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(96, 23);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar Usuario";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevoRegistro
            // 
            this.btnNuevoRegistro.Location = new System.Drawing.Point(385, 310);
            this.btnNuevoRegistro.Name = "btnNuevoRegistro";
            this.btnNuevoRegistro.Size = new System.Drawing.Size(96, 23);
            this.btnNuevoRegistro.TabIndex = 2;
            this.btnNuevoRegistro.Text = "Nuevo Registro";
            this.btnNuevoRegistro.UseVisualStyleBackColor = true;
            this.btnNuevoRegistro.Click += new System.EventHandler(this.btnNuevoRegistro_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(254, 310);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(96, 23);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // dgvGestionUsuario
            // 
            this.dgvGestionUsuario.AllowUserToAddRows = false;
            this.dgvGestionUsuario.AllowUserToDeleteRows = false;
            this.dgvGestionUsuario.AllowUserToResizeColumns = false;
            this.dgvGestionUsuario.AllowUserToResizeRows = false;
            this.dgvGestionUsuario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGestionUsuario.Location = new System.Drawing.Point(68, 30);
            this.dgvGestionUsuario.Name = "dgvGestionUsuario";
            this.dgvGestionUsuario.Size = new System.Drawing.Size(606, 274);
            this.dgvGestionUsuario.TabIndex = 0;
            // 
            // tabBitacora
            // 
            this.tabBitacora.Controls.Add(this.lblFecha);
            this.tabBitacora.Controls.Add(this.lblDetalle);
            this.tabBitacora.Controls.Add(this.lblEvento);
            this.tabBitacora.Controls.Add(this.lblAuditoriaId);
            this.tabBitacora.Controls.Add(this.lblUsuarioId);
            this.tabBitacora.Controls.Add(this.btnLimpiar);
            this.tabBitacora.Controls.Add(this.txtAuditoriaId);
            this.tabBitacora.Controls.Add(this.btnFiltrarBitacora);
            this.tabBitacora.Controls.Add(this.dtpHasta);
            this.tabBitacora.Controls.Add(this.dtpDesde);
            this.tabBitacora.Controls.Add(this.cmbEvento);
            this.tabBitacora.Controls.Add(this.txtTexto);
            this.tabBitacora.Controls.Add(this.txtId);
            this.tabBitacora.Controls.Add(this.dgvBitacora);
            this.tabBitacora.Location = new System.Drawing.Point(4, 22);
            this.tabBitacora.Margin = new System.Windows.Forms.Padding(2);
            this.tabBitacora.Name = "tabBitacora";
            this.tabBitacora.Padding = new System.Windows.Forms.Padding(2);
            this.tabBitacora.Size = new System.Drawing.Size(760, 383);
            this.tabBitacora.TabIndex = 1;
            this.tabBitacora.Text = "Bitacora";
            this.tabBitacora.UseVisualStyleBackColor = true;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(604, 258);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 18;
            this.lblFecha.Text = "Fecha";
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Location = new System.Drawing.Point(604, 185);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(40, 13);
            this.lblDetalle.TabIndex = 17;
            this.lblDetalle.Text = "Detalle";
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Location = new System.Drawing.Point(604, 133);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(41, 13);
            this.lblEvento.TabIndex = 16;
            this.lblEvento.Text = "Evento";
            // 
            // lblAuditoriaId
            // 
            this.lblAuditoriaId.AutoSize = true;
            this.lblAuditoriaId.Location = new System.Drawing.Point(604, 77);
            this.lblAuditoriaId.Name = "lblAuditoriaId";
            this.lblAuditoriaId.Size = new System.Drawing.Size(60, 13);
            this.lblAuditoriaId.TabIndex = 15;
            this.lblAuditoriaId.Text = "Auditoria Id";
            // 
            // lblUsuarioId
            // 
            this.lblUsuarioId.AutoSize = true;
            this.lblUsuarioId.Location = new System.Drawing.Point(604, 14);
            this.lblUsuarioId.Name = "lblUsuarioId";
            this.lblUsuarioId.Size = new System.Drawing.Size(55, 13);
            this.lblUsuarioId.TabIndex = 14;
            this.lblUsuarioId.Text = "Usuario Id";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(684, 347);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(56, 19);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtAuditoriaId
            // 
            this.txtAuditoriaId.Location = new System.Drawing.Point(603, 102);
            this.txtAuditoriaId.Margin = new System.Windows.Forms.Padding(2);
            this.txtAuditoriaId.Name = "txtAuditoriaId";
            this.txtAuditoriaId.Size = new System.Drawing.Size(94, 20);
            this.txtAuditoriaId.TabIndex = 12;
            // 
            // btnFiltrarBitacora
            // 
            this.btnFiltrarBitacora.Location = new System.Drawing.Point(610, 347);
            this.btnFiltrarBitacora.Margin = new System.Windows.Forms.Padding(2);
            this.btnFiltrarBitacora.Name = "btnFiltrarBitacora";
            this.btnFiltrarBitacora.Size = new System.Drawing.Size(56, 19);
            this.btnFiltrarBitacora.TabIndex = 10;
            this.btnFiltrarBitacora.Text = "Filtrar";
            this.btnFiltrarBitacora.UseVisualStyleBackColor = true;
            this.btnFiltrarBitacora.Click += new System.EventHandler(this.btnFiltrarBitacora_Click);
            // 
            // dtpHasta
            // 
            this.dtpHasta.Location = new System.Drawing.Point(605, 311);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(151, 20);
            this.dtpHasta.TabIndex = 9;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Location = new System.Drawing.Point(605, 287);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(151, 20);
            this.dtpDesde.TabIndex = 8;
            // 
            // cmbEvento
            // 
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(605, 148);
            this.cmbEvento.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(92, 21);
            this.cmbEvento.TabIndex = 6;
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(605, 209);
            this.txtTexto.Margin = new System.Windows.Forms.Padding(2);
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.Size = new System.Drawing.Size(92, 20);
            this.txtTexto.TabIndex = 5;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(605, 45);
            this.txtId.Margin = new System.Windows.Forms.Padding(2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(92, 20);
            this.txtId.TabIndex = 1;
            // 
            // dgvBitacora
            // 
            this.dgvBitacora.AllowUserToAddRows = false;
            this.dgvBitacora.AllowUserToDeleteRows = false;
            this.dgvBitacora.AllowUserToOrderColumns = true;
            this.dgvBitacora.AllowUserToResizeColumns = false;
            this.dgvBitacora.AllowUserToResizeRows = false;
            this.dgvBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBitacora.Location = new System.Drawing.Point(20, 14);
            this.dgvBitacora.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBitacora.Name = "dgvBitacora";
            this.dgvBitacora.RowHeadersWidth = 51;
            this.dgvBitacora.RowTemplate.Height = 24;
            this.dgvBitacora.Size = new System.Drawing.Size(569, 352);
            this.dgvBitacora.TabIndex = 0;
            this.dgvBitacora.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabControlCambios
            // 
            this.tabControlCambios.Controls.Add(this.lblCambioCampo);
            this.tabControlCambios.Controls.Add(this.txtCambioCampo);
            this.tabControlCambios.Controls.Add(this.txtCambioEntidad);
            this.tabControlCambios.Controls.Add(this.btnLimpiarCambios);
            this.tabControlCambios.Controls.Add(this.lblCambioFecha);
            this.tabControlCambios.Controls.Add(this.lblCambioEntidadId);
            this.tabControlCambios.Controls.Add(this.lblCambioEntidad);
            this.tabControlCambios.Controls.Add(this.lblCambioId);
            this.tabControlCambios.Controls.Add(this.lblCambioUsuarioId);
            this.tabControlCambios.Controls.Add(this.txtCambioId);
            this.tabControlCambios.Controls.Add(this.btnFiltrarCambios);
            this.tabControlCambios.Controls.Add(this.dtpCambiosHasta);
            this.tabControlCambios.Controls.Add(this.dtpCambiosDesde);
            this.tabControlCambios.Controls.Add(this.txtCambioEntidadId);
            this.tabControlCambios.Controls.Add(this.txtCambioUsuarioId);
            this.tabControlCambios.Controls.Add(this.dgvCambios);
            this.tabControlCambios.Location = new System.Drawing.Point(4, 22);
            this.tabControlCambios.Name = "tabControlCambios";
            this.tabControlCambios.Size = new System.Drawing.Size(760, 383);
            this.tabControlCambios.TabIndex = 3;
            this.tabControlCambios.Text = "Control Cambios";
            this.tabControlCambios.UseVisualStyleBackColor = true;
            // 
            // lblCambioCampo
            // 
            this.lblCambioCampo.AutoSize = true;
            this.lblCambioCampo.Location = new System.Drawing.Point(600, 219);
            this.lblCambioCampo.Name = "lblCambioCampo";
            this.lblCambioCampo.Size = new System.Drawing.Size(78, 13);
            this.lblCambioCampo.TabIndex = 35;
            this.lblCambioCampo.Text = "Cambio Campo";
            // 
            // txtCambioCampo
            // 
            this.txtCambioCampo.Location = new System.Drawing.Point(603, 234);
            this.txtCambioCampo.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambioCampo.Name = "txtCambioCampo";
            this.txtCambioCampo.Size = new System.Drawing.Size(92, 20);
            this.txtCambioCampo.TabIndex = 34;
            // 
            // txtCambioEntidad
            // 
            this.txtCambioEntidad.Location = new System.Drawing.Point(601, 131);
            this.txtCambioEntidad.Name = "txtCambioEntidad";
            this.txtCambioEntidad.Size = new System.Drawing.Size(100, 20);
            this.txtCambioEntidad.TabIndex = 33;
            // 
            // btnLimpiarCambios
            // 
            this.btnLimpiarCambios.Location = new System.Drawing.Point(686, 340);
            this.btnLimpiarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiarCambios.Name = "btnLimpiarCambios";
            this.btnLimpiarCambios.Size = new System.Drawing.Size(56, 19);
            this.btnLimpiarCambios.TabIndex = 32;
            this.btnLimpiarCambios.Text = "Limpiar";
            this.btnLimpiarCambios.UseVisualStyleBackColor = true;
            this.btnLimpiarCambios.Click += new System.EventHandler(this.btnLimpiarCambios_Click);
            // 
            // lblCambioFecha
            // 
            this.lblCambioFecha.AutoSize = true;
            this.lblCambioFecha.Location = new System.Drawing.Point(600, 265);
            this.lblCambioFecha.Name = "lblCambioFecha";
            this.lblCambioFecha.Size = new System.Drawing.Size(37, 13);
            this.lblCambioFecha.TabIndex = 31;
            this.lblCambioFecha.Text = "Fecha";
            // 
            // lblCambioEntidadId
            // 
            this.lblCambioEntidadId.AutoSize = true;
            this.lblCambioEntidadId.Location = new System.Drawing.Point(602, 166);
            this.lblCambioEntidadId.Name = "lblCambioEntidadId";
            this.lblCambioEntidadId.Size = new System.Drawing.Size(87, 13);
            this.lblCambioEntidadId.TabIndex = 30;
            this.lblCambioEntidadId.Text = "Cambio Entida Id";
            // 
            // lblCambioEntidad
            // 
            this.lblCambioEntidad.AutoSize = true;
            this.lblCambioEntidad.Location = new System.Drawing.Point(598, 115);
            this.lblCambioEntidad.Name = "lblCambioEntidad";
            this.lblCambioEntidad.Size = new System.Drawing.Size(81, 13);
            this.lblCambioEntidad.TabIndex = 29;
            this.lblCambioEntidad.Text = "Cambio Entidad";
            // 
            // lblCambioId
            // 
            this.lblCambioId.AutoSize = true;
            this.lblCambioId.Location = new System.Drawing.Point(600, 15);
            this.lblCambioId.Name = "lblCambioId";
            this.lblCambioId.Size = new System.Drawing.Size(54, 13);
            this.lblCambioId.TabIndex = 28;
            this.lblCambioId.Text = "Cambio Id";
            // 
            // lblCambioUsuarioId
            // 
            this.lblCambioUsuarioId.AutoSize = true;
            this.lblCambioUsuarioId.Location = new System.Drawing.Point(598, 65);
            this.lblCambioUsuarioId.Name = "lblCambioUsuarioId";
            this.lblCambioUsuarioId.Size = new System.Drawing.Size(55, 13);
            this.lblCambioUsuarioId.TabIndex = 27;
            this.lblCambioUsuarioId.Text = "Usuario Id";
            // 
            // txtCambioId
            // 
            this.txtCambioId.Location = new System.Drawing.Point(603, 30);
            this.txtCambioId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambioId.Name = "txtCambioId";
            this.txtCambioId.Size = new System.Drawing.Size(94, 20);
            this.txtCambioId.TabIndex = 25;
            // 
            // btnFiltrarCambios
            // 
            this.btnFiltrarCambios.Location = new System.Drawing.Point(606, 340);
            this.btnFiltrarCambios.Margin = new System.Windows.Forms.Padding(2);
            this.btnFiltrarCambios.Name = "btnFiltrarCambios";
            this.btnFiltrarCambios.Size = new System.Drawing.Size(56, 19);
            this.btnFiltrarCambios.TabIndex = 24;
            this.btnFiltrarCambios.Text = "Filtrar";
            this.btnFiltrarCambios.UseVisualStyleBackColor = true;
            this.btnFiltrarCambios.Click += new System.EventHandler(this.btnFiltrarCambios_Click);
            // 
            // dtpCambiosHasta
            // 
            this.dtpCambiosHasta.Location = new System.Drawing.Point(601, 304);
            this.dtpCambiosHasta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCambiosHasta.Name = "dtpCambiosHasta";
            this.dtpCambiosHasta.Size = new System.Drawing.Size(151, 20);
            this.dtpCambiosHasta.TabIndex = 23;
            // 
            // dtpCambiosDesde
            // 
            this.dtpCambiosDesde.Location = new System.Drawing.Point(601, 280);
            this.dtpCambiosDesde.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCambiosDesde.Name = "dtpCambiosDesde";
            this.dtpCambiosDesde.Size = new System.Drawing.Size(151, 20);
            this.dtpCambiosDesde.TabIndex = 22;
            // 
            // txtCambioEntidadId
            // 
            this.txtCambioEntidadId.Location = new System.Drawing.Point(603, 181);
            this.txtCambioEntidadId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambioEntidadId.Name = "txtCambioEntidadId";
            this.txtCambioEntidadId.Size = new System.Drawing.Size(92, 20);
            this.txtCambioEntidadId.TabIndex = 20;
            // 
            // txtCambioUsuarioId
            // 
            this.txtCambioUsuarioId.Location = new System.Drawing.Point(601, 80);
            this.txtCambioUsuarioId.Margin = new System.Windows.Forms.Padding(2);
            this.txtCambioUsuarioId.Name = "txtCambioUsuarioId";
            this.txtCambioUsuarioId.Size = new System.Drawing.Size(92, 20);
            this.txtCambioUsuarioId.TabIndex = 19;
            // 
            // dgvCambios
            // 
            this.dgvCambios.AllowUserToAddRows = false;
            this.dgvCambios.AllowUserToDeleteRows = false;
            this.dgvCambios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCambios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCambios.Location = new System.Drawing.Point(14, 13);
            this.dgvCambios.Name = "dgvCambios";
            this.dgvCambios.ReadOnly = true;
            this.dgvCambios.Size = new System.Drawing.Size(569, 356);
            this.dgvCambios.TabIndex = 1;
            // 
            // tabPermisos
            //
            this.tabPermisos.Controls.Add(this.grpPermisosAdmin);
            this.tabPermisos.Controls.Add(this.lblUsuarioSel);
            this.tabPermisos.Controls.Add(this.btnAsignar);
            this.tabPermisos.Controls.Add(this.btnQuitar);
            this.tabPermisos.Controls.Add(this.treeDisponibles);
            this.tabPermisos.Controls.Add(this.treeUsuarios);
            this.tabPermisos.Location = new System.Drawing.Point(4, 22);
            this.tabPermisos.Name = "tabPermisos";
            this.tabPermisos.Size = new System.Drawing.Size(760, 383);
            this.tabPermisos.TabIndex = 4;
            this.tabPermisos.Text = "Permisos";
            this.tabPermisos.UseVisualStyleBackColor = true;
            //
            // grpPermisosAdmin
            //
            this.grpPermisosAdmin.Controls.Add(this.btnQuitarRelacionPermiso);
            this.grpPermisosAdmin.Controls.Add(this.btnAgregarRelacionPermiso);
            this.grpPermisosAdmin.Controls.Add(this.btnCrearPermiso);
            this.grpPermisosAdmin.Controls.Add(this.btnActualizarPermiso);
            this.grpPermisosAdmin.Controls.Add(this.chkNuevoPermisoCompuesto);
            this.grpPermisosAdmin.Controls.Add(this.chkPermisoEsCompuesto);
            this.grpPermisosAdmin.Controls.Add(this.cmbPermisoHijo);
            this.grpPermisosAdmin.Controls.Add(this.cmbPermisoPadre);
            this.grpPermisosAdmin.Controls.Add(this.txtNuevoPermisoNombre);
            this.grpPermisosAdmin.Controls.Add(this.txtPermisoNombre);
            this.grpPermisosAdmin.Controls.Add(this.lblPermisoHijo);
            this.grpPermisosAdmin.Controls.Add(this.lblPermisoPadre);
            this.grpPermisosAdmin.Controls.Add(this.lblNuevoPermiso);
            this.grpPermisosAdmin.Controls.Add(this.lblPermisoNombre);
            this.grpPermisosAdmin.Controls.Add(this.lblPermisoExistente);
            this.grpPermisosAdmin.Controls.Add(this.cmbPermisosExistentes);
            this.grpPermisosAdmin.Location = new System.Drawing.Point(3, 210);
            this.grpPermisosAdmin.Name = "grpPermisosAdmin";
            this.grpPermisosAdmin.Size = new System.Drawing.Size(754, 170);
            this.grpPermisosAdmin.TabIndex = 5;
            this.grpPermisosAdmin.TabStop = false;
            this.grpPermisosAdmin.Text = "Gestión de permisos";
            //
            // btnQuitarRelacionPermiso
            //
            this.btnQuitarRelacionPermiso.Location = new System.Drawing.Point(528, 130);
            this.btnQuitarRelacionPermiso.Name = "btnQuitarRelacionPermiso";
            this.btnQuitarRelacionPermiso.Size = new System.Drawing.Size(160, 23);
            this.btnQuitarRelacionPermiso.TabIndex = 15;
            this.btnQuitarRelacionPermiso.Text = "Quitar relación";
            this.btnQuitarRelacionPermiso.UseVisualStyleBackColor = true;
            this.btnQuitarRelacionPermiso.Click += new System.EventHandler(this.btnQuitarRelacionPermiso_Click);
            //
            // btnAgregarRelacionPermiso
            //
            this.btnAgregarRelacionPermiso.Location = new System.Drawing.Point(528, 100);
            this.btnAgregarRelacionPermiso.Name = "btnAgregarRelacionPermiso";
            this.btnAgregarRelacionPermiso.Size = new System.Drawing.Size(160, 23);
            this.btnAgregarRelacionPermiso.TabIndex = 14;
            this.btnAgregarRelacionPermiso.Text = "Agregar relación";
            this.btnAgregarRelacionPermiso.UseVisualStyleBackColor = true;
            this.btnAgregarRelacionPermiso.Click += new System.EventHandler(this.btnAgregarRelacionPermiso_Click);
            //
            // btnCrearPermiso
            //
            this.btnCrearPermiso.Location = new System.Drawing.Point(528, 66);
            this.btnCrearPermiso.Name = "btnCrearPermiso";
            this.btnCrearPermiso.Size = new System.Drawing.Size(160, 23);
            this.btnCrearPermiso.TabIndex = 13;
            this.btnCrearPermiso.Text = "Crear permiso";
            this.btnCrearPermiso.UseVisualStyleBackColor = true;
            this.btnCrearPermiso.Click += new System.EventHandler(this.btnCrearPermiso_Click);
            //
            // btnActualizarPermiso
            //
            this.btnActualizarPermiso.Location = new System.Drawing.Point(528, 32);
            this.btnActualizarPermiso.Name = "btnActualizarPermiso";
            this.btnActualizarPermiso.Size = new System.Drawing.Size(160, 23);
            this.btnActualizarPermiso.TabIndex = 12;
            this.btnActualizarPermiso.Text = "Guardar cambios";
            this.btnActualizarPermiso.UseVisualStyleBackColor = true;
            this.btnActualizarPermiso.Click += new System.EventHandler(this.btnActualizarPermiso_Click);
            //
            // chkNuevoPermisoCompuesto
            //
            this.chkNuevoPermisoCompuesto.AutoSize = true;
            this.chkNuevoPermisoCompuesto.Location = new System.Drawing.Point(358, 70);
            this.chkNuevoPermisoCompuesto.Name = "chkNuevoPermisoCompuesto";
            this.chkNuevoPermisoCompuesto.Size = new System.Drawing.Size(141, 17);
            this.chkNuevoPermisoCompuesto.TabIndex = 11;
            this.chkNuevoPermisoCompuesto.Text = "Es permiso compuesto";
            this.chkNuevoPermisoCompuesto.UseVisualStyleBackColor = true;
            //
            // chkPermisoEsCompuesto
            //
            this.chkPermisoEsCompuesto.AutoSize = true;
            this.chkPermisoEsCompuesto.Location = new System.Drawing.Point(358, 36);
            this.chkPermisoEsCompuesto.Name = "chkPermisoEsCompuesto";
            this.chkPermisoEsCompuesto.Size = new System.Drawing.Size(141, 17);
            this.chkPermisoEsCompuesto.TabIndex = 10;
            this.chkPermisoEsCompuesto.Text = "Es permiso compuesto";
            this.chkPermisoEsCompuesto.UseVisualStyleBackColor = true;
            //
            // cmbPermisoHijo
            //
            this.cmbPermisoHijo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermisoHijo.FormattingEnabled = true;
            this.cmbPermisoHijo.Location = new System.Drawing.Point(150, 130);
            this.cmbPermisoHijo.Name = "cmbPermisoHijo";
            this.cmbPermisoHijo.Size = new System.Drawing.Size(188, 21);
            this.cmbPermisoHijo.TabIndex = 9;
            //
            // cmbPermisoPadre
            //
            this.cmbPermisoPadre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermisoPadre.FormattingEnabled = true;
            this.cmbPermisoPadre.Location = new System.Drawing.Point(150, 100);
            this.cmbPermisoPadre.Name = "cmbPermisoPadre";
            this.cmbPermisoPadre.Size = new System.Drawing.Size(188, 21);
            this.cmbPermisoPadre.TabIndex = 8;
            //
            // txtNuevoPermisoNombre
            //
            this.txtNuevoPermisoNombre.Location = new System.Drawing.Point(150, 68);
            this.txtNuevoPermisoNombre.Name = "txtNuevoPermisoNombre";
            this.txtNuevoPermisoNombre.Size = new System.Drawing.Size(188, 20);
            this.txtNuevoPermisoNombre.TabIndex = 7;
            //
            // txtPermisoNombre
            //
            this.txtPermisoNombre.Location = new System.Drawing.Point(150, 34);
            this.txtPermisoNombre.Name = "txtPermisoNombre";
            this.txtPermisoNombre.Size = new System.Drawing.Size(188, 20);
            this.txtPermisoNombre.TabIndex = 6;
            //
            // lblPermisoHijo
            //
            this.lblPermisoHijo.AutoSize = true;
            this.lblPermisoHijo.Location = new System.Drawing.Point(10, 133);
            this.lblPermisoHijo.Name = "lblPermisoHijo";
            this.lblPermisoHijo.Size = new System.Drawing.Size(69, 13);
            this.lblPermisoHijo.TabIndex = 5;
            this.lblPermisoHijo.Text = "Permiso hijo";
            //
            // lblPermisoPadre
            //
            this.lblPermisoPadre.AutoSize = true;
            this.lblPermisoPadre.Location = new System.Drawing.Point(10, 103);
            this.lblPermisoPadre.Name = "lblPermisoPadre";
            this.lblPermisoPadre.Size = new System.Drawing.Size(74, 13);
            this.lblPermisoPadre.TabIndex = 4;
            this.lblPermisoPadre.Text = "Permiso padre";
            //
            // lblNuevoPermiso
            //
            this.lblNuevoPermiso.AutoSize = true;
            this.lblNuevoPermiso.Location = new System.Drawing.Point(10, 71);
            this.lblNuevoPermiso.Name = "lblNuevoPermiso";
            this.lblNuevoPermiso.Size = new System.Drawing.Size(82, 13);
            this.lblNuevoPermiso.TabIndex = 3;
            this.lblNuevoPermiso.Text = "Nuevo permiso";
            //
            // lblPermisoNombre
            //
            this.lblPermisoNombre.AutoSize = true;
            this.lblPermisoNombre.Location = new System.Drawing.Point(10, 37);
            this.lblPermisoNombre.Name = "lblPermisoNombre";
            this.lblPermisoNombre.Size = new System.Drawing.Size(89, 13);
            this.lblPermisoNombre.TabIndex = 2;
            this.lblPermisoNombre.Text = "Nombre asignado";
            //
            // lblPermisoExistente
            //
            this.lblPermisoExistente.AutoSize = true;
            this.lblPermisoExistente.Location = new System.Drawing.Point(10, 17);
            this.lblPermisoExistente.Name = "lblPermisoExistente";
            this.lblPermisoExistente.Size = new System.Drawing.Size(90, 13);
            this.lblPermisoExistente.TabIndex = 1;
            this.lblPermisoExistente.Text = "Permiso existente";
            //
            // cmbPermisosExistentes
            //
            this.cmbPermisosExistentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermisosExistentes.FormattingEnabled = true;
            this.cmbPermisosExistentes.Location = new System.Drawing.Point(150, 14);
            this.cmbPermisosExistentes.Name = "cmbPermisosExistentes";
            this.cmbPermisosExistentes.Size = new System.Drawing.Size(188, 21);
            this.cmbPermisosExistentes.TabIndex = 0;
            this.cmbPermisosExistentes.SelectedIndexChanged += new System.EventHandler(this.cmbPermisosExistentes_SelectedIndexChanged);
            //
            // lblUsuarioSel
            //
            this.lblUsuarioSel.AutoSize = true;
            this.lblUsuarioSel.Location = new System.Drawing.Point(222, 22);
            this.lblUsuarioSel.Name = "lblUsuarioSel";
            this.lblUsuarioSel.Size = new System.Drawing.Size(35, 13);
            this.lblUsuarioSel.TabIndex = 4;
            this.lblUsuarioSel.Text = "label1";
            //
            // btnAsignar
            //
            this.btnAsignar.Location = new System.Drawing.Point(328, 80);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(120, 23);
            this.btnAsignar.TabIndex = 3;
            this.btnAsignar.Text = "Asignar Permiso";
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            //
            // btnQuitar
            //
            this.btnQuitar.Location = new System.Drawing.Point(328, 120);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(120, 23);
            this.btnQuitar.TabIndex = 2;
            this.btnQuitar.Text = "Quitar Permiso";
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            //
            // treeDisponibles
            //
            this.treeDisponibles.Location = new System.Drawing.Point(534, 3);
            this.treeDisponibles.Name = "treeDisponibles";
            this.treeDisponibles.Size = new System.Drawing.Size(223, 200);
            this.treeDisponibles.TabIndex = 1;
            this.treeDisponibles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDisponibles_AfterSelect);
            //
            // treeUsuarios
            //
            this.treeUsuarios.Location = new System.Drawing.Point(3, 3);
            this.treeUsuarios.Name = "treeUsuarios";
            this.treeUsuarios.Size = new System.Drawing.Size(223, 200);
            this.treeUsuarios.TabIndex = 0;
            this.treeUsuarios.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeUsuarios_AfterSelect_1);
            //
            // tabIdiomas
            //
            this.tabIdiomas.Controls.Add(this.btnNuevaLeyenda);
            this.tabIdiomas.Controls.Add(this.btnGuardarLeyenda);
            this.tabIdiomas.Controls.Add(this.txtLeyendaTexto);
            this.tabIdiomas.Controls.Add(this.lblLeyendaTexto);
            this.tabIdiomas.Controls.Add(this.txtLeyendaDescripcion);
            this.tabIdiomas.Controls.Add(this.lblLeyendaDescripcion);
            this.tabIdiomas.Controls.Add(this.txtLeyendaClave);
            this.tabIdiomas.Controls.Add(this.lblLeyendaClave);
            this.tabIdiomas.Controls.Add(this.dgvLeyendas);
            this.tabIdiomas.Controls.Add(this.btnNuevoIdioma);
            this.tabIdiomas.Controls.Add(this.btnActualizarIdioma);
            this.tabIdiomas.Controls.Add(this.btnCrearIdioma);
            this.tabIdiomas.Controls.Add(this.chkIdiomaPorDefecto);
            this.tabIdiomas.Controls.Add(this.lblIdiomaPorDefecto);
            this.tabIdiomas.Controls.Add(this.txtIdiomaNombre);
            this.tabIdiomas.Controls.Add(this.lblIdiomaNombre);
            this.tabIdiomas.Controls.Add(this.txtIdiomaCodigo);
            this.tabIdiomas.Controls.Add(this.lblIdiomaCodigo);
            this.tabIdiomas.Controls.Add(this.dgvIdiomasAdmin);
            this.tabIdiomas.Controls.Add(this.lblIdiomaSeleccionadoAdmin);
            this.tabIdiomas.Location = new System.Drawing.Point(4, 22);
            this.tabIdiomas.Name = "tabIdiomas";
            this.tabIdiomas.Padding = new System.Windows.Forms.Padding(3);
            this.tabIdiomas.Size = new System.Drawing.Size(760, 383);
            this.tabIdiomas.TabIndex = 5;
            this.tabIdiomas.Text = "Idiomas";
            this.tabIdiomas.UseVisualStyleBackColor = true;
            //
            // btnNuevaLeyenda
            //
            this.btnNuevaLeyenda.Location = new System.Drawing.Point(635, 347);
            this.btnNuevaLeyenda.Name = "btnNuevaLeyenda";
            this.btnNuevaLeyenda.Size = new System.Drawing.Size(115, 23);
            this.btnNuevaLeyenda.TabIndex = 19;
            this.btnNuevaLeyenda.Text = "Nueva leyenda";
            this.btnNuevaLeyenda.UseVisualStyleBackColor = true;
            this.btnNuevaLeyenda.Click += new System.EventHandler(this.btnNuevaLeyenda_Click);
            //
            // btnGuardarLeyenda
            //
            this.btnGuardarLeyenda.Location = new System.Drawing.Point(635, 318);
            this.btnGuardarLeyenda.Name = "btnGuardarLeyenda";
            this.btnGuardarLeyenda.Size = new System.Drawing.Size(115, 23);
            this.btnGuardarLeyenda.TabIndex = 18;
            this.btnGuardarLeyenda.Text = "Guardar leyenda";
            this.btnGuardarLeyenda.UseVisualStyleBackColor = true;
            this.btnGuardarLeyenda.Click += new System.EventHandler(this.btnGuardarLeyenda_Click);
            //
            // txtLeyendaTexto
            //
            this.txtLeyendaTexto.Location = new System.Drawing.Point(315, 349);
            this.txtLeyendaTexto.Name = "txtLeyendaTexto";
            this.txtLeyendaTexto.Size = new System.Drawing.Size(314, 20);
            this.txtLeyendaTexto.TabIndex = 17;
            //
            // lblLeyendaTexto
            //
            this.lblLeyendaTexto.AutoSize = true;
            this.lblLeyendaTexto.Location = new System.Drawing.Point(251, 352);
            this.lblLeyendaTexto.Name = "lblLeyendaTexto";
            this.lblLeyendaTexto.Size = new System.Drawing.Size(34, 13);
            this.lblLeyendaTexto.TabIndex = 16;
            this.lblLeyendaTexto.Text = "Texto";
            //
            // txtLeyendaDescripcion
            //
            this.txtLeyendaDescripcion.Location = new System.Drawing.Point(510, 319);
            this.txtLeyendaDescripcion.Name = "txtLeyendaDescripcion";
            this.txtLeyendaDescripcion.Size = new System.Drawing.Size(119, 20);
            this.txtLeyendaDescripcion.TabIndex = 15;
            //
            // lblLeyendaDescripcion
            //
            this.lblLeyendaDescripcion.AutoSize = true;
            this.lblLeyendaDescripcion.Location = new System.Drawing.Point(407, 322);
            this.lblLeyendaDescripcion.Name = "lblLeyendaDescripcion";
            this.lblLeyendaDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblLeyendaDescripcion.TabIndex = 14;
            this.lblLeyendaDescripcion.Text = "Descripción";
            //
            // txtLeyendaClave
            //
            this.txtLeyendaClave.Location = new System.Drawing.Point(315, 319);
            this.txtLeyendaClave.Name = "txtLeyendaClave";
            this.txtLeyendaClave.Size = new System.Drawing.Size(86, 20);
            this.txtLeyendaClave.TabIndex = 13;
            //
            // lblLeyendaClave
            //
            this.lblLeyendaClave.AutoSize = true;
            this.lblLeyendaClave.Location = new System.Drawing.Point(251, 322);
            this.lblLeyendaClave.Name = "lblLeyendaClave";
            this.lblLeyendaClave.Size = new System.Drawing.Size(34, 13);
            this.lblLeyendaClave.TabIndex = 12;
            this.lblLeyendaClave.Text = "Clave";
            //
            // dgvLeyendas
            //
            this.dgvLeyendas.AllowUserToAddRows = false;
            this.dgvLeyendas.AllowUserToDeleteRows = false;
            this.dgvLeyendas.Location = new System.Drawing.Point(254, 170);
            this.dgvLeyendas.Name = "dgvLeyendas";
            this.dgvLeyendas.ReadOnly = true;
            this.dgvLeyendas.Size = new System.Drawing.Size(496, 140);
            this.dgvLeyendas.TabIndex = 11;
            this.dgvLeyendas.SelectionChanged += new System.EventHandler(this.dgvLeyendas_SelectionChanged);
            //
            // btnNuevoIdioma
            //
            this.btnNuevoIdioma.Location = new System.Drawing.Point(566, 130);
            this.btnNuevoIdioma.Name = "btnNuevoIdioma";
            this.btnNuevoIdioma.Size = new System.Drawing.Size(120, 23);
            this.btnNuevoIdioma.TabIndex = 10;
            this.btnNuevoIdioma.Text = "Nuevo idioma";
            this.btnNuevoIdioma.UseVisualStyleBackColor = true;
            this.btnNuevoIdioma.Click += new System.EventHandler(this.btnNuevoIdioma_Click);
            //
            // btnActualizarIdioma
            //
            this.btnActualizarIdioma.Location = new System.Drawing.Point(440, 130);
            this.btnActualizarIdioma.Name = "btnActualizarIdioma";
            this.btnActualizarIdioma.Size = new System.Drawing.Size(120, 23);
            this.btnActualizarIdioma.TabIndex = 9;
            this.btnActualizarIdioma.Text = "Actualizar idioma";
            this.btnActualizarIdioma.UseVisualStyleBackColor = true;
            this.btnActualizarIdioma.Click += new System.EventHandler(this.btnActualizarIdioma_Click);
            //
            // btnCrearIdioma
            //
            this.btnCrearIdioma.Location = new System.Drawing.Point(314, 130);
            this.btnCrearIdioma.Name = "btnCrearIdioma";
            this.btnCrearIdioma.Size = new System.Drawing.Size(120, 23);
            this.btnCrearIdioma.TabIndex = 8;
            this.btnCrearIdioma.Text = "Crear idioma";
            this.btnCrearIdioma.UseVisualStyleBackColor = true;
            this.btnCrearIdioma.Click += new System.EventHandler(this.btnCrearIdioma_Click);
            //
            // chkIdiomaPorDefecto
            //
            this.chkIdiomaPorDefecto.AutoSize = true;
            this.chkIdiomaPorDefecto.Location = new System.Drawing.Point(440, 104);
            this.chkIdiomaPorDefecto.Name = "chkIdiomaPorDefecto";
            this.chkIdiomaPorDefecto.Size = new System.Drawing.Size(15, 14);
            this.chkIdiomaPorDefecto.TabIndex = 7;
            this.chkIdiomaPorDefecto.UseVisualStyleBackColor = true;
            //
            // lblIdiomaPorDefecto
            //
            this.lblIdiomaPorDefecto.AutoSize = true;
            this.lblIdiomaPorDefecto.Location = new System.Drawing.Point(251, 104);
            this.lblIdiomaPorDefecto.Name = "lblIdiomaPorDefecto";
            this.lblIdiomaPorDefecto.Size = new System.Drawing.Size(103, 13);
            this.lblIdiomaPorDefecto.TabIndex = 6;
            this.lblIdiomaPorDefecto.Text = "Idioma por defecto";
            //
            // txtIdiomaNombre
            //
            this.txtIdiomaNombre.Location = new System.Drawing.Point(315, 72);
            this.txtIdiomaNombre.Name = "txtIdiomaNombre";
            this.txtIdiomaNombre.Size = new System.Drawing.Size(247, 20);
            this.txtIdiomaNombre.TabIndex = 5;
            //
            // lblIdiomaNombre
            //
            this.lblIdiomaNombre.AutoSize = true;
            this.lblIdiomaNombre.Location = new System.Drawing.Point(251, 75);
            this.lblIdiomaNombre.Name = "lblIdiomaNombre";
            this.lblIdiomaNombre.Size = new System.Drawing.Size(44, 13);
            this.lblIdiomaNombre.TabIndex = 4;
            this.lblIdiomaNombre.Text = "Nombre";
            //
            // txtIdiomaCodigo
            //
            this.txtIdiomaCodigo.Location = new System.Drawing.Point(315, 44);
            this.txtIdiomaCodigo.Name = "txtIdiomaCodigo";
            this.txtIdiomaCodigo.Size = new System.Drawing.Size(120, 20);
            this.txtIdiomaCodigo.TabIndex = 3;
            //
            // lblIdiomaCodigo
            //
            this.lblIdiomaCodigo.AutoSize = true;
            this.lblIdiomaCodigo.Location = new System.Drawing.Point(251, 47);
            this.lblIdiomaCodigo.Name = "lblIdiomaCodigo";
            this.lblIdiomaCodigo.Size = new System.Drawing.Size(40, 13);
            this.lblIdiomaCodigo.TabIndex = 2;
            this.lblIdiomaCodigo.Text = "Código";
            //
            // dgvIdiomasAdmin
            //
            this.dgvIdiomasAdmin.AllowUserToAddRows = false;
            this.dgvIdiomasAdmin.AllowUserToDeleteRows = false;
            this.dgvIdiomasAdmin.Location = new System.Drawing.Point(6, 28);
            this.dgvIdiomasAdmin.Name = "dgvIdiomasAdmin";
            this.dgvIdiomasAdmin.ReadOnly = true;
            this.dgvIdiomasAdmin.Size = new System.Drawing.Size(230, 335);
            this.dgvIdiomasAdmin.TabIndex = 1;
            this.dgvIdiomasAdmin.SelectionChanged += new System.EventHandler(this.dgvIdiomasAdmin_SelectionChanged);
            //
            // lblIdiomaSeleccionadoAdmin
            //
            this.lblIdiomaSeleccionadoAdmin.AutoSize = true;
            this.lblIdiomaSeleccionadoAdmin.Location = new System.Drawing.Point(3, 10);
            this.lblIdiomaSeleccionadoAdmin.Name = "lblIdiomaSeleccionadoAdmin";
            this.lblIdiomaSeleccionadoAdmin.Size = new System.Drawing.Size(108, 13);
            this.lblIdiomaSeleccionadoAdmin.TabIndex = 0;
            this.lblIdiomaSeleccionadoAdmin.Text = "Idioma seleccionado";
            // 
            // 
            // FormPrueba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabGeneral);
            this.Controls.Add(this.btnCerrarSesion);
            this.Name = "FormPrueba";
            this.Text = "FormPrueba";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormPrueba_FormClosed);
            this.Load += new System.EventHandler(this.FormPrueba_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tabGeneral.ResumeLayout(false);
            this.tabMenuPrincipal.ResumeLayout(false);
            this.tabMenuPrincipal.PerformLayout();
            this.tabUsuarios.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGestionUsuario)).EndInit();
            this.tabBitacora.ResumeLayout(false);
            this.tabBitacora.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBitacora)).EndInit();
            this.tabControlCambios.ResumeLayout(false);
            this.tabControlCambios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCambios)).EndInit();
            this.tabPermisos.ResumeLayout(false);
            this.grpPermisosAdmin.ResumeLayout(false);
            this.grpPermisosAdmin.PerformLayout();
            this.tabIdiomas.ResumeLayout(false);
            this.tabIdiomas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeyendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIdiomasAdmin)).EndInit();
            this.ResumeLayout(false);

        }

        private void treeUsuarios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion

        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabControl tabGeneral;
        private System.Windows.Forms.TabPage tabUsuarios;
        private System.Windows.Forms.TabPage tabBitacora;
        private System.Windows.Forms.TabPage tabMenuPrincipal;
        private System.Windows.Forms.DataGridView dgvBitacora;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.ComboBox cmbEvento;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnFiltrarBitacora;
        private System.Windows.Forms.TextBox txtAuditoriaId;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblUsuarioId;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblAuditoriaId;
        private System.Windows.Forms.Button btnNuevoRegistro;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.DataGridView dgvGestionUsuario;
        private System.Windows.Forms.TabPage tabControlCambios;
        private System.Windows.Forms.DataGridView dgvCambios;
        private System.Windows.Forms.Label lblCambioFecha;
        private System.Windows.Forms.Label lblCambioEntidadId;
        private System.Windows.Forms.Label lblCambioEntidad;
        private System.Windows.Forms.Label lblCambioId;
        private System.Windows.Forms.Label lblCambioUsuarioId;
        private System.Windows.Forms.TextBox txtCambioId;
        private System.Windows.Forms.Button btnFiltrarCambios;
        private System.Windows.Forms.DateTimePicker dtpCambiosHasta;
        private System.Windows.Forms.DateTimePicker dtpCambiosDesde;
        private System.Windows.Forms.TextBox txtCambioEntidadId;
        private System.Windows.Forms.TextBox txtCambioUsuarioId;
        private System.Windows.Forms.Button btnLimpiarCambios;
        private System.Windows.Forms.TextBox txtCambioEntidad;
        private System.Windows.Forms.Label lblCambioCampo;
        private System.Windows.Forms.TextBox txtCambioCampo;
        private System.Windows.Forms.ComboBox cmbIdiomas;
        private System.Windows.Forms.TabPage tabPermisos;
        private System.Windows.Forms.TreeView treeUsuarios;
        private System.Windows.Forms.Label lblUsuarioSel;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.TreeView treeDisponibles;
        private System.Windows.Forms.GroupBox grpPermisosAdmin;
        private System.Windows.Forms.Button btnQuitarRelacionPermiso;
        private System.Windows.Forms.Button btnAgregarRelacionPermiso;
        private System.Windows.Forms.Button btnCrearPermiso;
        private System.Windows.Forms.Button btnActualizarPermiso;
        private System.Windows.Forms.CheckBox chkNuevoPermisoCompuesto;
        private System.Windows.Forms.CheckBox chkPermisoEsCompuesto;
        private System.Windows.Forms.ComboBox cmbPermisoHijo;
        private System.Windows.Forms.ComboBox cmbPermisoPadre;
        private System.Windows.Forms.TextBox txtNuevoPermisoNombre;
        private System.Windows.Forms.TextBox txtPermisoNombre;
        private System.Windows.Forms.Label lblPermisoHijo;
        private System.Windows.Forms.Label lblPermisoPadre;
        private System.Windows.Forms.Label lblNuevoPermiso;
        private System.Windows.Forms.Label lblPermisoNombre;
        private System.Windows.Forms.Label lblPermisoExistente;
        private System.Windows.Forms.ComboBox cmbPermisosExistentes;
        private System.Windows.Forms.TabPage tabIdiomas;
        private System.Windows.Forms.Button btnNuevaLeyenda;
        private System.Windows.Forms.Button btnGuardarLeyenda;
        private System.Windows.Forms.TextBox txtLeyendaTexto;
        private System.Windows.Forms.Label lblLeyendaTexto;
        private System.Windows.Forms.TextBox txtLeyendaDescripcion;
        private System.Windows.Forms.Label lblLeyendaDescripcion;
        private System.Windows.Forms.TextBox txtLeyendaClave;
        private System.Windows.Forms.Label lblLeyendaClave;
        private System.Windows.Forms.DataGridView dgvLeyendas;
        private System.Windows.Forms.Button btnNuevoIdioma;
        private System.Windows.Forms.Button btnActualizarIdioma;
        private System.Windows.Forms.Button btnCrearIdioma;
        private System.Windows.Forms.CheckBox chkIdiomaPorDefecto;
        private System.Windows.Forms.Label lblIdiomaPorDefecto;
        private System.Windows.Forms.TextBox txtIdiomaNombre;
        private System.Windows.Forms.Label lblIdiomaNombre;
        private System.Windows.Forms.TextBox txtIdiomaCodigo;
        private System.Windows.Forms.Label lblIdiomaCodigo;
        private System.Windows.Forms.DataGridView dgvIdiomasAdmin;
        private System.Windows.Forms.Label lblIdiomaSeleccionadoAdmin;
        private Button btnEliminar;
        private System.Windows.Forms.Label lblEstadoIntegridad;
        private System.Windows.Forms.Button btnVerificarIntegridad;
        private System.Windows.Forms.Button btnRecalcularIntegridad;
    }
}