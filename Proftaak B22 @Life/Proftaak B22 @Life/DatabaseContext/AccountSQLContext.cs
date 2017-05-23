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
        public Account Login(Account account)
        {
                using (SqlConnection connection = Database.Connection)
                {
                    string query = "SELECT * FROM Account WHERE UPPER(email) = @email AND wachtwoord = @password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", account.Email.ToUpper());
                        command.Parameters.AddWithValue("@password", account.Wachtwoord);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return CreateAccountFromReader(reader);
                            }
                            return null;
                        }
                    }
                }


        }

        public Account GetAccountByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM Account WHERE account_id = @account_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@account_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateAccountFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public void InsertAccount(Account account)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT MAX(account_id) FROM Account";
                query = "INSERT INTO Account VALUES(@email, @wachtwoord)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", account.Email);
                    command.Parameters.AddWithValue("@wachtwoord", account.Wachtwoord);
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


        public Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account(Convert.ToInt32(reader["account_id"]),
                                          Convert.ToString(reader["email"]),
                                          Convert.ToString(reader["wachtwoord"]));

            return account;
        }
    }
}
