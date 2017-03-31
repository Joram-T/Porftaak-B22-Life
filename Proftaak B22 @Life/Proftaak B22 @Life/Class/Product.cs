using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    class Product
    {
        //AutoProperty generates private field for us
        public int product_id { get; }
        public string name { get; set; }
        public string description { get; set; }       
        public decimal Price { get; set; }
    }
}
