using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails();
        List<OrderDetail> GetOrderDetailByID(int orderId);
        void InsertOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(OrderDetail orderDetail);
    }
}
