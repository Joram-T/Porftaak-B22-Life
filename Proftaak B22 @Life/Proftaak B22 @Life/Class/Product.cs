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
        public int ID { get; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public decimal Price { get; set; }
    }
}
