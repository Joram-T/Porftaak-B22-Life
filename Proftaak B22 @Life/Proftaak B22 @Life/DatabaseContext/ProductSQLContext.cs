using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proftaak_B22__Life.Class;
using System.Data.SqlClient;
using System.Data;

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

        public List<Product> GetAllProducten()
        {
            List<Product> result = new List<Product>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Product";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateProductFromReader(reader));
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



        public void UpdateArtikel(int artikelID, int leverancierid, int productid)
        {
            using (SqlConnection connection = Database.Connection)
            {

                using (SqlCommand command = new SqlCommand("spUpdateArtikel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@artikel_id", artikelID);
                    if (leverancierid != -1)
                    {
                        command.Parameters.AddWithValue("@leverancier_id", leverancierid);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@leverancier_id", null);
                    }
                    if (productid != -1)
                    {
                        command.Parameters.AddWithValue("@product_id", productid);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@product_id", null);
                    }
                    

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception e)
                    {
                        throw e;
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

        public void VerwijderArtikelEnOrderregel(int artikelID)
        {
            using (SqlConnection connection = Database.Connection)
            {
                using (SqlCommand command = new SqlCommand("spVerwijderArtikelEnOrderregel", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@artikel_id", artikelID);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception e)
                    {
                        throw e;
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

        public void InsertArtikel(Artikel artikel)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "INSERT INTO Artikel VALUES(@product_id, @leverancier_id)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@product_id", artikel.Productid);
                    command.Parameters.AddWithValue("@leverancier_id", artikel.Leverancierid);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (System.Exception e)
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
                Artikel artikel = new Proftaak_B22__Life.Artikel(Convert.ToInt32(reader["artikel_id"]),
                                                          Convert.ToInt32(reader["product_id"]),
                                                          Convert.ToInt32(reader["leverancier_id"]));
                return artikel;
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}
