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
            // Conexión de eventos
            this.Load += UC_AVentasEnviarBodega_Load;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            btnEnviarABodega.Click += btnEnviarABodega_Click;

            // Opcional: evita que el usuario escriba texto no existente
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CargarEstados()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT IdEstado, NombreEstado FROM EstadosPedido";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Primero: asignar DisplayMember/ValueMember
                    cmbEstado.DisplayMember = "NombreEstado";
                    cmbEstado.ValueMember = "IdEstado";
                    // Luego: asignar DataSource
                    cmbEstado.DataSource = dt;
                }
            }
        }

        private void CargarPedidosPorEstado()
        {
            int? estadoId = GetSelectedEstadoId();
            if (estadoId == null)
            {
                dgvPedidos.DataSource = null;
                lblTotalPedidos.Text = "0";
                return;
            }

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"SELECT IdVenta, NombreCliente, Fecha, Total
                                 FROM Ventas
                                 WHERE EstadoId = @EstadoId";

                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@EstadoId", estadoId.Value);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPedidos.DataSource = dt;
                    lblTotalPedidos.Text = dt.Rows.Count.ToString();
                }
            }
        }

        private void btnEnviarABodega_Click(object sender, EventArgs e)
        {
            if (dgvPedidos.Rows.Count == 0)
            {
                MessageBox.Show("No hay pedidos para enviar.");
                return;
            }

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    string query = @"UPDATE Ventas 
                             SET EstadoId = @NuevoEstado 
                             WHERE EstadoId = @EstadoActual";

                    using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                    {
                        cmd.Parameters.AddWithValue("@NuevoEstado", 2); // En Bodega
                        cmd.Parameters.AddWithValue("@EstadoActual", Convert.ToInt32(cmbEstado.SelectedValue));
                        int afectados = cmd.ExecuteNonQuery();

                        trans.Commit();
                        MessageBox.Show($"Se enviaron {afectados} pedidos a bodega.");
                        CargarPedidosPorEstado(); // refrescar
                    }
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
            // Seleccionar primer estado si existe para evitar SelectedValue = null
            if (cmbEstado.Items.Count > 0 && cmbEstado.SelectedIndex < 0)
                cmbEstado.SelectedIndex = 0;

            CargarPedidosPorEstado();
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Evita ejecutar mientras el ComboBox aún está bindando
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
