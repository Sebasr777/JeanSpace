using Proyecto_de_Herramientas.Proyecto_de_Herramientas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
    public partial class EditarUsuarios : Form
    {
        private string usuarioActual;
        
        public EditarUsuarios(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string idBuscado = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(idBuscado))
            {
                MessageBox.Show("Ingrese un ID válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dgvUsuarios.Visible = true;
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT U.IdUsuario, U.Nombre, U.UsuarioLogin, U.Contraseña, 
                                      R.NombreRol, U.Direccion, U.Telefono, U.Email, U.ImagenPath
                               FROM Usuarios U
                               INNER JOIN Roles R ON U.IdRol = R.IdRol
                               WHERE U.IdUsuario = @IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsuario", idBuscado);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontró ningún usuario con ese ID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvUsuarios.DataSource = null;
                        return;
                    }

                    dgvUsuarios.DataSource = dt;
                    dgvUsuarios.ReadOnly = false; // editable
                    dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
        }

      

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            string nombre = dgvUsuarios.CurrentRow.Cells["Nombre"].Value.ToString();

            var confirmacion = MessageBox.Show($"¿Estás seguro de eliminar a {nombre}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                using (SqlConnection conn = BDJeanStore.Conectar())
                {
                    string sql = "DELETE FROM Usuarios WHERE IdUsuario=@IdUsuario";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        cmd.ExecuteNonQuery();
                    }
                }

                dgvUsuarios.DataSource = null;
                txtBuscar.Clear();

                MessageBox.Show("Usuario eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormAdministrador(usuarioActual).Show();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells["IdUsuario"].Value);

                // Abrir ventana secundaria
                EditarUsuarioDetalle detalle = new EditarUsuarioDetalle(idUsuario);
                detalle.ShowDialog();

                // Refrescar la tabla después de cerrar el detalle
                CargarUsuarios();
            }

        }

        private void EditarUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void CargarUsuarios(string filtro = "")
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT U.IdUsuario, U.Nombre, U.UsuarioLogin, U.Contraseña, 
                                      R.NombreRol, U.Direccion, U.Telefono, U.Email, U.ImagenPath
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
                                Contraseña = reader.GetString(3),
                                Rol = reader.GetString(4),
                                Direccion = reader.GetString(5),
                                Telefono = reader.GetString(6),
                                Email = reader.GetString(7),
                                ImagenPath = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            dgvUsuarios.DataSource = lista;
            dgvUsuarios.ReadOnly = false; // editable
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

    }
}
