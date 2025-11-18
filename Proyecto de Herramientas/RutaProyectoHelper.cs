using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    using System;
    using System.IO;
    using System.Windows.Forms;


    namespace Proyecto_de_Herramientas
    {
        public static class RutaProyectoHelper
        {
            public static string ObtenerRaizProyecto()
            {
                string carpetaActual = Application.StartupPath;

                while (carpetaActual != null)
                {
                    string posibleRuta = Path.Combine(carpetaActual, "Images", "Usuarios");
                    if (Directory.Exists(posibleRuta))
                    {
                        return carpetaActual;
                    }

                    carpetaActual = Directory.GetParent(carpetaActual)?.FullName;
                }

                // Si no se encuentra, usar la carpeta de inicio como fallback
                return Application.StartupPath;
            }
        }
    }

}
