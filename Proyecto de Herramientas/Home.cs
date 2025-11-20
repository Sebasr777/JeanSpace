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
        private string _rolUsuario;
        private string _nombreUsuario;

        public Home(string rol, string nombreUsuario)
        {
            InitializeComponent();

            _rolUsuario = rol;
            _nombreUsuario = nombreUsuario;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            CargarInterfazPorRol();
        }

        private void CargarInterfazPorRol()
        {
            switch (_rolUsuario)
            {
                case "Administrador":
                    CargarPanelLateral(new UC_AdminMenuLateral());
                    CargarPanelContenido(new UC_AdminDashboard());
                    break;

                case "Auxiliar de Ventas":
                    CargarPanelLateral(new UC_AVentasMenuLateral());
                    CargarPanelContenido(new UC_AVentasDashboard());
                    break;

                case "Auxiliar de Bodega":
                    CargarPanelLateral(new UC_BodegaMenuLateral());
                    CargarPanelContenido(new UC_BodegaDashboard());
                    break;
            }
        }

        public void CargarPanelLateral(UserControl uc)
        {
            panelLateral.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelLateral.Controls.Add(uc);
        }

        public void CargarPanelContenido(UserControl uc)
        {
            panelContenedor.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContenedor.Controls.Add(uc);
        }

        public void CerrarSesion()
        {
            DialogResult r = MessageBox.Show("¿Desea cerrar sesión?",
                                             "Confirmación",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                login frmLogin = new login();
                frmLogin.Show();

                this.Close();
            }
        }
    }
}
