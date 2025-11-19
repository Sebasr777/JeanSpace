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
    public partial class VerProductoDetalle : Form
    {
        private int idProducto;
        public VerProductoDetalle(int idProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
        }

        private void VerProductoDetalle_Load(object sender, EventArgs e)
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
                            txtNombreProducto.Text = reader.GetString(0);
                            txtTalla.Text = reader.GetString(1);
                            txtColor.Text = reader.GetString(2);
                            txtPrecio.Text = reader.GetDecimal(3).ToString("N2");
                            txtStock.Text = reader.GetInt32(4).ToString();

                            string imagenRelativa = reader.GetString(5);
                            string rutaCompleta = Path.Combine(RutaProyectoHelper.ObtenerRaizProyecto(), imagenRelativa);

                            ImagenHelper.CargarImagenSinBloqueo(pictureBoxProducto, rutaCompleta);
                        }
                    }
                }
            }

            // Desactivar edición de los campos
            txtNombreProducto.ReadOnly = true;
            txtTalla.ReadOnly = true;
            txtColor.ReadOnly = true;
            txtPrecio.ReadOnly = true;
            txtStock.ReadOnly = true;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
