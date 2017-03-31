using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    class Medewerker: User
    {
        //AutoProperty generates private field for us
        public Medewerker(int id, Account account, string name, string address, string city): base(id, account, name, address, city)
        {
        }
    }
}
