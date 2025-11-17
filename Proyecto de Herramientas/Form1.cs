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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Debe ingresar usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = BDJeanStore.Conectar()) // tu método de conexión
                {
                    string sql = @"SELECT U.IdUsuario, U.Nombre, U.Contraseña, R.NombreRol
                                   FROM Usuarios U
                                   INNER JOIN Roles R ON U.IdRol = R.IdRol
                                   WHERE U.UsuarioLogin = @UsuarioLogin AND U.Estado = 1";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioLogin", usuario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string passBD = reader["Contraseña"].ToString();
                                string rol = reader["NombreRol"].ToString();
                                string nombreUsuario = reader["Nombre"].ToString();

                                if (clave == passBD) // aquí luego puedes encriptar
                                {
                                    MessageBox.Show($"Bienvenido {reader["Nombre"]} - Rol: {rol}", "Bienvenido/a", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Abrir formulario según rol
                                    switch (rol)
                                    {
                                        case "Administrador":
                                            FormAdministrador frmAdmin = new FormAdministrador(nombreUsuario);
                                            frmAdmin.Show();
                                            break;

                                        case "Auxiliar de Ventas":
                                            Pantalla_para_el_vendedor frmVentas = new Pantalla_para_el_vendedor();
                                            frmVentas.Show();
                                            break;

                                        case "Auxiliar de Bodega":
                                            // Cuando tengas listo el formulario de bodega, descomenta:
                                            // FormBodega frmBodega = new FormBodega();
                                            // frmBodega.Show();
                                            MessageBox.Show("El formulario de Bodega aún no está implementado.");
                                            break;
                                    }

                                    this.Hide(); // ocultar login
                                }
                                else
                                {
                                    MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Usuario no encontrado o inactivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar: " + ex.Message);
            }
        }

        private void picVer_Click(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = false;
            picVer.Visible = false;
            picOcultar.Visible = true;
        }

        private void picOcultar_Click(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
            picVer.Visible = true;
            picOcultar.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
            picOcultar.Visible = false;
        }
    }
}
