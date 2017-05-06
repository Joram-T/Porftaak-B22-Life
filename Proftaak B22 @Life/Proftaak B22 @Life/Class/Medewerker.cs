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
        public Medewerker(int id, Account account, string firstname, string insertion, string lastname, string address, string city): base(id, account, firstname, insertion, lastname, address, city)
        {
        }

        public Medewerker(Account account, string firstname, string insertion, string lastname, string address, string city) : base(account, firstname, insertion, lastname, address, city)
        {
        }
    }
}
