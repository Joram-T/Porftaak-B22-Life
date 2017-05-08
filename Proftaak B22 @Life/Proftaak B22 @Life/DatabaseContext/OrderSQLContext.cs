using Proftaak_B22__Life.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak_B22__Life.DatabaseContext
{
    class OrderSQLContext
    {
        public List<Order> GetAllOrdersForMedewerker(Medewerker medewerker)
        {
            List<Order> result = new List<Order>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Order] where medewerker_id = @medewerker_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@medewerker_id", medewerker.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(CreateOrderFromReader(reader));
                        }
                    }
                }
            }
            return result;
        }

        public List<Product> GetArtikelenForOrder(Order order)
        {
            ProductSQLContext productContext = new ProductSQLContext();
            List<Product> artikelen = new List<Product>();
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Orderregel] JOIN Artikel on [Orderregel].artikel_id = Artikel.artikel_id JOIN Product on artikel.product_id = product.product_id where order_id = @order_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@order_id", order.OrderID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            artikelen.Add(productContext.CreateProductFromReader(reader));
                        }
                    }
                }
            }
            return artikelen;
        }

        public Order GetOrderByID(int id)
        {
            using (SqlConnection connection = Database.Connection)
            {
                string query = "SELECT * FROM [Order] WHERE order_id = @order_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@order_id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return CreateOrderFromReader(reader);
                        }
                        return null;
                    }
                }
            }
        }

        private Order CreateOrderFromReader(SqlDataReader reader)
        {
            KlantSQLContext klantContext = new KlantSQLContext();
            MedewerkerSQLContext medewerkerContext = new MedewerkerSQLContext();
            Order order = new Order(Convert.ToInt32(reader["order_id"]),
                                    null,
                                    klantContext.GetKlantByID(Convert.ToInt32(reader["klant_id"])),
                                    medewerkerContext.GetMedewerkerByID(Convert.ToInt32(reader["medewerker_id"])),
                                    Convert.ToDateTime(reader["besteldatum"]),
                                    Convert.ToDateTime(reader["leverdatum"]),
                                    DateTime.Now);
            if (!reader.IsDBNull(5))
            {
                order.PaymentDate = Convert.ToDateTime(reader["betaaldatum"]);   
            }
            order.Articles = GetArtikelenForOrder(order);
            return order;
        }
    }
}
