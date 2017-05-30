using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.Class
{
    class ListViewObjectMaandOrder
    {
        public int Maand;
        public int AantalOrders;

        public ListViewObjectMaandOrder(int maand, int aantalorders)
        {
            this.Maand = maand;
            this.AantalOrders = aantalorders;
        }
    }
}
