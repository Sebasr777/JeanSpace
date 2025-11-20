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
    public partial class UC_AVentasEnviarBodega : UserControl
    {


        public UC_AVentasEnviarBodega()
        {
            InitializeComponent();
            this.Load += UC_AVentasEnviarBodega_Load;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            btnEnviarABodega.Click += btnEnviarABodega_Click;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
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
                string query = @"SELECT V.IdVenta, C.Nombre AS NombreCliente, V.Fecha, V.Total
                 FROM Ventas V
                 INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
                 WHERE V.EstadoId = @EstadoId";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@EstadoId", estadoId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPedidos.DataSource = dt;
                lblTotalPedidos.Text = dt.Rows.Count.ToString();
            }
        }

        private void btnEnviarABodega_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos para enviar.");
                return;
            }

            if (cmbEstado.SelectedValue == null)
            {
                MessageBox.Show("Selecciona un estado válido.");
                return;
            }

            int estadoActual = Convert.ToInt32(cmbEstado.SelectedValue);

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE Ventas 
                         SET EstadoId = @NuevoEstado 
                         WHERE EstadoId = @EstadoActual";

                    SqlCommand cmd = new SqlCommand(query, conn, trans);
                    cmd.Parameters.AddWithValue("@NuevoEstado", 2); // En Bodega
                    cmd.Parameters.AddWithValue("@EstadoActual", estadoActual);

                    int afectados = cmd.ExecuteNonQuery();
                    trans.Commit();

                    MessageBox.Show($"Se enviaron {afectados} pedidos a bodega.");
                    CargarPedidosPorEstado();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al enviar pedidos: " + ex.Message);
                }
            }

        }

        private void UC_AVentasEnviarBodega_Load(object sender, EventArgs e)
        {
            CargarEstados();
            if (cmbEstado.Items.Count > 0 && cmbEstado.SelectedIndex < 0)
                cmbEstado.SelectedIndex = 0;

            CargarPedidosPorEstado();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsHandleCreated) return;
            CargarPedidosPorEstado();
        }

        private int? GetSelectedEstadoId()
        {
            // Maneja los casos de SelectedValue = null o DataRowView
            if (cmbEstado.SelectedValue == null)
            {
                // Alternativa: leer desde SelectedItem cuando es DataRowView
                if (cmbEstado.SelectedItem is DataRowView drv && drv.Row.Table.Columns.Contains("IdEstado"))
                    return Convert.ToInt32(drv["IdEstado"]);
                return null;
            }

            // Si ya es convertible (int, string numérico), conviértelo
            if (cmbEstado.SelectedValue is int i) return i;

            // Si viene como DataRowView
            if (cmbEstado.SelectedValue is DataRowView drv2 && drv2.Row.Table.Columns.Contains("IdEstado"))
                return Convert.ToInt32(drv2["IdEstado"]);

            // Último intento: ToString() -> int
            if (int.TryParse(cmbEstado.SelectedValue.ToString(), out int parsed))
                return parsed;

            return null;
        }
    }
}
