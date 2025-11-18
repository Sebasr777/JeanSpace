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
    public partial class ConsultaUsuarios : Form
    {
        private string usuarioActual;

        public ConsultaUsuarios(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }
        private void CargarUsuarios(string filtro = "")
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT U.IdUsuario, U.Nombre, U.UsuarioLogin, R.NombreRol, 
                              U.Email, U.Telefono, U.Estado, U.Direccion, U.ImagenPath
                       FROM Usuarios U
                       INNER JOIN Roles R ON U.IdRol = R.IdRol";

                if (!string.IsNullOrEmpty(filtro))
                {
                    sql += " WHERE U.Nombre LIKE @Filtro OR U.UsuarioLogin LIKE @Filtro";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(filtro))
                        cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Usuario
                            {
                                ID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                UsuarioLogin = reader.GetString(2),
                                Rol = reader.GetString(3),
                                Email = reader.GetString(4),
                                Telefono = reader.GetString(5),
                                Estado = reader.GetBoolean(6),
                                Direccion = reader.GetString(7),
                                ImagenPath = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            dgvUsuarios.DataSource = lista;
        }
 

        private void ConsultaUsuarios_Load(object sender, EventArgs e)
        {

            CargarUsuarios();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta el formulario actual
            new FormAdministrador(usuarioActual).Show(); // Abre el formulario de destino
        }

        private void linkActualizar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new EditarUsuarios(usuarioActual).Show();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Usuario usuarioSeleccionado = (Usuario)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
                DetalleUsuario detalle = new DetalleUsuario(usuarioSeleccionado);
                detalle.ShowDialog();
            }
        }
    }
}
