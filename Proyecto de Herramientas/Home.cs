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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            CargarUserControlLateral(new UC_AdminMenuLateral());

            CargarUserControlContenido(new UC_AdminDashboard());
        }

        public void CargarUserControlLateral(UserControl uc)
        {
            panelLateral.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelLateral.Controls.Add(uc);
        }

        public void CargarUserControlContenido(UserControl uc)
        {
            panelContenedor.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(uc);
        }
    }
}
