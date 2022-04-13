using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PurchaseOrder
    {
        public decimal Total { get; private set; }
        public DateTime PurchaseDate { get; private set; } = DateTime.Now;
        public Provider Provider { get; private set; }
        public List<Product> PurchasedProducts { get; private set; }

        public PurchaseOrder(Provider provider, List<Product> purchasedProducts, DateTime? purchaseDate = null)
        {
            if (provider == null)
                throw new ArgumentNullException("El proveedor no puede ser vacio");

            if (purchasedProducts == null || !purchasedProducts.Any())
                throw new ArgumentNullException("Hay que agregar productos a la orden");

            Total = purchasedProducts.Sum(c => c.Price * c.Stock);
            Provider = provider;
            PurchasedProducts = purchasedProducts;
            PurchaseDate = purchaseDate ?? DateTime.Now;
        }

        public override string ToString()
        {
            var report = new StringBuilder();
            report.AppendLine($"Provider: {Provider.Name}");
            report.AppendLine($"Purchase date: {PurchaseDate}");
            report.AppendLine($"Total: {Total}");
            report.AppendLine($"Products:");
            foreach(var product in PurchasedProducts)
            {
                report.AppendLine($"Name: {product.Name} Stock: {product.Stock}");
            }

            //    Console.WriteLine($"Provider: {orden.Provider.Name}\nPurchase date: {orden.PurchaseDate}\nTotal: {orden.Total}\nProducts:");
            //foreach (var product in orden.PurchasedProducts)
            //{
            //    Console.WriteLine($"Name: {product.Name} Stock: {product.Stock}");
            //}
            return report.ToString();
        }
    }
}
