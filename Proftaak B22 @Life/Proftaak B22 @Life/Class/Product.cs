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
        

        public Product(int id, string name, string description, decimal price)
        {
            id = this.ID;
            name = this.Name;
            description = this.Description;
            price = this.Price;
        }

        public Product(string name, string description, decimal price)
        {
            name = this.Name;
            description = this.Description;
            price = this.Price;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
