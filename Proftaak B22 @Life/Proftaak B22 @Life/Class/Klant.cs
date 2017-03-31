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

        public Klant(int id, Account account, string name, string address, string city, string zip): base(id, account, name, address, city)
        {
            this.Zip = zip;
        }

    }
}
