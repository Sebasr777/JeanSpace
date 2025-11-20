using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    public class Cliente
    {
        public int ID { get; set; }          // IdCliente en SQL
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }     // true = activo, false = inactivo
    }

    public static class ClienteDAO
    {
        public static List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT IdCliente, Nombre, Direccion, Telefono, Email, Estado
                               FROM Clientes";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Cliente
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Direccion = reader.GetString(2),
                            Telefono = reader.GetString(3),
                            Email = reader.GetString(4),
                            Estado = reader.GetBoolean(5)
                        });
                    }
                }
            }

            return lista;
        }
    }
}
