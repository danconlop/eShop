using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services.Abstractions;
using Data.Entities;

namespace Business.Services.Implementations
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private List<PurchaseOrder> _purchaseOrders = new List<PurchaseOrder>();
        public void AddPurchaseOrder(PurchaseOrder purchaseOrder) => _purchaseOrders.Add(purchaseOrder);
        public List<PurchaseOrder> GetPurchaseOrders() => _purchaseOrders;
    }
}
