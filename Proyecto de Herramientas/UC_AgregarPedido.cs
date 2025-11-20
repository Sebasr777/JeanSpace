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
            // Cargar productos y tallas disponibles
            CargarProductos();
            CargarTallas();

            // Configurar ListView para mostrar el detalle del pedido
            lvDetallePedido.View = View.Details;
            lvDetallePedido.FullRowSelect = true;
            lvDetallePedido.GridLines = true;

            lvDetallePedido.Columns.Clear();
            lvDetallePedido.Columns.Add("Producto", 150);
            lvDetallePedido.Columns.Add("Talla", 80);
            lvDetallePedido.Columns.Add("Cantidad", 80);
            lvDetallePedido.Columns.Add("Precio Unitario", 100);
            lvDetallePedido.Columns.Add("Subtotal", 100);

            // Configurar NumericUpDown para cantidad
            numericCantidad.Minimum = 1;
            numericCantidad.Maximum = 100;
            numericCantidad.Value = 1;

            // Inicializar total en cero
            lblCantidadTotal.Text = 0m.ToString("C0");

            // Limpiar campos iniciales
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

                        if (nombresUnicos.Add(nombre)) // solo agrega si no existe
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
            decimal precio = preciosProductos[producto];

            var detalle = new DetallePedido
            {
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

                    // ✅ Insertar venta con NombreCliente, sin IdCliente
                    string queryVenta = @"INSERT INTO Ventas (NombreCliente, IdUsuario, Fecha, Total, EstadoId)
                                  OUTPUT INSERTED.IdVenta
                                  VALUES (@NombreCliente, @IdUsuario, GETDATE(), @Total, 1)";

                    using (SqlCommand cmdVenta = new SqlCommand(queryVenta, conn, trans))
                    {
                        cmdVenta.Parameters.AddWithValue("@NombreCliente", txtNombreCliente.Text.Trim());
                        cmdVenta.Parameters.AddWithValue("@IdUsuario", SesionUsuario.IdUsuario);
                        cmdVenta.Parameters.AddWithValue("@Total", total);

                        int idVenta = Convert.ToInt32(cmdVenta.ExecuteScalar());

                        // Insertar detalles
                        foreach (var d in detalles)
                        {
                            string queryDetalle = @"INSERT INTO DetalleVenta (IdVenta, Producto, Talla, Cantidad, PrecioUnitario, Subtotal)
                                            VALUES (@IdVenta, @Producto, @Talla, @Cantidad, @PrecioUnitario, @Subtotal)";

                            using (SqlCommand cmdDet = new SqlCommand(queryDetalle, conn, trans))
                            {
                                cmdDet.Parameters.AddWithValue("@IdVenta", idVenta);
                                cmdDet.Parameters.AddWithValue("@Producto", d.Producto);
                                cmdDet.Parameters.AddWithValue("@Talla", d.Talla);
                                cmdDet.Parameters.AddWithValue("@Cantidad", d.Cantidad);
                                cmdDet.Parameters.AddWithValue("@PrecioUnitario", d.PrecioUnitario);
                                cmdDet.Parameters.AddWithValue("@Subtotal", d.Subtotal);

                                cmdDet.ExecuteNonQuery();
                            }
                        }

                        trans.Commit();
                        MessageBox.Show("Pedido confirmado correctamente.");

                        // Limpiar interfaz
                        detalles.Clear();
                        ActualizarListViewYTotal();
                        txtNombreCliente.Clear();
                        cmbProducto.SelectedIndex = -1;
                        numericCantidad.Value = 1;
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Error al confirmar el pedido: " + ex.Message);
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
    }
}


