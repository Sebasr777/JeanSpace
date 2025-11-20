using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_de_Herramientas
{
    public static class ProductoDAO
    {
        public static List<Producto> ObtenerTodos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"SELECT IdProducto, Nombre, Talla, Color, Precio, Stock, ImagenPath
                               FROM Productos
                               WHERE Estado = 1";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Producto
                        {
                            ID = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Talla = reader.GetString(2),
                            Color = reader.GetString(3),
                            Precio = reader.GetDecimal(4),
                            Stock = reader.GetInt32(5),
                            ImagenPath = reader.IsDBNull(6) ? null : reader.GetString(6)
                        });
                    }
                }
            }

            return lista;
        }

        public static void Actualizar(Producto p)
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = @"UPDATE Productos SET Nombre = @n, Talla = @t, Color = @c, Precio = @p, Stock = @s, ImagenPath = @img WHERE IdProducto = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@n", p.Nombre);
                    cmd.Parameters.AddWithValue("@t", p.Talla);
                    cmd.Parameters.AddWithValue("@c", p.Color);
                    cmd.Parameters.AddWithValue("@p", p.Precio);
                    cmd.Parameters.AddWithValue("@s", p.Stock);
                    cmd.Parameters.AddWithValue("@img", p.ImagenPath ?? "");
                    cmd.Parameters.AddWithValue("@id", p.ID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string sql = "DELETE FROM Productos WHERE IdProducto = @id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Insertar(Producto producto)
        {
            try
            {
                using (SqlConnection conn = BDJeanStore.Conectar())
                {
                    string query = @"INSERT INTO Productos
                             (Nombre, Talla, Color, Precio, Stock, ImagenPath)
                             VALUES
                             (@Nombre, @Talla, @Color, @Precio, @Stock, @ImagenPath)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@Talla", producto.Talla);
                    cmd.Parameters.AddWithValue("@Color", producto.Color);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@ImagenPath", producto.ImagenPath ?? "");

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar producto:\n\n" + ex.Message);
            }
        }

        public static int ObtenerTotalProductos()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COUNT(*) FROM Productos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int ObtenerStockTotal()
        {
            using (SqlConnection conn = BDJeanStore.Conectar())
            {
                string query = "SELECT COALESCE(SUM(Stock), 0) FROM Productos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
