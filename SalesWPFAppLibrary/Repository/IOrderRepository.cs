using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetOrdersByStatus(string status);
        Order GetOrderByID(int orderId);
        void InsertOrder(Order order);
        void DeleteOrder(Order order);
        void UpdateOrder(Order order);
    }
}
