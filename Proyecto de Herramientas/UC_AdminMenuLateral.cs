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

        private void inicio_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminDashboard());
        }

        private void inventario_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_AdminInventario());
        }
    }
}
