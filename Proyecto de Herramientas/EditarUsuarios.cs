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

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT U.IdUsuario, U.Nombre, U.UsuarioLogin, U.Contraseña, 
                                      R.NombreRol, U.Direccion, U.Telefono, U.Email
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
                        dgvUsuarios.Visible = false;
                        return;
                    }

                    dgvUsuarios.DataSource = dt;
                    dgvUsuarios.Visible = true;
                }
            }
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value);
            string nombre = dgvUsuarios.CurrentRow.Cells["Nombre"].Value.ToString();
            string usuarioLogin = dgvUsuarios.CurrentRow.Cells["UsuarioLogin"].Value.ToString();
            string contraseña = dgvUsuarios.CurrentRow.Cells["Contraseña"].Value.ToString();
            string direccion = dgvUsuarios.CurrentRow.Cells["Direccion"].Value.ToString();
            string telefono = dgvUsuarios.CurrentRow.Cells["Telefono"].Value.ToString();
            string email = dgvUsuarios.CurrentRow.Cells["Email"].Value.ToString();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"UPDATE Usuarios 
                               SET Nombre=@Nombre, UsuarioLogin=@UsuarioLogin, Contraseña=@Contraseña,
                                   Direccion=@Direccion, Telefono=@Telefono, Email=@Email
                               WHERE IdUsuario=@IdUsuario";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@UsuarioLogin", usuarioLogin);
                    cmd.Parameters.AddWithValue("@Contraseña", contraseña);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                dgvUsuarios.Visible = false;
                txtBuscar.Clear();

                MessageBox.Show("Usuario eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnregresar_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormAdministrador(usuarioActual).Show();
        }

      
    }
}
