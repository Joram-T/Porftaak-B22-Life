using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    public abstract class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string Insertion { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public User(int id, string firstname, string insertion, string lastname, string address, string city)
        {
            this.ID = id;
            this.FirstName = firstname;
            this.Insertion = insertion;
            this.LastName = lastname;
            this.Address = address;
            this.City = city;
        }

        public User(string firstname, string insertion, string lastname, string address, string city)
        {
            this.FirstName = firstname;
            this.Insertion = insertion;
            this.LastName = lastname;
            this.Address = address;
            this.City = city;
        }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Insertion + " " + this.LastName + ", " + this.FirstName;
        }
    }
}
