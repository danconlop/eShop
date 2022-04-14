using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Cart
    {
        public decimal Total { get; private set; }
        public DateTime PurchaseDate { get; private set; } = DateTime.Now;
        public List<Product> PurchasedProducts { get; private set; }
        public bool Status { get; set; }

        public Cart(List<Product> purchasedProducts, DateTime? purchaseDate = null)
        {
            if (purchasedProducts == null || !purchasedProducts.Any())
                throw new ArgumentNullException("Hay que agregar productos al carrito");

            PurchasedProducts = purchasedProducts;
            Total = PurchasedProducts.Sum(product => product.Price * product.Stock);

            PurchaseDate = purchaseDate ?? DateTime.Now;
        }

    }
}
