using Proyecto_de_Herramientas.Proyecto_de_Herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
    public partial class UC_EditarProducto : UserControl
    {
        private Producto productoActual;

        public UC_EditarProducto(Producto producto)
        {
            InitializeComponent();
            productoActual = producto;
            CargarDatos();            
        }

        private void btnCambiarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Imágenes|*.jpg;*.png;*.jpeg";

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivo = Path.GetFileName(dialogo.FileName);
                string rutaDestino = ImagenHelper.ObtenerRutaImagenProducto(nombreArchivo);

                try
                {
                    File.Copy(dialogo.FileName, rutaDestino, true);
                    productoActual.ImagenPath = ImagenHelper.ObtenerRutaRelativaProducto(nombreArchivo);
                    ImagenHelper.CargarImagenSinBloqueo(picImagen, rutaDestino);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo copiar la imagen.\n\n" + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarDatos()
        {
            if (productoActual == null)
            {
                MessageBox.Show("No se recibió ningún producto para editar.");
                return;
            }

            txtNombre.Text = productoActual.Nombre;
            txtTalla.Text = productoActual.Talla;
            txtColor.Text = productoActual.Color;
            txtPrecio.Text = productoActual.Precio.ToString();
            txtStock.Text = productoActual.Stock.ToString();
            picImagen.ImageLocation = productoActual.ImagenPath;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || !int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Precio o stock inválido.");
                return;
            }

            productoActual.Nombre = txtNombre.Text;
            productoActual.Talla = txtTalla.Text;
            productoActual.Color = txtColor.Text;
            productoActual.Precio = precio;
            productoActual.Stock = stock;

            // Ya está asignado en btnCambiarImagen si se cambió
            // Si no se cambió, se mantiene la ruta original

            ProductoDAO.Actualizar(productoActual);
            MessageBox.Show("Producto actualizado correctamente.");
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("¿Estás seguro de eliminar este producto?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                ProductoDAO.Eliminar(productoActual.ID);
                MessageBox.Show("Producto eliminado.");
                this.Dispose(); // Cierra el UserControl

            }
        }

        private void btnProbarImagen_Click(object sender, EventArgs e)
        {
            string ruta = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), productoActual.ImagenPath);
            MessageBox.Show("Ruta: " + ruta);

            if (File.Exists(ruta))
            {
                ImagenHelper.LiberarImagen(picImagen);
                ImagenHelper.CargarImagenSinBloqueo(picImagen, ruta);
                MessageBox.Show("Imagen cargada correctamente.");
            }
            else
            {
                MessageBox.Show("La imagen no existe.");
            }
        }
    }
}
