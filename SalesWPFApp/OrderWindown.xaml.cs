using SalesWPFAppLibrary.Dao;
using SalesWPFAppLibrary.DataAccess;
using SalesWPFAppLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderWindown.xaml
    /// </summary>
    public partial class OrderWindown : Window
    {
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public OrderWindown()
        {
            InitializeComponent();
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
            LoadList();
            LoadStatus();
        }

        public void LoadList()
        {
            IEnumerable<Order> orders = orderRepository.GetOrders();
            int count = 0;
            foreach (Order o in orders)
            {
                count++;
            }
            Ghi.Text = $"Record: {count}";
            DataGrid.ItemsSource = orders;
        }

        public void LoadStatus()
        {
            List<string> statusItems = new List<string> { "Processing", "Shipped","Done"};
            cbStatus.ItemsSource = statusItems;
            cbStatus.SelectedIndex = 0;
        }

        private Order GetObject()
        {
            Order order = null;
            try
            {
                order = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = dpOrderDate.SelectedDate,
                    ShippedDate = DateTime.Now,
                    TotalPrice = decimal.Parse(txtTotalPrice.Text),
                    Status = cbStatus.SelectedItem.ToString()
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order");
            }
            return order;
        }


        private void btDetail_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Order clickedOrder = button?.DataContext as Order;
            if (clickedOrder != null)
            {
                int orderId = clickedOrder.OrderId;
                OrderDetailWindown orderDetailWindown = new OrderDetailWindown(orderId);
                orderDetailWindown.Show();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order od = GetObject();
                orderRepository.UpdateOrder(od);
                LoadList();
                MessageBox.Show($"{od.OrderId} updated successfully ", "Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order od = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {od.OrderId}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailByID(od.OrderId);
                    foreach (OrderDetail detail in orderDetails)
                    {
                        orderDetailRepository.DeleteOrderDetail(detail);
                    }
                    orderRepository.DeleteOrder(od);
                    LoadList();
                    MessageBox.Show($"{od.OrderId} deleted successfully", "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtOrderId.Text = "";
            txtMemberId.Text = "";
            dpOrderDate.SelectedDate = null;
            dpShippedDate.SelectedDate = null;
            txtTotalPrice.Text = "";
            cbStatus.SelectedIndex = 0;
        }

        public void SearchbyStatus(string status)
        {
            try
            {
                IEnumerable<Order> orders = orderRepository.GetOrdersByStatus(status);
                int count = 0;
                foreach (Order o in orders)
                {
                    count++;
                }
                Ghi.Text = $"Record: {count}";
                DataGrid.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Process");
            }
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            SearchbyStatus("Processing");
        }

        private void Ship_Click(object sender, RoutedEventArgs e)
        {
            SearchbyStatus("Shipped");
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            SearchbyStatus("Done");
        }

        private void MemberPage_Click(object sender, RoutedEventArgs e)
        {
            MemberWindown memberWindown = new MemberWindown();
            memberWindown.Show();
            this.Close();
        }

        private void ProductPage_Click(object sender, RoutedEventArgs e)
        {
            ProductWindown productWindown = new ProductWindown();
            productWindown.Show();
            this.Close();
        }

        private void OrderPage_Click(object sender, RoutedEventArgs e)
        {
            OrderWindown orderWindown = new OrderWindown();
            orderWindown.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindown loginWindown = new LoginWindown();
            loginWindown.Show();
            this.Close();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindown profileWindown = new ProfileWindown();
            profileWindown.Show();
        }
    }
}
