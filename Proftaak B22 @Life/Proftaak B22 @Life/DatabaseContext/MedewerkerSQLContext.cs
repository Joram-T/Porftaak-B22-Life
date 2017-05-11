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
        public List<Bestelling> GetBestellingenForMedewerker(Medewerker medewerker)
        {
            BestellingSQLContext bestellingContext = new BestellingSQLContext();
            List<Bestelling> result = new List<Bestelling>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM \"Order\" where medewerker_id = @medewerker_id order by besteldatum";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
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


        public void InsertMedewerker(Medewerker medewerker)
        {
            using (SqlConnection connection = Database.Connection)
            {
                // eerst het account id krijgen van het laatst geinserte account
                int LastAccountID = 0;
                string query = "SELECT MAX(account_id) FROM Account";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LastAccountID = Convert.ToInt32(reader[0]);
                            }
                        }
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }
                }
                    //daarna een medewerker inserten met het verkregen accountid
                    query = "INSERT INTO Medewerker VALUES(@managerid, @accountid, @voornaam, @achternaam, @tussenvoegsel, @adres, @woonplaats)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@managerid", DBNull.Value);
                    command.Parameters.AddWithValue("@accountid", LastAccountID);
                    command.Parameters.AddWithValue("@voornaam", medewerker.FirstName);
                    command.Parameters.AddWithValue("@achternaam", medewerker.LastName);
                    command.Parameters.AddWithValue("@tussenvoegsel", medewerker.Insertion);
                    command.Parameters.AddWithValue("@adres", medewerker.Address);
                    command.Parameters.AddWithValue("@woonplaats", medewerker.City);
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


        private Medewerker CreateMedewerkerFromReader(SqlDataReader reader)
        {
            AccountSQLContext accountContext = new AccountSQLContext();
            Medewerker medewerker = new Proftaak_B22__Life.Medewerker(Convert.ToInt32(reader["medewerker_id"]),
                                        accountContext.GetAccountByID(Convert.ToInt32(reader["account_id"])),
                                                                      Convert.ToString(reader["voornaam"]),
                                                                      Convert.ToString(reader["tussenvoegsel"]),
                                                                      Convert.ToString(reader["achternaam"]),
                                                                      Convert.ToString(reader["adres"]),
                                                                      Convert.ToString(reader["woonplaats"]));
            return medewerker;

        }
    }
}
