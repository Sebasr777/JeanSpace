using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
    public partial class UC_BodegaDashboard : UserControl
    {
        public UC_BodegaDashboard()
        {
            InitializeComponent();
        }

        private void UC_BodegaDashboard_Load(object sender, EventArgs e)
        {CargarResumenBodega();

        }

        private void CargarResumenBodega()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"
            SELECT EstadoId, COUNT(*) AS Total
            FROM Ventas
            GROUP BY EstadoId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Inicializar en cero
                    int porRecibir = 0;
                    int enPreparacion = 0;
                    int listosDespachar = 0;

                    while (reader.Read())
                    {
                        int estado = reader.GetInt32(0);
                        int total = reader.GetInt32(1);

                        switch (estado)
                        {
                            case 1: porRecibir = total; break;
                            case 3: enPreparacion = total; break;
                            case 4: listosDespachar = total; break;
                        }
                    }

                    // Mostrar en etiquetas
                    lblPorRecibir.Text = porRecibir.ToString();
                    lblEnPreparacion.Text = enPreparacion.ToString();
                    lblListosDespachar.Text = listosDespachar.ToString();
                }
            }
        }

    }
}
