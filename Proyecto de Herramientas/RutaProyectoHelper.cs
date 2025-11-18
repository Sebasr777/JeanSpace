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
    using System.Drawing;



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

        public static class ImagenHelper
        {
            public static string ObtenerRutaImagenUsuario(string nombreArchivo)
            {
                // Carpeta raíz del proyecto
                string carpetaProyecto = RutaProyectoHelper.ObtenerRaizProyecto();

                // Carpeta destino para las imágenes de usuarios
                string carpetaDestino = Path.Combine(carpetaProyecto, "Images", "Usuarios");

                // Crear carpeta si no existe
                Directory.CreateDirectory(carpetaDestino);

                // Ruta completa del archivo
                string rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);

                return rutaCompleta;
            }

            public static bool ArchivoDisponible(string ruta)
            {
                try
                {
                    using (FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        // Si se puede abrir sin compartir, está disponible
                        return true;
                    }
                }
                catch (IOException)
                {
                    return false;
                }
            }

            public static void EliminarImagen(string rutaCompleta)
            {
                if (string.IsNullOrEmpty(rutaCompleta)) return;

                try
                {
                    if (File.Exists(rutaCompleta))
                    {
                        File.Delete(rutaCompleta);
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("No se pudo eliminar la imagen anterior.\n\n" + ex.Message,
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            public static string ObtenerRutaRelativa(string nombreArchivo)
            {
                return Path.Combine("Images", "Usuarios", nombreArchivo);
            }

           

            public static void LiberarImagen(PictureBox pic)
            {
                if (pic.Image != null)
                {
                    pic.Image.Dispose();
                    pic.Image = null;
                }

                // Forzar liberación de recursos
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            public static void CargarImagenSinBloqueo(PictureBox pic, string ruta)
            {
                if (string.IsNullOrEmpty(ruta) || !File.Exists(ruta))
                    return;

                try
                {
                    if (pic.Image != null)
                    {
                        pic.Image.Dispose();
                        pic.Image = null;
                    }

                    using (FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    {
                        pic.Image = Image.FromStream(fs);
                    }

                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("⚠️ No se pudo cargar la imagen.\n\n" + ex.Message,
                                    "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }




        }
    }


}
