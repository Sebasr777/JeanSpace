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
    public partial class UC_AdminUsuariosyRoles : UserControl
    {
        public UC_AdminUsuariosyRoles()
        {
            InitializeComponent();
        }

        private void CargarUsuarios()
        {

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = @"SELECT U.IdUsuario, 
                                        U.Nombre, 
                                        U.UsuarioLogin,
                                        U.Contraseña,
                                        U.Email, 
                                        U.Direccion,
                                        U.Telefono,
                                        R.NombreRol AS Rol, 
                                        U.ImagenPath
                                 FROM Usuarios U
                                 INNER JOIN Roles R ON U.IdRol = R.IdRol";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUsuarios.DataSource = dt;

                // Opcional: ocultar columnas sensibles
                dgvUsuarios.Columns["Contraseña"].Visible = false;
                dgvUsuarios.Columns["UsuarioLogin"].Visible = false;
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsuarios.Rows[e.RowIndex];

            var usuario = new Usuario
            {
                ID = Convert.ToInt32(row.Cells["IdUsuario"].Value),
                Nombre = row.Cells["Nombre"].Value?.ToString() ?? "",
                UsuarioLogin = row.Cells["UsuarioLogin"].Value?.ToString() ?? "",
                Contraseña = row.Cells["Contraseña"].Value?.ToString() ?? "",
                Email = row.Cells["Email"].Value?.ToString() ?? "",
                Direccion = row.Cells["Direccion"].Value?.ToString() ?? "",
                Telefono = row.Cells["Telefono"].Value?.ToString() ?? "",
                Rol = row.Cells["Rol"].Value?.ToString() ?? "",
                ImagenPath = row.Cells["ImagenPath"].Value?.ToString() ?? ""
            };

            var editor = new UC_EditarUsuario(usuario);
            editor.Dock = DockStyle.Fill;
            this.Controls.Add(editor);
            editor.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var agregar = new UC_AgregarUsuario();
            agregar.Dock = DockStyle.Fill;
            this.Controls.Add(agregar);
            agregar.BringToFront();
        }

        private void UC_AdminUsuariosyRoles_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            MostrarTotalUsuarios();
        }
        private void MostrarTotalUsuarios()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Estado = 1"; // solo activos

                SqlCommand cmd = new SqlCommand(query, conn);
               
                int total = (int)cmd.ExecuteScalar();
               
                lblTotalUsuarios.Text = total.ToString();
            }
        }

    }
}
