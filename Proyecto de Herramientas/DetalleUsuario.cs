using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            txtNombre.Text = usuario.Nombre;
            txtUsuarioLogin.Text = usuario.UsuarioLogin;
            txtRol.Text = usuario.Rol;
            txtDireccion.Text = usuario.Direccion;
            txtTelefono.Text = usuario.Telefono;
            txtEmail.Text = usuario.Email;

            // Reconstruir ruta absoluta desde la relativa guardada
            string carpetaProyecto = Directory.GetParent(Application.StartupPath).Parent.Parent.FullName;
            string rutaImagen = Path.Combine(carpetaProyecto, usuario.ImagenPath);

            if (File.Exists(rutaImagen))
            {
                pictureBox1.Image = Image.FromFile(rutaImagen);
                
            }
            else
            {
                pictureBox1.Image = null;
                MessageBox.Show("No se encontró la imagen en: " + rutaImagen);
            }
        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
