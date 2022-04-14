using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Total { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public List<Product> OrderProducts { get; private set; }
        public Order(int id, List<Product> orderProducts, DateTime? orderDate = null)
        {
            if (orderProducts == null || !orderProducts.Any())
                throw new ArgumentNullException("No hay productos en el carrito");

            Id = id;
            OrderProducts = orderProducts;
            Total = OrderProducts.Sum(product => product.Price * product.Stock);
            OrderDate = orderDate ?? DateTime.Now;
        }
    }
}
