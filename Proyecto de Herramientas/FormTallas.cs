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
    public partial class FormTallas : Form
    {
        private Producto productoActual;
        public FormTallas(Producto producto)
        {
            InitializeComponent();
            productoActual = producto;
            cmbTallas.Items.AddRange(new string[] { "S", "M", "L", "XL" });
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string talla = cmbTallas.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(talla))
            {
                MessageBox.Show("Selecciona una talla.");
                return;
            }

            // Validar disponibilidad
            if (productoActual.Stock <= 0)
            {
                MessageBox.Show("Este producto no está disponible actualmente.", "Sin stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Agregar al carrito
            DatosCarrito.ListaCarrito.Add(new ItemCarrito
            {
                NombreProducto = productoActual.Nombre,
                TallaSeleccionada = talla,
                Precio = productoActual.Precio
            });

            MessageBox.Show("Producto agregado al carrito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
