using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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


        public void InsertMedewerkerMetAccount(Medewerker medewerker)
        {
            using (SqlConnection connection = Database.Connection)
            {
                    
                using (SqlCommand command = new SqlCommand("spInsertMedewerkerMetAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", medewerker.Account.Email);
                    command.Parameters.AddWithValue("@wachtwoord", medewerker.Account.Wachtwoord);
                    command.Parameters.AddWithValue("@voornaam", medewerker.FirstName);
                    command.Parameters.AddWithValue("@achternaam", medewerker.LastName);
                    command.Parameters.AddWithValue("@tussenvoegsel", medewerker.Insertion);
                    command.Parameters.AddWithValue("@adres", medewerker.Address);
                    command.Parameters.AddWithValue("@woonplaats", medewerker.City);
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

        public BitmapImage GetProfielfotoForMedewerker(Medewerker medewerker)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT profielfoto FROM [Medewerker] WHERE medewerker_id = @medewerker_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return CreateProfielfotoFromReader(reader);
                            }
                        }
                    }
                }
            }
            return null;
        }

        public void UpdateProfielfoto(Medewerker medewerker, byte[] profielfoto)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "UPDATE [Medewerker] SET profielfoto = @profielfoto WHERE medewerker_id = @medewerker_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
                    command.Parameters.AddWithValue("@profielfoto", profielfoto);

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

        public void UpdateMedewerker(Medewerker medewerker, string voornaam, string achternaam, string tussenvoegsel, string woonplaats, string adres)
        {
            using (SqlConnection connection = Database.Connection)
            {
               

                using (SqlCommand command = new SqlCommand("spUpdateMedewerker", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
                    command.Parameters.AddWithValue("@voornaam", voornaam);
                    command.Parameters.AddWithValue("@achternaam", achternaam);
                    command.Parameters.AddWithValue("@tussenvoegsel", tussenvoegsel);
                    command.Parameters.AddWithValue("@woonplaats", woonplaats);
                    command.Parameters.AddWithValue("@adres", adres);

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

        private BitmapImage CreateProfielfotoFromReader(SqlDataReader reader)
        {
            BitmapImage image = new BitmapImage();
            byte[] profielfoto = new byte[0];
            int colIndex = reader.GetOrdinal("profielfoto");
            if (!reader.IsDBNull(colIndex))
            {
                profielfoto = (byte[])(reader["profielfoto"]);
                using (MemoryStream stream = new MemoryStream(profielfoto))
                {
                    stream.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = stream;
                    image.EndInit();
                    image.Freeze();
                    return image;
                }
            }
            return null;
        }
    }
}
