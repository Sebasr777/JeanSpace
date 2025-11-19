using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto_de_Herramientas.PersistenciaProductos;

namespace Proyecto_de_Herramientas
{
    public partial class Pantalla_para_el_vendedor : Form
    {
        public int contador_ventas = 0;
        public Pantalla_para_realizar_compras formCarrito;
       
        public Pantalla_para_el_vendedor()
        {
            InitializeComponent();
            formCarrito = new Pantalla_para_realizar_compras(this);
        }

     

        private void btnCarritoparacomprar_Click(object sender, EventArgs e)
        {
            formCarrito.Show();
            formCarrito.BringToFront();
            this.Hide();
        }


        private void picSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int refProducto = 301;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnAgregardeJeanArmadora_Click(object sender, EventArgs e)
        {
            int refProducto = 4748;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnAgregardeJeanStraight_Click(object sender, EventArgs e)
        {
            int refProducto = 4897;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnJeanBluelvy_Click(object sender, EventArgs e)
        {
            int refProducto = 300;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnJeanRooibos_Click(object sender, EventArgs e)
        {
            int refProducto = 4904;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnJeanSea_Click(object sender, EventArgs e)
        {
            int refProducto = 4880;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void BtnJeanBlueTea_Click(object sender, EventArgs e)
        {
            int refProducto = 282;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

        private void btnJeanFlare_Click(object sender, EventArgs e)
        {
            int refProducto = 4903;

            var producto = DatosProductos.ListaProductos.FirstOrDefault(p => p.ID == refProducto);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            FormTallas frm = new FormTallas(producto);
            frm.ShowDialog();
        }

    }
}
