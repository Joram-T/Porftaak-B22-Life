using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    class Order
    {
        public int OrderID { get; set; }
        public List<Product> Articles { get; set; }
        public Klant Customer { get; set; }
        public Medewerker Employee { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime PaymentDate { get; set; }

        public Order(int orderid, List<Product> artikelen, Klant klant, Medewerker medewerker, DateTime besteldatum, DateTime leverdatum, 
                     DateTime betaaldatum)
        {
            this.OrderID = orderid;
            this.Customer = klant;
            this.Employee = medewerker;
            this.OrderDate = besteldatum;
            this.DeliveryDate = leverdatum;
            this.PaymentDate = betaaldatum;
        }

        public double GetTotalPrice()
        {
            double totalprice = 0;
            if (Articles.Count > 0)
            {
                foreach (Product article in this.Articles)
                {
                    totalprice += Convert.ToDouble(article.Price);
                }
            }
            return totalprice;
        }

        public override string ToString()
        {
            return "Bestelling " + OrderID.ToString() + " - €" + GetTotalPrice().ToString();
        }

    }
}
