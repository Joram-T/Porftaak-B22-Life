using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    class Klant : User
    {
        //AutoProperty generates private field for us
        public string Zip { get; set; }

        public Klant(int id, string firstname, string insertion, string lastname, string address, string city, string zip): base(id, firstname, insertion, lastname, address, city)
        {
            this.Zip = zip;
        }

        public override string ToString()
        {
            return this.LastName + " " + this.Insertion + ", " + this.FirstName;
        }

    }
}
