using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Enums;

namespace Data.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public decimal Total { get; private set; }
        public DateTime PurchaseDate { get; private set; } = DateTime.Now;
        public Provider Provider { get; private set; }
        public List<Product> PurchasedProducts { get; private set; }
        public PurchaseOrderStatus Status { get; set; }
        public bool Processed { get; set; }

        private static int consecutiveNumber = 1;

        public PurchaseOrder(Provider provider, List<Product> purchasedProducts, DateTime? purchaseDate = null)
        {
            if (provider == null)
                throw new ArgumentNullException("El proveedor no puede ser vacio");

            if (purchasedProducts == null || !purchasedProducts.Any())
                throw new ArgumentNullException("Hay que agregar productos a la orden");

            Status = PurchaseOrderStatus.Pending;
            Id = consecutiveNumber++;
            Total = purchasedProducts.Sum(c => c.Price * c.Stock);
            Provider = provider;
            PurchasedProducts = purchasedProducts;
            PurchaseDate = purchaseDate ?? DateTime.Now;
        }

        // OVERRIDE A TOSTRING
        public override string ToString()
        {
            var report = new StringBuilder();
            report.AppendLine($"Provider: {Provider.Name}");
            report.AppendLine($"Purchase date: {PurchaseDate}");
            report.AppendLine($"Total: {Total:C}");
            report.AppendLine($"Estatus: {Status.ToString().ToUpper()}");
            report.AppendLine($"Products:");
            foreach(var product in PurchasedProducts)
            {
                report.AppendLine($"Name: {product.Name} Stock: {product.Stock:N0}");
            }

            return report.ToString();
        }

        public void ChangeStatus(PurchaseOrderStatus status)
        {
            Status = status;

            if (Status == PurchaseOrderStatus.Paid)
                Processed = true;
        }
    }
}
