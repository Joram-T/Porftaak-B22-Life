using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class ProductSQLContext
    {
        public Product GetArtikelByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Artikel JOIN Product ON artikel.product_id = product.product_id WHERE artikel_id = @artikel_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@artikel_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateProductFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public Product CreateProductFromReader(SqlDataReader reader)
        {
            Product product = new Product(Convert.ToInt32(reader["artikel_id"]),
                                          Convert.ToString(reader["naam"]),
                                          Convert.ToString(reader["omschrijving"]),
                                          Convert.ToDouble(reader["prijs"]));
            return product;
        }
    }
}
