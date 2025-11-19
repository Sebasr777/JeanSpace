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
    public partial class EditarUsuarioDetalle : Form
    {
        private int idUsuario;
        public EditarUsuarioDetalle(int idUsuario)
        {
            InitializeComponent();
            this.idUsuario = idUsuario;
            CargarDatos();
        }
        private void CargarDatos()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT Nombre, UsuarioLogin, Contraseña, Direccion, Telefono, Email, ImagenPath
                           FROM Usuarios WHERE IdUsuario=@IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader.GetString(0);
                            txtUsuarioLogin.Text = reader.GetString(1);
                            txtContraseña.Text = reader.GetString(2);
                            txtDireccion.Text = reader.GetString(3);
                            txtTelefono.Text = reader.GetString(4);
                            txtEmail.Text = reader.GetString(5);

                            string imagenRelativa = reader.GetString(6);
                            string rutaImagen = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), imagenRelativa);
                            if (File.Exists(rutaImagen))
                            {
                                picFoto.Image = Image.FromFile(rutaImagen);
                                picFoto.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                        }
                    }
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"UPDATE Usuarios 
                           SET Nombre=@Nombre, UsuarioLogin=@UsuarioLogin, Contraseña=@Contraseña,
                               Direccion=@Direccion, Telefono=@Telefono, Email=@Email
                           WHERE IdUsuario=@IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@UsuarioLogin", txtUsuarioLogin.Text);
                    cmd.Parameters.AddWithValue("@Contraseña", txtContraseña.Text);
                    cmd.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Cambios guardados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCambiarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), "Images", "Usuarios");
                ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";
                ofd.Title = "Seleccionar imagen de usuario";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string rutaOrigen = ofd.FileName;
                    string nombreArchivo = Path.GetFileName(rutaOrigen);
                    string rutaDestino = ImagenHelper.ObtenerRutaImagenUsuario(nombreArchivo);

                    // Verificar si el archivo destino está bloqueado
                    if (File.Exists(rutaDestino) && !ImagenHelper.ArchivoDisponible(rutaDestino))
                    {
                        MessageBox.Show("La imagen actual está siendo usada por otro proceso. Ciérrala antes de continuar.",
                                        "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Liberar imagen del PictureBox y forzar recolección
                    ImagenHelper.LiberarImagen(picFoto);

                    // Obtener ruta anterior desde la BD
                    string rutaAnterior = null;
                    using (SqlConnection conn = BDJeanStore.Conectar())
                    {
                        string sqlSelect = "SELECT ImagenPath FROM Usuarios WHERE IdUsuario=@IdUsuario";
                        using (SqlCommand cmd = new SqlCommand(sqlSelect, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            object resultado = cmd.ExecuteScalar();
                            if (resultado != null && resultado != DBNull.Value)
                            {
                                rutaAnterior = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), resultado.ToString());
                            }
                        }
                    }

                    // Copiar nueva imagen si es distinta
                    if (!string.Equals(rutaOrigen, rutaDestino, StringComparison.OrdinalIgnoreCase))
                    {
                        File.Copy(rutaOrigen, rutaDestino, true);
                    }

                    // Eliminar imagen anterior si es distinta
                    if (!string.IsNullOrEmpty(rutaAnterior) && !string.Equals(rutaAnterior, rutaDestino, StringComparison.OrdinalIgnoreCase))
                    {
                        ImagenHelper.EliminarImagen(rutaAnterior);
                    }

                    // Actualizar BD con nueva ruta relativa
                    string rutaRelativa = ImagenHelper.ObtenerRutaRelativa(nombreArchivo);
                    using (SqlConnection conn = BDJeanStore.Conectar())
                    {
                        string sqlUpdate = "UPDATE Usuarios SET ImagenPath=@ImagenPath WHERE IdUsuario=@IdUsuario";
                        using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                        {
                            cmd.Parameters.AddWithValue("@ImagenPath", rutaRelativa);
                            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Mostrar nueva imagen en el PictureBox sin bloquear el archivo
                    ImagenHelper.CargarImagenSinBloqueo(picFoto, rutaDestino);

                    MessageBox.Show("Imagen actualizada correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }




        }
    }

}
