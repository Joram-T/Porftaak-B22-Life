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
        public List<Klant> GetAllKlanten()
        {
            List<Klant> result = new List<Klant>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Klant";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateKlantFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        private Klant CreateKlantFromReader(SqlDataReader reader)
        {
            Klant klant = new Klant(Convert.ToInt32(reader["klant_id"]),
                                    Convert.ToString(reader["voornaam"]),
                                    "",
                                    Convert.ToString(reader["achternaam"]),
                                    Convert.ToString(reader["adres"]),
                                    Convert.ToString(reader["woonplaats"]),
                                    Convert.ToString(reader["postcode"]));
            if (!string.IsNullOrWhiteSpace(klant.Insertion))
            {
                klant.Insertion = Convert.ToString(reader["tussenvoegsel"]);
            }
            return klant;
        }
    }
}
