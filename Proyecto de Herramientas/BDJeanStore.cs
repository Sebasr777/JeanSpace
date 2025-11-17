using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proyecto_de_Herramientas
{
    public class BDJeanStore
    {
        public static SqlConnection Conectar()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = @".\SQLEXPRESS", // Instancia local genérica
                    InitialCatalog = "TiendaRopa", // Nombre de tu base de datos
                    UserID = "usuarioTienda",      // Usuario SQL Server
                    Password = "ProyectoDeHerramientas123"   // Contraseña SQL Server
                };

                SqlConnection conn = new SqlConnection(builder.ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectar con la base de datos: " + ex.Message);
            }
        }
    }
}
