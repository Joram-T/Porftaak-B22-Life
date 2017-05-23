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


        public void InsertKlant(Klant klant)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "INSERT INTO Klant VALUES(@voornaam, @achternaam, @tussenvoegsel, @adres, @woonplaats, @postcode)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@voornaam", klant.FirstName);
                    command.Parameters.AddWithValue("@achternaam", klant.LastName);
                    command.Parameters.AddWithValue("@tussenvoegsel", klant.Insertion);
                    command.Parameters.AddWithValue("@adres", klant.Address);
                    command.Parameters.AddWithValue("@woonplaats", klant.City);
                    command.Parameters.AddWithValue("@postcode", klant.Zip);
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

        public List<Bestelling> GetOpenBestellingenForKlant(Klant klant)
        {
            BestellingSQLContext bestellingContext = new BestellingSQLContext();
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM \"Order\" where klant_id = @klant_id AND betaaldatum is null order by besteldatum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@klant_id", klant.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(bestellingContext.CreateBestellingFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }


        public List<Bestelling> GetGeslotenBestellingenForKlant(Klant klant)
        {
            BestellingSQLContext bestellingContext = new BestellingSQLContext();
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM \"Order\" where klant_id = @klant_id AND betaaldatum is not null order by besteldatum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@klant_id", klant.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(bestellingContext.CreateBestellingFromReader(reader));
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
                                    "");
            if (!string.IsNullOrWhiteSpace(klant.Insertion))
            {
                klant.Insertion = Convert.ToString(reader["tussenvoegsel"]);
            }
            if (!string.IsNullOrWhiteSpace(klant.Zip))
            {
                klant.Insertion = Convert.ToString(reader["postcode"]);
            }
            return klant;
        }
    }
}
