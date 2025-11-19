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
    public partial class FormAgregarUsuario : Form
    {
        private string usuarioActual;
        private string imagenRelativa = null;
        public FormAgregarUsuario(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtUsuarioLogin.Clear();
            txtContraseña.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cbRol.SelectedIndex = -1;
            pictureBox1.Image = null;
            imagenRelativa = null;
        }





        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Oculta el formulario actual
            new FormAdministrador(usuarioActual).Show(); // Abre el formulario de destino
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtUsuarioLogin.Text) || string.IsNullOrEmpty(txtContraseña.Text))
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar si el usuario ya existe
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string validarSql = "SELECT COUNT(*) FROM Usuarios WHERE UsuarioLogin = @UsuarioLogin";
                using (SqlCommand validarCmd = new SqlCommand(validarSql, conn))
                {
                    validarCmd.Parameters.AddWithValue("@UsuarioLogin", txtUsuarioLogin.Text.Trim());
                    int existe = (int)validarCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("El nombre de usuario ya existe. Elija otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insertar nuevo usuario
                string sql = @"INSERT INTO Usuarios (Nombre, UsuarioLogin, Contraseña, IdRol, ImagenPath, Direccion, Telefono, Email, Estado)
                               VALUES (@Nombre, @UsuarioLogin, @Contraseña, @IdRol, @ImagenPath, @Direccion, @Telefono, @Email, 1)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                    cmd.Parameters.AddWithValue("@UsuarioLogin", txtUsuarioLogin.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contraseña", txtContraseña.Text.Trim());
                    cmd.Parameters.AddWithValue("@IdRol", ((Rol)cbRol.SelectedItem).IdRol);
                    cmd.Parameters.AddWithValue("@ImagenPath", imagenRelativa ?? "");
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text.Trim());
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Usuario agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
        }

      

        private void FormAgregarUsuario_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = "SELECT IdRol, NombreRol FROM Roles";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Rol> roles = new List<Rol>();

                    while (reader.Read())
                    {
                        roles.Add(new Rol
                        {
                            IdRol = reader.GetInt32(0),
                            NombreRol = reader.GetString(1)
                        });
                    }

                    cbRol.DataSource = roles;
                    cbRol.DisplayMember = "NombreRol";
                    cbRol.ValueMember = "IdRol";
                }
            }
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            string carpetaImagenes = Path.Combine(
      Directory.GetParent(Application.StartupPath).Parent.Parent.FullName,
      "Images"
  );

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Selecciona una imagen de usuario",
                InitialDirectory = carpetaImagenes
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string rutaOrigen = ofd.FileName;
                string nombreUsuario = txtUsuarioLogin.Text.Trim();

                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    MessageBox.Show("⚠️ Debes ingresar un nombre de usuario antes de seleccionar la imagen.",
                                    "Nombre requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string extension = Path.GetExtension(rutaOrigen);
                string nombreArchivo = $"{nombreUsuario}{extension}";

                if (string.IsNullOrEmpty(nombreArchivo) || string.IsNullOrEmpty(extension))
                {
                    MessageBox.Show("⚠️ El nombre del archivo no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string carpetaProyecto = RutaProyectoHelper.ObtenerRaizProyecto();
                string carpetaDestino = Path.Combine(carpetaProyecto, "Images", "Usuarios");
                string rutaDestino = Path.Combine(carpetaDestino, nombreArchivo);

                // Liberar imagen actual del PictureBox
                ImagenHelper.LiberarImagen(pictureBox1);

                // Evitar copiar sobre sí mismo
                if (!string.Equals(rutaOrigen, rutaDestino, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(rutaOrigen, rutaDestino, true);
                }

                // Cargar imagen sin bloquear el archivo
                ImagenHelper.CargarImagenSinBloqueo(pictureBox1, rutaDestino);

                // Guardar ruta relativa para BD
                imagenRelativa = Path.Combine("Images", "Usuarios", nombreArchivo).Replace("\\", "/");
            }
        }
    }
}
