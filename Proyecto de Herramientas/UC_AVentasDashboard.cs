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
    public partial class UC_AVentasDashboard : UserControl
    {
        public UC_AVentasDashboard()
        {
            InitializeComponent();
            CargarPedidosActivos();
            CargarMétricasDashboard();
        }

        private void UC_AVentasDashboard_Load(object sender, EventArgs e)
        {
            int idUsuarioActual = SesionUsuario.IdUsuario;
        }

        public void CargarPedidosActivos()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"
            SELECT V.IdVenta, C.Nombre AS Cliente, V.Total, E.NombreEstado AS Estado
            FROM Ventas V
            INNER JOIN Clientes C ON V.IdCliente = C.IdCliente
            INNER JOIN EstadosPedido E ON V.EstadoId = E.IdEstado
            WHERE E.NombreEstado IN ('Creado', 'Enviado a Bodega', 'Recibido en Bodega', 'Listo')";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPedidosActivos.DataSource = dt;
            }
        }

        private void CargarMétricasDashboard()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                // Mis pedidos del día (por usuario actual)
                string pedidosDiaQuery = @"
                SELECT COUNT(*) 
                FROM Ventas 
                WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)
                  AND IdUsuario = @IdUsuario";

                // En Bodega
                string enBodegaQuery = @"
                SELECT COUNT(*) 
                FROM Ventas 
                WHERE EstadoId = (
                    SELECT IdEstado FROM EstadosPedido WHERE NombreEstado = 'Enviado a Bodega')";

                // Listos para Cobrar
                string listosQuery = @"
                SELECT COUNT(*) 
                FROM Ventas 
                WHERE EstadoId = (
                    SELECT IdEstado FROM EstadosPedido WHERE NombreEstado = 'Listo')";

                SqlCommand cmdDia = new SqlCommand(pedidosDiaQuery, conn);
                SqlCommand cmdBodega = new SqlCommand(enBodegaQuery, conn);
                SqlCommand cmdListos = new SqlCommand(listosQuery, conn);

                // ✅ Usa el IdUsuario desde sesión
                cmdDia.Parameters.AddWithValue("@IdUsuario", SesionUsuario.IdUsuario);

                int pedidosDia = Convert.ToInt32(cmdDia.ExecuteScalar());
                int enBodega = Convert.ToInt32(cmdBodega.ExecuteScalar());
                int listosCobrar = Convert.ToInt32(cmdListos.ExecuteScalar());

                lblMisPedidosDia.Text = pedidosDia.ToString();
                lblEnBodega.Text = enBodega.ToString();
                lblListosCobrar.Text = listosCobrar.ToString();
            }
        }
    }
}
