using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    class Leverancier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        public Leverancier(int id, string name, string address, string city)
        {
            id = this.ID;
            name = this.Name;
            address = this.Address;
            city = this.City;
        }

        public Leverancier(string name, string address, string city)
        {
            name = this.Name;
            address = this.Address;
            city = this.City;
        }

        public override string ToString()
        {
            return this.ID.ToString() + " " + this.Name;
        }


    }
}
