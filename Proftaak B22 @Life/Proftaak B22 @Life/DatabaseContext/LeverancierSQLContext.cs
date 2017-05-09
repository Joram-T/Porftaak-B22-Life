using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class LeverancierSQLContext
    {
        public List<Leverancier> GetAllLeveranciers()
        {
            List<Leverancier> result = new List<Leverancier>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Leverancier";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateLeverancierFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }


        public Leverancier GetLeverancierByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Leverancier WHERE Leverancier_id = @Leverancier_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Leverancier_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateLeverancierFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public void InsertLeverancier(Leverancier Leverancier)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT MAX(account_id) FROM Account";
                    query = "INSERT INTO Leverancier VALUES(@naam, @adres, @woonplaats)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@naam", Leverancier.Name);
                    command.Parameters.AddWithValue("@adres", Leverancier.Address);
                    command.Parameters.AddWithValue("@woonplaats", Leverancier.City);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
            }
        }


        private Leverancier CreateLeverancierFromReader(SqlDataReader reader)
        {
            Leverancier leverancier = new Proftaak_B22__Life.Leverancier(Convert.ToInt32(reader["leverancier_id"]),
                                                                      Convert.ToString(reader["naam"]),
                                                                      Convert.ToString(reader["adres"]),
                                                                      Convert.ToString(reader["woonplaats"]));
            return leverancier;

        }
    }
}
