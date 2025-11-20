using Proyecto_de_Herramientas.Proyecto_de_Herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto_de_Herramientas
{

    public partial class UC_EditarUsuario : UserControl
    {
        private Usuario usuarioActual;
        public UC_EditarUsuario(Usuario usuario)
        {
            InitializeComponent();
            usuarioActual = usuario;
            this.Load += UC_EditarUsuario_Load;
            btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
            btnGuardarCambios.Click += btnGuardarCambios_Click;
        }

        private void UC_EditarUsuario_Load(object sender, EventArgs e)
        {
            // TextBox: siempre muestran lo que trae el usuarioActual
            txtNombre.Text = usuarioActual.Nombre;
            txtDireccion.Text = usuarioActual.Direccion;
            txtCorreo.Text = usuarioActual.Email;
            txtTelefono.Text = usuarioActual.Telefono;

            // ✅ Lo que pediste: usuario y contraseña
            txtUsuarioLogin.Text = usuarioActual.UsuarioLogin;
            txtContraseña.Text = usuarioActual.Contraseña;

            CargarRoles(usuarioActual.Rol);

            // Imagen (igual que producto)
            if (!string.IsNullOrEmpty(usuarioActual.ImagenPath))
            {
                string rutaCompleta = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), usuarioActual.ImagenPath);
                if (File.Exists(rutaCompleta))
                {
                    ImagenHelper.LiberarImagen(pbImagen);
                    ImagenHelper.CargarImagenSinBloqueo(pbImagen, rutaCompleta);
                }
            }
        }

        private void CargarRoles(string rolActual)
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Auxiliar de Ventas");
            cmbRol.Items.Add("Auxiliar de Bodega");
            cmbRol.SelectedItem = rolActual;
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {

            // Si quieres conservar valores cuando están vacíos, comenta estas dos líneas
            usuarioActual.UsuarioLogin = txtUsuarioLogin.Text;
            usuarioActual.Contraseña = txtContraseña.Text;

            usuarioActual.Nombre = txtNombre.Text.Trim();
            usuarioActual.Direccion = txtDireccion.Text.Trim();
            usuarioActual.Rol = cmbRol.SelectedItem?.ToString() ?? usuarioActual.Rol;
            usuarioActual.Telefono = txtTelefono.Text.Trim();
            usuarioActual.Email = txtCorreo.Text.Trim();
            usuarioActual.Estado = true;

            UsuarioDAO.ActualizarUsuario(usuarioActual);
            MessageBox.Show("Usuario actualizado correctamente.");

        }

        private void btnSeleccionarImagen_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Imágenes|*.jpg;*.png;*.jpeg" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivo = Path.GetFileName(ofd.FileName);
                string rutaDestino = ImagenHelper.ObtenerRutaImagenUsuario(nombreArchivo);

                try
                {
                    ImagenHelper.LiberarImagen(pbImagen);
                    File.Copy(ofd.FileName, rutaDestino, true);
                    ImagenHelper.CargarImagenSinBloqueo(pbImagen, rutaDestino);
                    usuarioActual.ImagenPath = ImagenHelper.ObtenerRutaRelativa(nombreArchivo);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo copiar la imagen.\n\n" + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var confirmacion = MessageBox.Show($"¿Estás seguro de que deseas eliminar a {usuarioActual.Nombre}?",
                                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                using (SqlConnection conn = BDJeanStore.Conectar())
                {
                    string query = "DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdUsuario", usuarioActual.ID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                if (!string.IsNullOrEmpty(usuarioActual.ImagenPath))
                {
                    string rutaCompleta = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), usuarioActual.ImagenPath);
                    ImagenHelper.EliminarImagen(rutaCompleta);
                }

                MessageBox.Show("Usuario eliminado correctamente.");
            }
        }
    }
}
