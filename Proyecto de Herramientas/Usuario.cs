using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Proyecto_de_Herramientas
{
    public class Usuario
    {
        public int ID { get; set; } // IdUsuario en SQL
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string UsuarioLogin { get; set; }
        public string Contraseña { get; set; }
        public string Rol { get; set; } // NombreRol desde tabla Roles
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string ImagenPath { get; set; }
        public bool Estado { get; set; } // true = activo, false = inactivo
    }
    public static class UsuarioDAO
    {
        public static List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT U.IdUsuario, U.Nombre, U.Direccion, U.UsuarioLogin, U.Contraseña,
                                  R.NombreRol, U.Telefono, U.Email, U.ImagenPath, U.Estado
                           FROM Usuarios U
                           INNER JOIN Roles R ON U.IdRol = R.IdRol";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Direccion = reader.GetString(2),
                            UsuarioLogin = reader.GetString(3),
                            Contraseña = reader.GetString(4),
                            Rol = reader.GetString(5),
                            Telefono = reader.GetString(6),
                            Email = reader.GetString(7),
                            ImagenPath = reader.GetString(8),
                            Estado = reader.GetBoolean(9)
                        });
                    }
                }
            }

            return lista;
        }
    }

}
