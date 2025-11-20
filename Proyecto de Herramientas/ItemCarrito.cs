using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    public class ItemCarrito
    {
        public string NombreProducto { get; set; }
        public string TallaSeleccionada { get; set; }
        public decimal Precio { get; set; }
    }

    public static class DatosCarrito
    {
        public static List<ItemCarrito> ListaCarrito = new List<ItemCarrito>();
    }

}
