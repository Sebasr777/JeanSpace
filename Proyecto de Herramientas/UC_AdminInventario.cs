using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_de_Herramientas
{
    public partial class UC_AdminInventario : UserControl
    {
        public UC_AdminInventario()
        {
            InitializeComponent();
            // Configuración inicial del texto marcador de posición
            txtbuscar.Text = "Escriba aquí su nombre de usuario...";
            txtbuscar.ForeColor = Color.Gray; // Usar un color tenue

            // Asociar los manejadores de eventos
            txtbuscar.Enter += new EventHandler(txtbuscar_Enter);
            txtbuscar.Leave += new EventHandler(txtbuscar_Leave);
        }

        private void UC_AdminInventario_Load(object sender, EventArgs e)
        {

        }

        private void txtbuscar_Enter(object sender, EventArgs e)
        {
            // Si el texto actual es el marcador de posición, lo borramos
            if (txtbuscar.Text == "Busca por nombre, código o color...")
            {
                txtbuscar.Text = "";
                txtbuscar.ForeColor = Color.Black; // Volver al color normal
            }
        }

        private void txtbuscar_Leave(object sender, EventArgs e)
        {
            // Si el usuario no escribió nada, restauramos el marcador de posición
            if (string.IsNullOrWhiteSpace(txtbuscar.Text))
            {
                txtbuscar.Text = "Escriba aquí su nombre de usuario...";
                txtbuscar.ForeColor = Color.Gray;
            }
        }
    }
}
