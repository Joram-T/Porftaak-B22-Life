using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class KlantSQLContext
    {
        public Klant GetKlantByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Klant WHERE klant_id = @klant_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@klant_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateKlantFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        private Klant CreateKlantFromReader(SqlDataReader reader)
        {
            Klant klant = new Klant(Convert.ToInt32(reader["klant_id"]),
                                    Convert.ToString(reader["voornaam"]),
                                    Convert.ToString(reader["tussenvoegsel"]),
                                    Convert.ToString(reader["achternaam"]),
                                    Convert.ToString(reader["adres"]),
                                    Convert.ToString(reader["woonplaats"]),
                                    Convert.ToString(reader["postcode"]));

            return klant;
        }
    }
}
