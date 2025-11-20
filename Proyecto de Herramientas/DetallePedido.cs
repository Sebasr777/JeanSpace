using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    public class DetallePedido
    {
        public int IdProducto { get; set; }           // Se usa en el INSERT
        public string Producto { get; set; }          // Para mostrar en el ListView
        public string Talla { get; set; }             // Para mostrar y lógica
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
