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
    public partial class FormAdministrador : Form
    {
        private string usuarioActual;

        public FormAdministrador(string usuario)
        {
            InitializeComponent();
            this.usuarioActual =  usuario;
        }


        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormAgregarUsuario(usuarioActual).Show();
            this.Hide();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConsultaUsuarios(usuarioActual).Show();
            this.Hide();
        }

        private void consultarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new ConsultarProductos(usuarioActual).Show();
            this.Hide();
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AgregarProducto(usuarioActual).Show();
            this.Close();
        }

        private void consultarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //new ConsultarFacturas(usuarioActual).Show();
            this.Close();
        }

        private void FormAdministrador_Load(object sender, EventArgs e)
        {
            lblBienvenida.Text = $"{usuarioActual}";
        }

        private void lblBienvenida_Click(object sender, EventArgs e)
        {

        }
    }
}
