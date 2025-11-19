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
using static Proyecto_de_Herramientas.PersistenciaProductos;

namespace Proyecto_de_Herramientas
{
    public partial class EditarProducto : Form
    {
        private string usuarioActual;

        public EditarProducto(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void BuscarProductoPorID(int idProducto)
        {
            List<Producto> resultado = new List<Producto>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = "SELECT IdProducto, Nombre, Talla, Color, Precio, Stock, ImagenPath FROM Productos WHERE IdProducto=@IdProducto";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        { 
                            dgvProductos.Visible = true;
                            resultado.Add(new Producto
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
                        else
                        {
                            MessageBox.Show("❌ No se encontró ningún producto con ese ID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }

            dgvProductos.DataSource = resultado;
            dgvProductos.Columns["ImagenPath"].Visible = false;
        }



        private void EditarProducto_Load(object sender, EventArgs e)
        {

            dgvProductos.AutoGenerateColumns = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ReadOnly = true;
            dgvProductos.Visible = false;
        }

    
        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Ingresa un ID de producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtBuscar.Text, out int idProducto))
            {
                MessageBox.Show("El ID debe ser un número entero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BuscarProductoPorID(idProducto);
        }

        private void btnregresar_Click_1(object sender, EventArgs e)
        {
            new FormAdministrador(usuarioActual).Show();
            this.Close();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["ID"].Value);
            string nombre = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();

            var confirmacion = MessageBox.Show($"¿Eliminar el producto '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion == DialogResult.Yes)
            {
                using (SqlConnection conn = BDJeanStore.Conectar())
                {
                    string sql = "DELETE FROM Productos WHERE IdProducto=@IdProducto";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Producto eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.DataSource = null;
                txtBuscar.Text = "";
            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["ID"].Value);
                new EditarProductoDetalle(idProducto).ShowDialog();
                txtBuscar.Text = "";
                dgvProductos.DataSource = null;
            }
        }
    }
}
