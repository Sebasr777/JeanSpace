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
    public partial class UC_AgregarPedido : UserControl
    {
        private List<DetallePedido> detalles = new List<DetallePedido>();
        private Dictionary<string, decimal> preciosProductos = new Dictionary<string, decimal>();

        public UC_AgregarPedido()
        {
            InitializeComponent();
        }

        private void UC_AgregarPedido_Load(object sender, EventArgs e)
        {
            CargarProductos();
            CargarTallas();

            lvDetallePedido.View = View.Details;
            lvDetallePedido.FullRowSelect = true;
            lvDetallePedido.GridLines = true;

            lvDetallePedido.Columns.Clear();
            lvDetallePedido.Columns.Add("Producto", 150);
            lvDetallePedido.Columns.Add("Talla", 80);
            lvDetallePedido.Columns.Add("Cantidad", 80);
            lvDetallePedido.Columns.Add("Precio Unitario", 100);
            lvDetallePedido.Columns.Add("Subtotal", 100);

            numericCantidad.Minimum = 1;
            numericCantidad.Maximum = 100;
            numericCantidad.Value = 1;

            lblCantidadTotal.Text = 0m.ToString("C0");

            txtNombreCliente.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbTalla.SelectedIndex = 0;
        }

        private void CargarProductos()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT Nombre, Precio FROM Productos WHERE Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    cmbProducto.Items.Clear();
                    preciosProductos.Clear();
                    HashSet<string> nombresUnicos = new HashSet<string>();

                    while (reader.Read())
                    {
                        string nombre = reader.GetString(0);
                        decimal precio = reader.GetDecimal(1);

                        if (nombresUnicos.Add(nombre))
                        {
                            cmbProducto.Items.Add(nombre);
                            preciosProductos[nombre] = precio;
                        }
                    }
                }
            }
        }

        private void CargarTallas()
        {
            cmbTalla.Items.Clear();
            cmbTalla.Items.AddRange(new string[] { "XS", "S", "M", "L", "XL", "XXL" });
            cmbTalla.SelectedIndex = 0;
        }

        private void ActualizarListView()
        {
            lvDetallePedido.Items.Clear();
            foreach (var d in detalles)
            {
                var item = new ListViewItem(d.Producto);
                item.SubItems.Add(d.Talla);
                item.SubItems.Add(d.Cantidad.ToString());
                item.SubItems.Add(d.PrecioUnitario.ToString("C0"));
                item.SubItems.Add(d.Subtotal.ToString("C0"));
                lvDetallePedido.Items.Add(item);
            }

            lblCantidadTotal.Text = detalles.Sum(d => d.Subtotal).ToString("C0");
        }

        private void btnAgregarAlPedido_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un producto.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Escribe el nombre del cliente.");
                return;
            }

            string producto = cmbProducto.SelectedItem.ToString();
            string talla = cmbTalla.SelectedItem?.ToString() ?? "";
            int cantidad = (int)numericCantidad.Value;

            int idProducto = 0;
            decimal precio = 0;

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT IdProducto, Precio FROM Productos WHERE Nombre = @Nombre AND Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", producto);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idProducto = Convert.ToInt32(reader["IdProducto"]);
                            precio = Convert.ToDecimal(reader["Precio"]);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el producto en la base de datos.");
                            return;
                        }
                    }
                }
            }

            var detalle = new DetallePedido
            {
                IdProducto = idProducto,
                Producto = producto,
                Talla = talla,
                Cantidad = cantidad,
                PrecioUnitario = precio
            };

            detalles.Add(detalle);
            ActualizarListViewYTotal();
        }

        private void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            if (detalles.Count == 0)
            {
                MessageBox.Show("No has agregado productos al pedido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Escribe el nombre del cliente.");
                return;
            }

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    decimal total = detalles.Sum(d => d.Subtotal);

                    // Insertar cliente
                    int idCliente;
                    string queryCliente = @"INSERT INTO Clientes (Nombre, Estado)
                                            OUTPUT INSERTED.IdCliente
                                            VALUES (@Nombre, 1)";
                    using (SqlCommand cmdCliente = new SqlCommand(queryCliente, conn, trans))
                    {
                        cmdCliente.Parameters.AddWithValue("@Nombre", txtNombreCliente.Text.Trim());
                        idCliente = Convert.ToInt32(cmdCliente.ExecuteScalar());
                    }

                    // Insertar venta
                    int idVenta;
                    string queryVenta = @"INSERT INTO Ventas (IdCliente, IdUsuario, Fecha, Total, EstadoId)
                                          OUTPUT INSERTED.IdVenta
                                          VALUES (@IdCliente, @IdUsuario, GETDATE(), @Total, 1)";
                    using (SqlCommand cmdVenta = new SqlCommand(queryVenta, conn, trans))
                    {
                        cmdVenta.Parameters.AddWithValue("@IdCliente", idCliente);
                        cmdVenta.Parameters.AddWithValue("@IdUsuario", SesionUsuario.IdUsuario);
                        cmdVenta.Parameters.AddWithValue("@Total", total);
                        idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());
                    }

                    // Insertar detalles
                    foreach (var d in detalles)
                    {
                        string queryDetalle = @"INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, PrecioUnitario)
                                                VALUES (@IdVenta, @IdProducto, @Cantidad, @PrecioUnitario)";
                        using (SqlCommand cmdDet = new SqlCommand(queryDetalle, conn, trans))
                        {
                            cmdDet.Parameters.AddWithValue("@IdVenta", idVenta);
                            cmdDet.Parameters.AddWithValue("@IdProducto", d.IdProducto);
                            cmdDet.Parameters.AddWithValue("@Cantidad", d.Cantidad);
                            cmdDet.Parameters.AddWithValue("@PrecioUnitario", d.PrecioUnitario);
                            cmdDet.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    MessageBox.Show("Pedido confirmado correctamente.");
                    detalles.Clear();
                    ActualizarListViewYTotal();
                    txtNombreCliente.Clear();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al confirmar el pedido:\n" + ex.Message);
                }
            }

        }

        private void ActualizarListViewYTotal()
        {
            lvDetallePedido.Items.Clear();
            foreach (var d in detalles)
            {
                var item = new ListViewItem(d.Producto);
                item.SubItems.Add(d.Talla);
                item.SubItems.Add(d.Cantidad.ToString());
                item.SubItems.Add(d.PrecioUnitario.ToString("C0"));
                item.SubItems.Add(d.Subtotal.ToString("C0"));
                lvDetallePedido.Items.Add(item);
            }

            lblCantidadTotal.Text = detalles.Sum(d => d.Subtotal).ToString("C0");
        }

        private void lvDetallePedido_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


