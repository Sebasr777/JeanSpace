using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto_de_Herramientas.PersistenciaProductos;

namespace Proyecto_de_Herramientas
{
    public partial class AgregarProducto : Form
    {
        private string usuarioActual;

        public AgregarProducto(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTalla.Text) ||
                string.IsNullOrWhiteSpace(txtColor.Text) ||
                string.IsNullOrWhiteSpace(txtPrecio.Text) ||
                string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el ID ya existe
            if (DatosProductos.ListaProductos.Any(p => p.ID == txtID.Text.Trim()))
            {
                MessageBox.Show("Ya existe un producto con ese ID.", "ID duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear nuevo producto
            Producto nuevo = new Producto
            {
                ID = txtID.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Talla = txtTalla.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text.Trim()),
                Stock = int.Parse(txtStock.Text.Trim())
            };

            // Guardar en la lista y en el archivo
            DatosProductos.ListaProductos.Add(nuevo);
            PersistenciaProductos.GuardarProductos(DatosProductos.ListaProductos);

            MessageBox.Show("Producto guardado correctamente.", "Producto agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtID.Clear();
            txtNombre.Clear();
            txtTalla.Clear();
            txtColor.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            new FormAdministrador(usuarioActual).Show();
        }
    }
}
