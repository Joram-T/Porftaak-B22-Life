﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    class Database
    {
        /// <summary>
        /// In this class the connection between C# and MSSQL is created
        /// </summary>
        private static readonly string connectionString = "Server=mssql.fhict.local;Database=dbi319198;User Id=dbi319198;Password=apenkop35;";
        public static SqlConnection Connection
        {
            get
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }
    }
}
