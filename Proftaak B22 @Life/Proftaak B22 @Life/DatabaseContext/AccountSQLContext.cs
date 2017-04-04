using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class AccountSQLContext
    {
        public Account Login(string email, string password)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Account WHERE email = @email AND wachtwoord = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                        }
                        return null;
                    }
                }
            }
        }


        public Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account(Convert.ToInt32(reader["account_id"]),
                                          Convert.ToString(reader["email"]),
                                          Convert.ToString(reader["wachtwoord"]));

            return account;
        }
    }
}
