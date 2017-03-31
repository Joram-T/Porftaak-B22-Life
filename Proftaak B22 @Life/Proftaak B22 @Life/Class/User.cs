using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    abstract class User
    {
        public int ID { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public User(int id, Account account, string name, string address, string city)
        {
            this.ID = id;
            this.Account = account;
            this.Name = name;
            this.Address = address;
            this.City = city;
        }
    }
}
