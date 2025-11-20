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
  
    public partial class DetalleUsuario : Form
    {
        private Usuario usuario;
        public DetalleUsuario(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            MostrarDatos();
        }
        private void MostrarDatos()
        {
            // Mostrar datos en los TextBox
            txtNombre.Text = usuario.Nombre;
            txtUsuarioLogin.Text = usuario.UsuarioLogin;
            txtRol.Text = usuario.Rol;
            txtDireccion.Text = usuario.Direccion;
            txtTelefono.Text = usuario.Telefono;
            txtEmail.Text = usuario.Email;

            // Reconstruir la ruta absoluta de la imagen
            string carpetaProyecto = RutaProyectoHelper.ObtenerRaizProyecto();

            string rutaImagen = Path.Combine(carpetaProyecto, usuario.ImagenPath);
          

            try
            {
                using (FileStream fs = new FileStream(rutaImagen, FileMode.Open, FileAccess.Read))
                {
                    pictureBox1.Image = Image.FromStream(fs);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir imagen con FileStream:\n" + ex.Message);
            }


        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

  
    }
}
