using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Proyecto_de_Herramientas
{
    public class Producto
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }

    public static class PersistenciaProductos
    {
        private static string rutaArchivo = "productos.json";

        public static void GuardarProductos(List<Producto> lista)
        {
            string json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static List<Producto> CargarProductos()
        {
            if (!File.Exists(rutaArchivo))
            {
                var productosBase = new List<Producto>
            {
                new Producto { ID = "4748", Nombre = "Jean Amadora", Talla = "S", Color = "Azul", Precio = 90000, Stock = 10 },
                new Producto { ID = "4897", Nombre = "Jean Straight", Talla = "M", Color = "Azul Oscuro", Precio = 90000, Stock = 8 },
                new Producto { ID = "0300", Nombre = "Jean Bluelvy", Talla = "L", Color = "Índigo", Precio = 90000, Stock = 12 },
                new Producto { ID = "4904", Nombre = "Jean Rooibos", Talla = "S", Color = "Azul Claro", Precio = 90000, Stock = 6 },
                new Producto { ID = "4880", Nombre = "Jean Sea", Talla = "M", Color = "Celeste", Precio = 90000, Stock = 9 },
                new Producto { ID = "0282", Nombre = "Jean Blue Tea", Talla = "L", Color = "Azul Marino", Precio = 90000, Stock = 7 },
                new Producto { ID = "4902", Nombre = "Jean Jasmine", Talla = "S", Color = "Azul", Precio = 90000, Stock = 5 },
                new Producto { ID = "4903", Nombre = "Jean Flare", Talla = "M", Color = "Azul Oscuro", Precio = 90000, Stock = 11 }
            };

                GuardarProductos(productosBase);
                return productosBase;
            }

            string json = File.ReadAllText(rutaArchivo);
            return JsonConvert.DeserializeObject<List<Producto>>(json);
        }
        public static class DatosProductos
        {
            public static List<Producto> ListaProductos = PersistenciaProductos.CargarProductos();
        }


    }
}
