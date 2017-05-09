using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life
{
    class Artikel
    {
        public int Artikelid { get; set; }
        public int Productid { get; set; }
        public int Leverancierid { get; set; }

        public Artikel(int artikelid, int productid, int leverancierid)
        {
            artikelid = this.Artikelid;
            productid = this.Productid;
            leverancierid = this.Leverancierid;
        }

        public Artikel(int productid, int leverancierid)
        {
            productid = this.Productid;
            leverancierid = this.Leverancierid;
        }

        public override string ToString()
        {
            return this.Artikelid.ToString() + " " + this.Productid.ToString() + " " + this.Leverancierid.ToString();
        }
    }
}
