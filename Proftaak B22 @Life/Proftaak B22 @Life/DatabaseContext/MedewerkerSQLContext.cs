using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class MedewerkerSQLContext
    {
        public List<Medewerker> GetAllMedewerkers()
        {
            List<Medewerker> result = new List<Medewerker>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Medewerker";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateMedewerkerFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }


        public Medewerker GetMedewerkerByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Medewerker WHERE medewerker_id = @medewerker_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@medewerker_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateMedewerkerFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }


        private Medewerker CreateMedewerkerFromReader(SqlDataReader reader)
        {
            AccountSQLContext accountContext = new AccountSQLContext();
            Medewerker medewerker = new Proftaak_B22__Life.Medewerker(Convert.ToInt32(reader["medewerker_id"]),
                                        accountContext.GetAccountByID(Convert.ToInt32(reader["account_id"])),
                                                                      Convert.ToString(reader["tussenvoegsel"]) + " " + Convert.ToString(reader["achternaam"]) + ", " + Convert.ToString(reader["voornaam"]),
                                                                      Convert.ToString(reader["adres"]),
                                                                      Convert.ToString(reader["woonplaats"]));
            return medewerker;

        }
    }
}
