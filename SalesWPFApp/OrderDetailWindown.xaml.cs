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
    /// Interaction logic for OrderDetailWindown.xaml
    /// </summary>
    public partial class OrderDetailWindown : Window
    {   
        int orderId;
        IOrderDetailRepository orderDetailRepository;
        public OrderDetailWindown(int id)
        {
            InitializeComponent();
            orderDetailRepository = new OrderDetailRepository();
            this.orderId = id;
            LoadList();
        }
        public void LoadList()
        {
            IEnumerable<OrderDetail> orderDetails = orderDetailRepository.GetOrderDetailByID(orderId);
            int count = 0;
            foreach (OrderDetail o in orderDetails)
            {
                count++;
            }
            Record.Text = $"Record: {count}";
            tbOrderId.Text = $"Order Id: {orderId}";
            DataGrid.ItemsSource = orderDetails;
        }

        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
