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
    public partial class UC_AVentasMenuLateral : UserControl
    {
        public UC_AVentasMenuLateral()
        {
            InitializeComponent();
        }

        private void btninicio_Click(object sender, EventArgs e)
        {
            Home AVentasHome = (Home)FindForm();
            AVentasHome.CargarPanelContenido(new UC_AVentasDashboard());
        }

        private void btnregistrarpedido_Click(object sender, EventArgs e)
        {
            Home AVentasHome = (Home)FindForm();
            AVentasHome.CargarPanelContenido(new UC_AVentasRegistrarPedido());
        }

        private void btnenviarabodega_Click(object sender, EventArgs e)
        {
            Home AVentasHome = (Home)FindForm();
            AVentasHome.CargarPanelContenido(new UC_AVentasEnviarBodega());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Home homeForm = (Home)this.FindForm();

            if (homeForm != null)
            {
                homeForm.CerrarSesion();
            }
        }
    }
}
