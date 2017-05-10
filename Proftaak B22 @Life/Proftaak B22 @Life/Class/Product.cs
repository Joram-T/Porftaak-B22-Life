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
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public Product(string name, string description, decimal price)
        {
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
