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
    public partial class UC_AgregarProducto : UserControl
    {
        public UC_AgregarProducto()
        {
            InitializeComponent();
        }

        private void btnSeleccionarImgaen_Click(object sender, EventArgs e)
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
                    picImagen.ImageLocation = rutaDestino;
                    picImagen.Tag = ImagenHelper.ObtenerRutaRelativaProducto(nombreArchivo); // Guardar ruta relativa
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo copiar la imagen.\n\n" + ex.Message);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio) || !int.TryParse(txtStock.Text.Trim(), out int stock))
            {
                MessageBox.Show("Precio o Stock inválido.");
                return;
            }

            Producto nuevo = new Producto
            {
                Nombre = txtNombre.Text,
                Talla = txtTalla.Text,
                Color = txtColor.Text,
                Precio = precio,
                Stock = stock,
                ImagenPath = picImagen.Tag?.ToString() ?? ""
            };

            ProductoDAO.Insertar(nuevo);
            MessageBox.Show("Producto agregado correctamente.");
            this.Dispose(); // Cierra el control
        }
    }
}
