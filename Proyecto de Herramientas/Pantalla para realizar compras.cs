using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
  
    public partial class Pantalla_para_realizar_compras : Form
    {
        private Pantalla_para_el_vendedor Ventanaanterior;
        public Pantalla_para_realizar_compras(Pantalla_para_el_vendedor ventanaanterior)
        {
            InitializeComponent();
            Ventanaanterior = ventanaanterior;
        }

        /*public void AgregarFila(string nombre, double precio)
        {
            dvgProductos.Rows.Add(nombre, precio);
        }*/

  


        private void picFinalizarCompra_Click(object sender, EventArgs e)
        {
            if (DatosCarrito.ListaCarrito.Count == 0)
            {
                MessageBox.Show("El carrito está vacío.", "Sin productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Calcular total
            decimal total = DatosCarrito.ListaCarrito.Sum(item => item.Precio);

            // Mostrar resumen
            MessageBox.Show($"Compra finalizada.\nTotal a pagar: ${total:N0}", "Gracias por tu compra", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar carrito y tabla
            DatosCarrito.ListaCarrito.Clear();
            dvgProductos.DataSource = null;
            new Pantalla_para_el_vendedor().Show();
            this.Close();
        }

        private void Pantalla_para_realizar_compras_Load(object sender, EventArgs e)
        {
            dvgProductos.DataSource = null;
            dvgProductos.DataSource = DatosCarrito.ListaCarrito;
        }

        private void btnregresar_Click_1(object sender, EventArgs e)
        {
            // Crear una instancia de la ventana anterior
            Pantalla_para_el_vendedor ventanaAnterior = new Pantalla_para_el_vendedor();

            // Mostrar la ventana anterior
            ventanaAnterior.Show();

            // Cerrar la ventana actual
            this.Close();
        }

        private void picSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
