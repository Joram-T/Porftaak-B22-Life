using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proftaak_B22__Life.Class;
using System.Data.SqlClient;

namespace Proftaak_B22__Life.DatabaseContext
{
    class ProductSQLContext
    {
        public List<Artikel> GetAllArtikelen()
        {
            List<Artikel> result = new List<Artikel>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Artikel";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateArtikelFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }


        public Artikel GetArtikelByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Artikel WHERE Artikel_id = @artikel_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@artikel_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateArtikelFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public Product GetProductByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Product WHERE product_id = @product_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", id);
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

        private Product CreateProductFromReader(SqlDataReader reader)
        {
            Product product = new Product(Convert.ToInt32(reader["product_id"]),
                                          Convert.ToString(reader["naam"]),
                                          Convert.ToString(reader["omschrijving"]),
                                          Convert.ToDecimal(reader["prijs"]));
            return product;
        }

        public void InsertArtikel(Artikel Artikel)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "INSERT INTO Artikel VALUES(@product_id, @leverancier_id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", Artikel.Productid);
                    command.Parameters.AddWithValue("@leverancier_id", Artikel.Leverancierid);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }
            }
        }


        public Artikel CreateArtikelFromReader(SqlDataReader reader)
        {
            try
            {
                Artikel Artikel = new Proftaak_B22__Life.Artikel(Convert.ToInt32(reader["artikel_id"]),
                                                          Convert.ToInt32(reader["product_id"]),
                                                          Convert.ToInt32(reader["leverancier_id"]));
                return Artikel;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
