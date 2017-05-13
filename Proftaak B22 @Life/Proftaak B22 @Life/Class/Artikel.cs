using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Proftaak_B22__Life.Class;
using Proftaak_B22__Life.DatabaseContext;

namespace Proftaak_B22__Life
{
    class Artikel
    {
        public int Artikelid { get; set; }
        public int Productid { get; set; }
        public int Leverancierid { get; set; }

        public Artikel(int artikelid, int productid, int leverancierid)
        {
            this.Artikelid = artikelid;
            this.Productid = productid;
            this.Leverancierid = leverancierid;

        }

        public Artikel(int productid, int leverancierid)
        {
            this.Productid = productid;
            this.Leverancierid = leverancierid;
        }

        public override string ToString()
        {
            return this.Artikelid.ToString() + " " + Leverancierid.ToString() + " " +
                   Productid.ToString();
        }
    }
}
