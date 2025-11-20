using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
    public partial class UC_AdminDashboard : UserControl
    {
        public UC_AdminDashboard()
        {
            InitializeComponent();
        }

        private void UC_AdminDashboard_Load(object sender, EventArgs e)
        {
            CargarResumenDashboard();
            CargarPedidosRecientes();
        }

        private void CargarResumenDashboard()
        {
            MostrarProductosEnStock();
            MostrarPedidosDelDia();
            MostrarVentasDelMes();
            MostrarUsuariosActivos();
        }

        private void MostrarProductosEnStock()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COUNT(*) FROM Productos WHERE Estado = 1 AND Stock > 0";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int productosEnStock = (int)cmd.ExecuteScalar();
                    lblStockNum.Text = productosEnStock.ToString();
                }
            }
        }

        private void MostrarPedidosDelDia()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COUNT(*) FROM Ventas WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int pedidosHoy = (int)cmd.ExecuteScalar();
                    lblPedidosHoy.Text = pedidosHoy.ToString();
                }
            }
        }

        private void MostrarVentasDelMes()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"
            SELECT ISNULL(SUM(Total), 0) 
            FROM Ventas 
            WHERE MONTH(Fecha) = MONTH(GETDATE()) AND YEAR(Fecha) = YEAR(GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    decimal totalMes = (decimal)cmd.ExecuteScalar();
                    lblVentasMes.Text = totalMes.ToString("C0"); // Formato moneda
                }
            }
        }

        private void MostrarUsuariosActivos()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int usuariosActivos = (int)cmd.ExecuteScalar();
                    lblUsuariosOn.Text = usuariosActivos.ToString();
                }
            }
        }

        private void CargarPedidosRecientes()
        {
            List<PedidoResumen> resumen = new List<PedidoResumen>();

            var ventas = VentaDAO.ObtenerRecientes(10);
            var clientes = ClienteDAO.ObtenerTodos();

            foreach (var venta in ventas)
            {
                var cliente = clientes.FirstOrDefault(c => c.ID == venta.IdCliente);
                resumen.Add(new PedidoResumen
                {
                    ID = venta.ID,
                    Cliente = cliente != null ? cliente.Nombre : "Desconocido",
                    Total = venta.Total,
                    Estado = venta.Estado,
                    Fecha = venta.Fecha
                });
            }

            dgvPedidosRecientes.DataSource = resumen;
        }
    }

}
