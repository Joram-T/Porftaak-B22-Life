using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    public class Leverancier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public Leverancier(int id, string name, string address, string city)
        {
            this.ID = id;
            this.Name = name;
            this.Address = address;
            this.City = city;
        }

        public Leverancier(string name, string address, string city)
        {
            this.Name = name;
            this.Address = address;
            this.City = city;
        }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Name;
        }


    }
}
