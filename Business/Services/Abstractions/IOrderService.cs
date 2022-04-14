using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Business.Services.Abstractions
{
    public interface IOrderService
    {
        public void AddOrder(Order order);
        public List<Order> GetOrders();
    }
}
