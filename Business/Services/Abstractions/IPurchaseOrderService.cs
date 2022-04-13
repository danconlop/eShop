using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Services.Abstractions
{
    public interface IPurchaseOrderService
    {
        public void AddPurchaseOrder(PurchaseOrder purchaseOrder);
        public List<PurchaseOrder> GetPurchaseOrders();
    }


}
