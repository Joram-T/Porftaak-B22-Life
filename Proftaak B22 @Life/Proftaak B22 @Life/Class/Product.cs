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
        public double Price { get; set; }

        public Product(int id, string name, string description, double price)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }

        public override string ToString()
        {
            return ID.ToString() + " - " + Name + " - €" + Price.ToString();
        }
    }
}
