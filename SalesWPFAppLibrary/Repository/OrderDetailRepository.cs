using SalesWPFAppLibrary.Dao;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public List<OrderDetail> GetOrderDetailByID(int orderId) => OrderDetailDAO.Instance.GetOrderDetailByID(orderId);
        public IEnumerable<OrderDetail> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailsList();
        public void InsertOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.AddNew(orderDetail);
        public void DeleteOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.Remove(orderDetail);
    }
}
