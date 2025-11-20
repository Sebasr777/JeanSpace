using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_de_Herramientas
{
    public class Venta
    {
        public int ID { get; set; }              // IdVenta en SQL
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }       // NombreEstado desde tabla EstadosPedido
    }

    public static class VentaDAO
    {
        public static List<Venta> ObtenerRecientes(int cantidad = 10)
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT TOP (@cantidad) V.IdVenta, V.IdCliente, V.IdUsuario, V.Fecha, V.Total, E.NombreEstado
                               FROM Ventas V
                               INNER JOIN EstadosPedido E ON V.EstadoId = E.IdEstado
                               ORDER BY V.Fecha DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Venta
                            {
                                ID = reader.GetInt32(0),
                                IdCliente = reader.GetInt32(1),
                                IdUsuario = reader.GetInt32(2),
                                Fecha = reader.GetDateTime(3),
                                Total = reader.GetDecimal(4),
                                Estado = reader.GetString(5)
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
