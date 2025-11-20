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
    public partial class UC_AgregarUsuario : UserControl
    {
        private string rutaImagenRelativa = "";
        public UC_AgregarUsuario()
        {
            InitializeComponent();
            btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
            btnGuardarUsuario.Click += btnGuardarUsuario_Click;
            CargarRoles();
        }
        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Auxiliar de Ventas");
            cmbRol.Items.Add("Bodega");
            cmbRol.SelectedIndex = 0;
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Imágenes|*.jpg;*.png;*.jpeg";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                // Generar nombre único para evitar conflicto
                string extension = Path.GetExtension(dialogo.FileName);
                string nombreUnico = Guid.NewGuid().ToString() + extension;
                string rutaDestino = ImagenHelper.ObtenerRutaImagenUsuario(nombreUnico);

                try
                {
                    ImagenHelper.LiberarImagen(pbImagen); // liberar antes de copiar

                    File.Copy(dialogo.FileName, rutaDestino, true); // copiar sin conflicto

                    rutaImagenRelativa = ImagenHelper.ObtenerRutaRelativa(nombreUnico);

                    ImagenHelper.CargarImagenSinBloqueo(pbImagen, rutaDestino);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo copiar la imagen.\n\n" + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtUsuarioLogin.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text))
            {
                MessageBox.Show("Nombre, Usuario y Contraseña son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"INSERT INTO Usuarios (Nombre, Direccion, UsuarioLogin, Contraseña, IdRol, ImagenPath, Telefono, Email, Estado)
                                 VALUES (@Nombre, @Direccion, @UsuarioLogin, @Contraseña,
                                         (SELECT IdRol FROM Roles WHERE NombreRol = @Rol),
                                         @ImagenPath, @Telefono, @Correo, 1)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                cmd.Parameters.AddWithValue("@UsuarioLogin", txtUsuarioLogin.Text.Trim());
                cmd.Parameters.AddWithValue("@Contraseña", txtContraseña.Text.Trim());
                cmd.Parameters.AddWithValue("@Rol", cmbRol.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ImagenPath", rutaImagenRelativa);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());

              
                cmd.ExecuteNonQuery();
              
            }

            MessageBox.Show("Usuario agregado correctamente.");
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtUsuarioLogin.Clear();
            txtContraseña.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            cmbRol.SelectedIndex = 0;
            pbImagen.Image = null;
            rutaImagenRelativa = "";
        }
    }
}
