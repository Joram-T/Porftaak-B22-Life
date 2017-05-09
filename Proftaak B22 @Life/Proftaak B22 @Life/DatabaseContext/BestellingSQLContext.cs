using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class BestellingSQLContext
    {
        public List<Bestelling> GetOpenBestellingen()
        {
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM \"Order\" where betaaldatum is null order by besteldatum";
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
                string query = "SELECT * FROM \"Order\" where betaaldatum is not null order by besteldatum";
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
                string query = "SELECT * FROM \"Order\" WHERE order_id = @order_id";
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
        

        private Bestelling CreateBestellingFromReader(SqlDataReader reader)
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
    }
}
