using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Business.Services.Abstractions;

namespace Business.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private List<Order> _orderList = new List<Order>();

        public void AddOrder(Order order) => _orderList.Add(order);

        public List<Order> GetOrders() => _orderList;
    }
}
