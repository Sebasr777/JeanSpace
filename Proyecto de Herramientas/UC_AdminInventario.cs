using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_de_Herramientas
{
    public partial class UC_AdminInventario : UserControl
    {
        public UC_AdminInventario()
        {
            InitializeComponent();
        }

        private void UC_AdminInventario_Load(object sender, EventArgs e)
        {
            CargarInventario();
            CargarMétricasInventario();
        }

        private void CargarInventario()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"SELECT IdProducto, Nombre, Talla, Color, Precio, Stock, ImagenPath FROM Productos";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvInventario.DataSource = dt;
            }
        }

        private void dgvInventario_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                var row = dgvInventario.Rows[e.RowIndex].DataBoundItem as DataRowView;
                if (row != null)
                {
                    var producto = new Producto
                    {
                        ID = Convert.ToInt32(row["IdProducto"]),
                        Nombre = row["Nombre"].ToString(),
                        Talla = row["Talla"].ToString(),
                        Color = row["Color"].ToString(),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        Stock = Convert.ToInt32(row["Stock"]),
                        ImagenPath = row["ImagenPath"].ToString()
                    };

                    var editor = new UC_EditarProducto(producto);
                    editor.Dock = DockStyle.Fill;
                    this.Controls.Add(editor);
                    editor.BringToFront();
                }
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var agregar = new UC_AgregarProducto();
            agregar.Dock = DockStyle.Fill;
            this.Controls.Add(agregar);
            agregar.BringToFront();
        }

        private void CargarMétricasInventario()
        {
            try
            {
                int totalProductos = ProductoDAO.ObtenerTotalProductos();      // o ObtenerTotalProductosActivos()
                int stockTotal = ProductoDAO.ObtenerStockTotal();          // o ObtenerStockTotalActivos()

                lblTotalProductos.Text = totalProductos.ToString();
                lblStockTotal.Text = stockTotal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar métricas:\n\n" + ex.Message,
                                "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
