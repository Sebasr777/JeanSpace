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

using Proyecto_de_Herramientas;


namespace Proyecto_de_Herramientas
{
    public partial class AgregarProducto : Form
    {
        private string usuarioActual;
        private string imagenRelativaProducto;


        public AgregarProducto(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void LimpiarFormulario()
        {
            txtNombreProducto.Text = "";
            txtTalla.Text = "";
            txtColor.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            ImagenHelper.LiberarImagen(pictureBoxProducto);
            imagenRelativaProducto = null;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {// Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text) ||
                string.IsNullOrWhiteSpace(txtTalla.Text) ||
                string.IsNullOrWhiteSpace(txtColor.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que precio y stock sean numéricos
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Precio debe ser decimal y Stock debe ser entero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que se haya seleccionado una imagen
            if (string.IsNullOrEmpty(imagenRelativaProducto))
            {
                MessageBox.Show("Debes seleccionar una imagen para el producto.", "Imagen requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insertar en la base de datos
            try
            {
                using (SqlConnection conn = BDJeanStore.Conectar())
                {
                    string sql = @"INSERT INTO Productos (Nombre, Talla, Color, Precio, Stock, ImagenPath)
                           VALUES (@Nombre, @Talla, @Color, @Precio, @Stock, @ImagenPath)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombreProducto.Text.Trim());
                        cmd.Parameters.AddWithValue("@Talla", txtTalla.Text.Trim());
                        cmd.Parameters.AddWithValue("@Color", txtColor.Text.Trim());
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.Parameters.AddWithValue("@Stock", stock);
                        cmd.Parameters.AddWithValue("@ImagenPath", imagenRelativaProducto);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el producto:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormAdministrador(usuarioActual).Show();
        }

        private void btnSeleccionarImagenProducto_Click(object sender, EventArgs e)
        {
         
            string carpetaImagenes = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), "Images", "Productos");

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Selecciona una imagen de producto",
                InitialDirectory = carpetaImagenes
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string rutaOrigen = ofd.FileName;
                string nombreProducto = txtNombreProducto.Text.Trim();
                string extension = Path.GetExtension(rutaOrigen);
                string nombreArchivo = $"{nombreProducto}{extension}";

                if (string.IsNullOrEmpty(nombreArchivo))
                {
                    MessageBox.Show("⚠El nombre del archivo no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string rutaDestino = ImagenHelper.ObtenerRutaImagenProducto(nombreArchivo);

                ImagenHelper.LiberarImagen(pictureBoxProducto);

                if (!string.Equals(rutaOrigen, rutaDestino, StringComparison.OrdinalIgnoreCase))
                {
                    File.Copy(rutaOrigen, rutaDestino, true);
                }

                ImagenHelper.CargarImagenSinBloqueo(pictureBoxProducto, rutaDestino);

                imagenRelativaProducto = ImagenHelper.ObtenerRutaRelativaProducto(nombreArchivo);
            }
        }
    }
}
