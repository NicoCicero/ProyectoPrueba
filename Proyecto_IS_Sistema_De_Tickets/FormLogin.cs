using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BL;

namespace Proyecto_IS_Sistema_De_Tickets
{
    public partial class FormLogin : Form, IIdiomaObserver
    {
        private readonly IdiomaService _idiomaSrv = new IdiomaService();
        private string _placeholderUsuarioActual;
        private string _placeholderContraseñaActual;
        private bool _usuarioPlaceholderActivo = true;
        private bool _contraseñaPlaceholderActiva = true;
        public FormLogin()
        {
            InitializeComponent();

            _placeholderUsuarioActual = txt_Usuario.Text;
            _placeholderContraseñaActual = txt_Contraseña.Text;
            ActivarPlaceholderUsuario();
            ActivarPlaceholderContraseña();

            // me suscribo al observer
            IdiomaManager.Instancia.Suscribir(this);
        }
        #region Diseño
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string sql = AdminSeedHelper.BuildUpdateAdminSql("admin@sistema.com", "Admin123!");
            Console.WriteLine(sql);

            var idiomas = _idiomaSrv.ListarIdiomas();
            cmbIdiomas.DataSource = idiomas;
            cmbIdiomas.DisplayMember = "Nombre";
            cmbIdiomas.ValueMember = "Codigo";

            // ⬇️ Respetar el idioma actual si ya hay uno elegido
            var codActual = IdiomaManager.Instancia.CodigoActual;

            if (!string.IsNullOrWhiteSpace(codActual) && idiomas.Any(i => i.Codigo == codActual))
                cmbIdiomas.SelectedValue = codActual;
            else
                cmbIdiomas.SelectedValue = idiomas.FirstOrDefault(i => i.EsPorDefecto)?.Codigo ?? idiomas.First().Codigo;

            try
            {
                var (ok, detalle) = BL.VerificadorIntegridadService.Instancia.ValidarTodo();
                if (!ok)
                {
                    try
                    {
                        new DAO.AuditoriaRepository().Registrar("INTEGRIDAD_FALLA", null, detalle);
                    }
                    catch
                    {
                        // si no se puede registrar, no detiene el flujo: el admin seguirá viendo el mensaje
                    }

                    var r = MessageBox.Show(
                        "Se detectó una inconsistencia de integridad (DVH/DVV).\n\n" +
                        detalle + "\n\n" +
                        "¿Deseás continuar de todas formas?",
                        "Integridad de datos",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (r == DialogResult.No)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar integridad: " + ex.Message);
            }


        }

        // este lo llama el IdiomaManager
        public void ActualizarIdioma(Dictionary<string, string> t)
        {
            // título arriba
            lblTituloLogin.Text = t["LOGIN_TITULO"];

            // labels
            var placeholderUsuarioPrevio = _placeholderUsuarioActual;
            var placeholderContraseñaPrevio = _placeholderContraseñaActual;

            bool usuarioTeniaPlaceholder = _usuarioPlaceholderActivo
                || string.IsNullOrWhiteSpace(txt_Usuario.Text)
                || string.Equals(txt_Usuario.Text, placeholderUsuarioPrevio, StringComparison.Ordinal);

            bool contraseñaTeniaPlaceholder = _contraseñaPlaceholderActiva
                || string.IsNullOrEmpty(txt_Contraseña.Text)
                || string.Equals(txt_Contraseña.Text, placeholderContraseñaPrevio, StringComparison.Ordinal);

            _placeholderUsuarioActual = t["LOGIN_USUARIO"];
            _placeholderContraseñaActual = t["LOGIN_CONTRASENA"];

            if (usuarioTeniaPlaceholder)
                ActivarPlaceholderUsuario();
            else
                _usuarioPlaceholderActivo = false;

            if (contraseñaTeniaPlaceholder)
                ActivarPlaceholderContraseña();
            else
            {
                _contraseñaPlaceholderActiva = false;
                txt_Contraseña.UseSystemPasswordChar = true;
            }

            // botón
            btnAcceder.Text = t["LOGIN_BTN_ACCEDER"];

            //// si tenés link
            //if (lnkOlvido != null)
            //    lnkOlvido.Text = "[ " + t["LOGIN_CONTRASENA"] + " ? ]"; // o una etiqueta propia
        }

        private void txt_Usuario_Enter(object sender, EventArgs e)
        {
            if (_usuarioPlaceholderActivo)
            {
                _usuarioPlaceholderActivo = false;
                txt_Usuario.Clear();
            }
        }

        private void txt_Usuario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Usuario.Text))
            {
                ActivarPlaceholderUsuario();
            }
            else { _usuarioPlaceholderActivo = false; }
        }

        private void txt_Contraseña_Enter(object sender, EventArgs e)
        {
            if (_contraseñaPlaceholderActiva)
            {
                _contraseñaPlaceholderActiva = false;
                txt_Contraseña.Clear();
                txt_Contraseña.UseSystemPasswordChar = true;
            }
        }

        private void txt_Contraseña_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Contraseña.Text))
            {
                ActivarPlaceholderContraseña();
            }
            else
            {
                _contraseñaPlaceholderActiva = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        private void btn_Acceder_Click(object sender, EventArgs e)
        {
            var ok = AuthService.Instancia.Login(ObtenerUsuarioIngresado(),
            ObtenerPasswordIngresado(), ip: null,userAgent : Application.ProductName);

            if (ok)
            {
                this.Hide();
                var main = new FormPrueba();
                main.FormClosed += (_, __) => Application.Exit();  // cierra todo al cerrar el main
                main.Show();
            }
            else
            {
                MsgError("Usuario o contraseña incorrecta o cuenta bloqueada");
                ActivarPlaceholderContraseña();
                txt_Usuario.Focus();
            }
        }
        private void MsgError(string msg)
        {
            lblError.Text = "     " + msg;
            lblError.Visible = true;
        }

        private void ActivarPlaceholderUsuario()
        {
            _usuarioPlaceholderActivo = true;
            txt_Usuario.Text = _placeholderUsuarioActual ?? string.Empty;
        }

        private void ActivarPlaceholderContraseña()
        {
            _contraseñaPlaceholderActiva = true;
            txt_Contraseña.UseSystemPasswordChar = false;
            txt_Contraseña.Text = _placeholderContraseñaActual ?? string.Empty;
        }

        private string ObtenerUsuarioIngresado()
        {
            return _usuarioPlaceholderActivo ? string.Empty : txt_Usuario.Text.Trim();
        }

        private string ObtenerPasswordIngresado()
        {
            return _contraseñaPlaceholderActiva ? string.Empty : txt_Contraseña.Text;
        }



        private void txt_Usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIdiomas.SelectedValue is string cod && !string.IsNullOrWhiteSpace(cod))
                _idiomaSrv.SeleccionarIdioma(cod); // esto dispara Notificar y setea el CodigoActual
        }


        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            IdiomaManager.Instancia.Desuscribir(this);
            base.OnFormClosed(e);
        }


    }
}
