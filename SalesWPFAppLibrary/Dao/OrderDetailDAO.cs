using Microsoft.EntityFrameworkCore;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWPFAppLibrary.Dao
{
    public class OrderDetailDAO
    {
        //-------------------------------------
        // Using Singleton Pattern
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        //-------------------------------------
        public IEnumerable<OrderDetail> GetOrderDetailsList()
        {
            List<OrderDetail> orderDetails;
            try
            {
                using (var salesWpfAppDB = new SalesWpfappContext())
                {
                    orderDetails = salesWpfAppDB.OrderDetails.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }

        //-------------------------------------
        public List<OrderDetail> GetOrderDetailByID(int orderId)
        {
            List<OrderDetail> orderDetail = null;
            try
            {
                using (var salesWpfAppDB = new SalesWpfappContext())
                {
                    orderDetail = salesWpfAppDB.OrderDetails.Where(
                        od => od.OrderId == orderId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }

        //-------------------------------------
        public void AddNew(OrderDetail orderDetail)
        {
            try
            {
                using (var salesWpfAppDB = new SalesWpfappContext())
                {
                    salesWpfAppDB.OrderDetails.Add(orderDetail);
                    salesWpfAppDB.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Remove(OrderDetail orderDetail)
        {
            try
            {
                using (var salesWpfAppDB = new SalesWpfappContext())
                {
                    int orderId = (int)orderDetail.OrderId;
                    List <OrderDetail> existingOrderDetail = GetOrderDetailByID(orderId);

                    if (existingOrderDetail.Count > 0)
                    {     
                        salesWpfAppDB.OrderDetails.Remove(orderDetail);
                        salesWpfAppDB.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("The orderDetail does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
