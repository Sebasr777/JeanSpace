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
    public partial class UC_AdminMenuLateral : UserControl
    {
        public UC_AdminMenuLateral()
        {
            InitializeComponent();
        }

        private void btninicio_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminDashboard());
        }

        private void btninventario_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminInventario());
        }

        private void btnpedidos_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminPedidos());
        }

        private void btnusuariosyroles_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminUsuariosyRoles());
        }

        private void btncerrarsesion_Click(object sender, EventArgs e)
        {

        }
    }
}
