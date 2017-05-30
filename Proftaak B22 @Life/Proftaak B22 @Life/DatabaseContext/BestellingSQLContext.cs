using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class BestellingSQLContext
    {
        DateTime defaultDate = new DateTime(2000,01,01);

        public List<Bestelling> GetOpenBestellingen()
        {
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Order] where betaaldatum is null order by besteldatum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateBestellingFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public List<Bestelling> GetGeslotenBestellingen()
        {
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Order] where betaaldatum is not null order by besteldatum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateBestellingFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public Bestelling GetBestellingByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Order] WHERE order_id = @order_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@order_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateBestellingFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public List<Artikel> GetArtikelenForBestelling(Bestelling bestelling)
        {
            ProductSQLContext productContext = new ProductSQLContext();
            List<Artikel> artikelen = new List<Artikel>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Orderregel] JOIN Artikel on [Orderregel].artikel_id = Artikel.artikel_id JOIN Product on artikel.product_id = product.product_id where order_id = @order_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@order_id", bestelling.Id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artikelen.Add(productContext.CreateArtikelFromReader(reader));
                        }
                    }
                }
            }
            return artikelen;
        }


        public Bestelling CreateBestellingFromReader(SqlDataReader reader)
        {
            Bestelling bestelling = new Bestelling(1, 1, 1, DateTime.Now);

            if (reader["leverdatum"].ToString()!="" && reader["betaaldatum"].ToString()!="")
            {
                bestelling = new Bestelling(Convert.ToInt32(reader["order_id"]),
                                                          Convert.ToInt32(reader["klant_id"]),
                                                          Convert.ToInt32(reader["medewerker_id"]),
                                                          Convert.ToDateTime(reader["besteldatum"]),
                                                          Convert.ToDateTime(reader["leverdatum"]),
                                                          Convert.ToDateTime(reader["betaaldatum"]));
            }

            else if (reader["leverdatum"].ToString() != "" && reader["betaaldatum"].ToString() == "")
            {
                bestelling = new Bestelling(Convert.ToInt32(reader["order_id"]),
                                                          Convert.ToInt32(reader["klant_id"]),
                                                          Convert.ToInt32(reader["medewerker_id"]),
                                                          Convert.ToDateTime(reader["besteldatum"]),
                                                          Convert.ToDateTime(reader["leverdatum"]));
            }
            else if(reader["leverdatum"].ToString() == "" && reader["betaaldatum"].ToString() == "")
            {
                bestelling = new Bestelling(Convert.ToInt32(reader["order_id"]),
                                                          Convert.ToInt32(reader["klant_id"]),
                                                          Convert.ToInt32(reader["medewerker_id"]),
                                                          Convert.ToDateTime(reader["besteldatum"]));
            }
            return bestelling;
        }

        public void SluitBestelling(int id, DateTime leverdatum, DateTime betaaldatum)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "Update \"Order\" SET leverdatum = @leverdatum, betaaldatum = @betaaldatum where order_id=@id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@leverdatum", leverdatum);
                    command.Parameters.AddWithValue("@betaaldatum", betaaldatum);
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

        public void UpdateBestelling(int id, Klant klant, Medewerker medewerker, DateTime? besteldatum, DateTime? leverdatum, DateTime? betaaldatum)
        {
            using (SqlConnection connection = Database.Connection)
            {
                
                using (SqlCommand command = new SqlCommand("spUpdateBestelling", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    if (klant != null)
                    {
                        command.Parameters.AddWithValue("@klant_id", klant.ID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@klant_id", DBNull.Value);
                    }
                    if (medewerker != null)
                    {
                        command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@medewerker_id", DBNull.Value);
                    }
                    command.Parameters.AddWithValue("@besteldatum", besteldatum);
                    command.Parameters.AddWithValue("@leverdatum", leverdatum);
                    command.Parameters.AddWithValue("@betaaldatum", betaaldatum);
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

        public int GetMaxID()
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT MAX(order_id) FROM \"Order\"";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                        return 0;
                    }
                }
            }

        }
        public List<ListViewObjectMaandOrder> GetAantalOrdersPerMaandPerJaar(int jaar)
        {
            List<ListViewObjectMaandOrder> lijst = new List<ListViewObjectMaandOrder>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "select datepart(mm, besteldatum) AS Maand, count(order_id) as Aantal FROM[order] WHERE datepart(yyyy, besteldatum) = @jaar group by datepart(mm, besteldatum)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@jaar", jaar);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lijst.Add(new ListViewObjectMaandOrder(Convert.ToInt32(reader["Maand"]), Convert.ToInt32(reader["Aantal"])));
                        }
                        return lijst;
                    }
                }
            }

        }

        public List<int> GetJaartallenOrders()
        {
            List<int> lijst = new List<int>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "select distinct Datepart(yyyy, besteldatum) as Jaartal from [order]";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lijst.Add(Convert.ToInt32(reader["Jaartal"]));
                        }
                        return lijst;
                    }
                }
            }
        }

        public int GetAantalOrdersHuidigeMaand()
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "select count(order_id) FROM[order] WHERE DATEPART(mm, besteldatum) = @maand ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maand", DateTime.Now.Date.Month);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                        return 0;
                    }
                }
            }

        }

        public void InsertBestelling(int klant_id, int medewerker_id, DateTime besteldatum, DateTime? leverdatum = null, DateTime? betaaldatum = null)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "Insert INTO \"Order\" VALUES(@id, @klant_id, @medewerker_id, @besteldatum, @leverdatum, @betaaldatum, @totaalprijs)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", GetMaxID()+1);
                    command.Parameters.AddWithValue("@klant_id", klant_id);
                    command.Parameters.AddWithValue("@medewerker_id", medewerker_id);
                    command.Parameters.AddWithValue("@besteldatum", besteldatum);
                    if (leverdatum == null)
                    {
                        command.Parameters.AddWithValue("@leverdatum", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@leverdatum", leverdatum);
                    }
                    if (betaaldatum == null)
                    {
                        command.Parameters.AddWithValue("@betaaldatum", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@betaaldatum", betaaldatum);
                    }
                    command.Parameters.AddWithValue("@totaalprijs", 0);
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
    }
}
