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
    public partial class EditarProducto : Form
    {
        private string usuarioActual;

        public EditarProducto(string usuario)
        {
            InitializeComponent();
            this.usuarioActual = usuario;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim();

            var filtrados = DatosProductos.ListaProductos
                .Where(p => p.ID.ToString().Contains(filtro))
                .ToList();

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = filtrados;
        }


  
        private void EditarProducto_Load(object sender, EventArgs e)
        {
            dgvProductos.Visible = false;
        }

    
        private void btnConsultar_Click_1(object sender, EventArgs e)
        {
            string idBuscado = txtBuscar.Text.Trim();

            if (string.IsNullOrEmpty(idBuscado))
            {
                MessageBox.Show("Ingresa un ID de producto para consultar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoFiltrado = DatosProductos.ListaProductos
                .Where(p => p.ToString() == idBuscado)
                .ToList();

            if (productoFiltrado.Count == 0)
            {
                MessageBox.Show("No se encontró ningún producto con ese ID.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvProductos.Visible = false;
                return;
            }

            dgvProductos.DataSource = null;
            dgvProductos.DataSource = productoFiltrado;
            dgvProductos.Visible = true;
        }

        private void btnregresar_Click_1(object sender, EventArgs e)
        {
            new FormAdministrador(usuarioActual).Show();
            this.Close();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvProductos.DataSource == null || dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos cargados para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var productosMostrados = dgvProductos.DataSource as List<Producto>;

            if (productosMostrados == null || productosMostrados.Count != 1)
            {
                MessageBox.Show("Solo se puede eliminar un producto a la vez.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoAEliminar = productosMostrados[0];

            var confirmacion = MessageBox.Show($"¿Eliminar el producto '{productoAEliminar.Nombre}' con REF {productoAEliminar.ID}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacion == DialogResult.Yes)
            {
                DatosProductos.ListaProductos.RemoveAll(p => p.ID == productoAEliminar.ID);
                PersistenciaProductos.GuardarProductos(DatosProductos.ListaProductos);

                dgvProductos.DataSource = null;
                dgvProductos.Visible = false;
                txtBuscar.Clear();

                MessageBox.Show("Producto eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            var productosEditados = dgvProductos.DataSource as List<Producto>;

            if (productosEditados == null)
            {
                MessageBox.Show("No hay productos para actualizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Actualizar la lista completa
            foreach (var productoEditado in productosEditados)
            {
                var original = DatosProductos.ListaProductos
                    .FirstOrDefault(p => p.ID == productoEditado.ID);

                if (original != null)
                {
                    original.Nombre = productoEditado.Nombre;
                    original.Talla = productoEditado.Talla;
                    original.Color = productoEditado.Color;
                    original.Precio = productoEditado.Precio;
                    original.Stock = productoEditado.Stock;
                }
            }

            PersistenciaProductos.GuardarProductos(DatosProductos.ListaProductos);
            MessageBox.Show("Cambios guardados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
