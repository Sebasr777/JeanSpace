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
    public partial class UC_AVentasRegistrarPedido : UserControl
    {
        public UC_AVentasRegistrarPedido()
        {
            InitializeComponent();
        }

        public void CargarPedidos(string estado = "")
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query;

                if (string.IsNullOrEmpty(estado) || estado == "Todos los estados")
                {
                    query = @"SELECT V.IdVenta, C.Nombre AS Cliente, U.Nombre AS Vendedor, 
                             V.Fecha, V.Total, E.NombreEstado AS Estado
                      FROM Ventas V
                      INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
                      INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario
                      INNER JOIN EstadosPedido E ON V.EstadoId = E.IdEstado";
                }
                else
                {
                    query = @"SELECT V.IdVenta, C.Nombre AS Cliente, U.Nombre AS Vendedor, 
                             V.Fecha, V.Total, E.NombreEstado AS Estado
                      FROM Ventas V
                      INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
                      INNER JOIN Usuarios U ON V.IdUsuario = U.IdUsuario
                      INNER JOIN EstadosPedido E ON V.EstadoId = E.IdEstado
                      WHERE E.NombreEstado = @Estado";
                }

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(estado) && estado != "Todos los estados")
                {
                    cmd.Parameters.AddWithValue("@Estado", estado);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPedidos.DataSource = dt;
                lblTotalPedidos.Text = dt.Rows.Count.ToString();
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estadoSeleccionado = cmbEstado.SelectedItem.ToString();
            CargarPedidos(estadoSeleccionado);
        }

        private void UC_AVentasRegistrarPedido_Load(object sender, EventArgs e)
        {
            CargarPedidos();
            cmbEstado.Items.AddRange(new string[]
            {
                "Todos los estados",
                "Creado",
                "Enviado a Bodega",
                "Recibido en Bodega",
                "Despachado"
            });
            cmbEstado.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home AVentasHome = (Home)FindForm();
            AVentasHome.CargarPanelContenido(new UC_AgregarPedido());
        }
    }
}
