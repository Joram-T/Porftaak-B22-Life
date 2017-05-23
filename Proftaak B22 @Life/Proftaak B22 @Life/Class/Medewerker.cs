using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    public class Medewerker: User
    {
        byte[] Profielfoto { get; set; }
        Account Account { get; set; }
        //AutoProperty generates private field for us
        public Medewerker(int id, Account account, string firstname, string insertion, string lastname, string address, string city): base(id, firstname, insertion, lastname, address, city)
        {
            this.Account = account;
        }

        public Medewerker(Account account, string firstname, string insertion, string lastname, string address, string city) : base(firstname, insertion, lastname, address, city)
        {
            this.Account = account;
        }
    }
}
