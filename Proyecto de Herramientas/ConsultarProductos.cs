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
    public partial class ConsultarProductos : Form
    {
        private string usuarioActual;

        public ConsultarProductos(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void ConsultarProductos_Load(object sender, EventArgs e)
        {
            dgvProductos.DataSource = null;
            dgvProductos.DataSource = DatosProductos.ListaProductos;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormAdministrador(usuarioActual).Show();
        }

        private void linkActualizar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new EditarProducto(usuarioActual).Show();
        }
    }
    }
