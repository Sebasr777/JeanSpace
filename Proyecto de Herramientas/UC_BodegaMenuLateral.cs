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
    public partial class UC_BodegaMenuLateral : UserControl
    {
        public UC_BodegaMenuLateral()
        {
            InitializeComponent();
        }

        private void btninicio_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_BodegaDashboard());
        }

        private void btnrecibirpedidos_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_BodegaRecibirPedidos());
        }

        private void btndespacharpedidos_Click(object sender, EventArgs e)
        {
            Home AdminHome = (Home)FindForm();
            AdminHome.CargarPanelContenido(new UC_BodegaDespacharPedidos());
        }
    }
}
