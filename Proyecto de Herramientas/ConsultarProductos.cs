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


namespace Proyecto_de_Herramientas
{
    public partial class ConsultarProductos : Form
    {
        private string usuarioActual;

        public ConsultarProductos(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void ConsultarProductos_Load(object sender, EventArgs e)
        {
            dgvProductos.AutoGenerateColumns = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;

            CargarProductos();
        }
        private void CargarProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = "SELECT IdProducto, Nombre, Talla, Color, Precio, Stock, ImagenPath FROM Productos";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Talla = reader.GetString(2),
                            Color = reader.GetString(3),
                            Precio = reader.GetDecimal(4),
                            Stock = reader.GetInt32(5),
                            ImagenPath = reader.GetString(6)
                        });
                    }
                }
            }

            dgvProductos.DataSource = lista;
            dgvProductos.Columns["ImagenPath"].Visible = false; // ocultar ruta
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormAdministrador(usuarioActual).Show();
        }

        private void linkActualizar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new EditarProducto(usuarioActual).Show();
        }

   

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["ID"].Value);
                new VerProductoDetalle(idProducto).ShowDialog(); // solo visualización
            }
        }
    }
    }
