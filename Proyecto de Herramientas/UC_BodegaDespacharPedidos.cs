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
    public partial class UC_BodegaDespacharPedidos : UserControl
    {
        public UC_BodegaDespacharPedidos()
        {
            InitializeComponent();
        }

        private void CargarEstados()
        {

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT IdEstado, NombreEstado FROM EstadosPedido";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbEstado.DisplayMember = "NombreEstado";
                cmbEstado.ValueMember = "IdEstado";
                cmbEstado.DataSource = dt;
            }
        }

        private void CargarPedidosPorEstado()
        {
           
            if (cmbEstado.SelectedValue == null) return;
            int estadoId = Convert.ToInt32(cmbEstado.SelectedValue);

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"SELECT V.IdVenta, C.Nombre AS NombreCliente, U.Nombre AS Vendedor, V.Fecha, V.Total, V.EstadoId AS EstadoPedido
                                 FROM Ventas V
                                 INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
                                 INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario
                                 WHERE V.EstadoId = @EstadoId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@EstadoId", estadoId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPedidos.DataSource = dt;
                lblTotalPedidos.Text = $"Total: {dt.Rows.Count}";

                VerificarDespachoCompleto();
            }
        }

        private void btnRecibirTodos_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos para despachar.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un estado válido.");
                return;
            }

            int estadoActual = Convert.ToInt32(cmbEstado.SelectedValue);
            int estadoDespachado = 4; // ✅ Despachado

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE Ventas
                                     SET EstadoId = @NuevoEstado
                                     WHERE EstadoId = @EstadoActual";

                    SqlCommand cmd = new SqlCommand(query, conn, trans);
                    cmd.Parameters.AddWithValue("@NuevoEstado", estadoDespachado);
                    cmd.Parameters.AddWithValue("@EstadoActual", estadoActual);

                    int afectados = cmd.ExecuteNonQuery();
                    trans.Commit();

                    MessageBox.Show($"Se despacharon {afectados} pedidos.");
                    CargarPedidosPorEstado();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al despachar pedidos: " + ex.Message);
                }
            }
        }
        

        private void btnDespacharTodos_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos para despachar.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un estado válido.");
                return;
            }

            int estadoActual = Convert.ToInt32(cmbEstado.SelectedValue);
            int estadoDespachado = 4; // Despachado

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE Ventas
                                     SET EstadoId = @NuevoEstado
                                     WHERE EstadoId = @EstadoActual";

                    SqlCommand cmd = new SqlCommand(query, conn, trans);
                    cmd.Parameters.AddWithValue("@NuevoEstado", estadoDespachado);
                    cmd.Parameters.AddWithValue("@EstadoActual", estadoActual);

                    int afectados = cmd.ExecuteNonQuery();
                    trans.Commit();

                    MessageBox.Show($"Se despacharon {afectados} pedidos.");
                    CargarPedidosPorEstado();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al despachar pedidos: " + ex.Message);
                }
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UC_BodegaDespacharPedidos_Load(object sender, EventArgs e)
        {
            CargarEstados();
            cmbEstado.SelectedValue = 3; // Estado "Recibido en Bodega"
            CargarPedidosPorEstado();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsHandleCreated) return;
            CargarPedidosPorEstado();
        }

        private void VerificarDespachoCompleto()
        {
            // Solo aplica si el filtro está en estado 4 (Despachado)
            if (Convert.ToInt32(cmbEstado.SelectedValue) != 4) return;

            if (dgvPedidos.Rows.Count == 0)
            {
                lblTotalPedidos.Text = "Total: 0";
                return;
            }

            bool todosDespachados = true;

            foreach (DataGridViewRow row in dgvPedidos.Rows)
            {
                // Asegúrate de que la columna se llame "EstadoPedido" en tu SQL
                if (row.Cells["EstadoPedido"].Value == null || Convert.ToInt32(row.Cells["EstadoPedido"].Value) != 4)
                {
                    todosDespachados = false;
                    break;
                }
            }

            if (todosDespachados)
            {
                MessageBox.Show("✅ Todos los pedidos han sido despachados.");
            }
        }
    }
}
