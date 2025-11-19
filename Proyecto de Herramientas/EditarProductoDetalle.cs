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
    public partial class EditarProductoDetalle : Form
    {
        private int idProducto;
        private string rutaImagenActual;
        public EditarProductoDetalle(int idProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
        }

        private void EditarProductoDetalle_Load(object sender, EventArgs e)
        {
            CargarProducto();
        }

        private void CargarProducto()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = "SELECT Nombre, Talla, Color, Precio, Stock, ImagenPath FROM Productos WHERE IdProducto=@IdProducto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNombre.Text = reader.GetString(0);
                            txtTalla.Text = reader.GetString(1);
                            txtColor.Text = reader.GetString(2);
                            txtPrecio.Text = reader.GetDecimal(3).ToString("N2");
                            txtStock.Text = reader.GetInt32(4).ToString();

                            rutaImagenActual = reader.GetString(5);
                            string rutaCompleta = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), rutaImagenActual);
                            ImagenHelper.CargarImagenSinBloqueo(picProducto, rutaCompleta);
                        }
                    }
                }
            }
        }

          
      

     

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("El stock debe ser un número entero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"UPDATE Productos 
                               SET Nombre=@Nombre, Talla=@Talla, Color=@Color, Precio=@Precio, Stock=@Stock, ImagenPath=@ImagenPath 
                               WHERE IdProducto=@IdProducto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Talla", txtTalla.Text);
                    cmd.Parameters.AddWithValue("@Color", txtColor.Text);
                    cmd.Parameters.AddWithValue("@Precio", precio);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@ImagenPath", rutaImagenActual);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCambiarImagenProducto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string nombreArchivo = Path.GetFileName(ofd.FileName);
                string rutaDestino = ImagenHelper.ObtenerRutaImagenProducto(nombreArchivo);

                // Copiar la nueva imagen
                File.Copy(ofd.FileName, rutaDestino, true);

                rutaImagenActual = ImagenHelper.ObtenerRutaRelativaProducto(nombreArchivo);
                ImagenHelper.CargarImagenSinBloqueo(picProducto, rutaDestino);
            }
        }
    }
}
        
    

