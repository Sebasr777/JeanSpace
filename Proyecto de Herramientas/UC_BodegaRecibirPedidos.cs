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
    public partial class UC_BodegaRecibirPedidos : UserControl
    {
        public UC_BodegaRecibirPedidos()
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
                string query = @"SELECT IdVenta, NombreCliente, Fecha, Total
                         FROM Ventas
                         WHERE EstadoId = @EstadoId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@EstadoId", estadoId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPedidos.DataSource = dt;
                lblTotalPedidos.Text = dt.Rows.Count.ToString();
            }
        }

        private void btnRecibirTodos_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos para recibir.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un estado válido.");
                return;
            }

            int estadoActual = Convert.ToInt32(cmbEstado.SelectedValue);
            int estadoRecibido = 3; // Recibido en Bodega

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE Ventas
                             SET EstadoId = @NuevoEstado
                             WHERE EstadoId = @EstadoActual";

                    SqlCommand cmd = new SqlCommand(query, conn, trans);
                    cmd.Parameters.AddWithValue("@NuevoEstado", estadoRecibido);
                    cmd.Parameters.AddWithValue("@EstadoActual", estadoActual);

                    int afectados = cmd.ExecuteNonQuery();
                    trans.Commit();

                    MessageBox.Show($"Se recibieron {afectados} pedidos.");
                    CargarPedidosPorEstado();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al recibir pedidos: " + ex.Message);
                }
            }

        }

        private void UC_BodegaRecibirPedidos_Load(object sender, EventArgs e)
        {
            CargarEstados();
            if (cmbEstado.Items.Count > 0 && cmbEstado.SelectedIndex < 0)
                cmbEstado.SelectedIndex = 0;

            CargarPedidosPorEstado();
        }
    }
}
