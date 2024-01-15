using Microsoft.EntityFrameworkCore;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Dao
{
    public class OrderDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        //-------------------------------------
        public IEnumerable<Order> GetOrdersList()
        {
            List<Order> orders;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                orders = salesWpfAppDB.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        //-------------------------------------
        public Order GetOrderByID(int orderID)
        {
            Order order = null;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                order = salesWpfAppDB.Orders.SingleOrDefault(order => order.OrderId == orderID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        //-------------------------------------
        public IEnumerable<Order> GetOrdersByStatus(string status)
        {
            List<Order> orders;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                orders = salesWpfAppDB.Orders.Where(x => x.Status.Equals(status)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }

        //-------------------------------------
        public void AddNew(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order == null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Orders.Add(order);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The order is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Update(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Entry<Order>(order).State = EntityState.Modified;
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Remove(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Orders.Remove(order);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The order does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
