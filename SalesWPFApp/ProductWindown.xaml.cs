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
using System.Xml.Linq;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductWindown.xaml
    /// </summary>
    public partial class ProductWindown : Window
    {
        ICategoryRepository categoryRepository;
        IProductRepository productRepository;
        IOrderRepository orderRepository;
        IOrderDetailRepository orderDetailRepository;
        public ProductWindown()
        {
            InitializeComponent();
            categoryRepository = new CategoryRepository();
            productRepository = new ProductRepository();
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
            LoadList();
            LoadStatus();
            LoadCategory();
            LoadPrice();
        }

        public void LoadList()
        {
            IEnumerable<Product> products = productRepository.GetProducts();
            int count = 0;
            foreach (Product p in products)
            {
                count++;
            }
            Ghi.Text = $"Record: {count}";
            DataGrid.ItemsSource = products;
        }

        public void LoadCategory()
        {
            cbCategory.ItemsSource = categoryRepository.GetCategories();
            cbCategory.SelectedIndex = 0;
        }

        public void LoadStatus()
        {
            List<string> statusItems = new List<string> { "Available", "Out of Stock" };
            cbStatus.ItemsSource = statusItems;
            cbStatus.SelectedIndex = 0;
        }

        public void LoadPrice()
        {
            List<string> sort = new List<string> { "Default","Ascending", "Descending"};
            SortByPrice.ItemsSource = sort;
            SortByPrice.SelectedIndex = 0;
        }

        private Product GetObject()
        {
            Product product = null;
            try
            {
                product = new Product
                {
                    ProductId = int.Parse(txtId.Text),
                    CategoryId = int.Parse(cbCategory.SelectedValue.ToString()),
                    ProductName = txtProductName.Text,
                    UnitPrice = decimal.Parse(UnitPrice.Text),
                    Status = cbStatus.SelectedItem.ToString()
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Product");
            }
            return product;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = GetObject();
                productRepository.InsertProduct(product);
                LoadList();
                MessageBox.Show($"{product.ProductId} inserted successfully ", "Insert");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = GetObject();
                productRepository.UpdateProduct(product);
                LoadList();
                MessageBox.Show($"{product.ProductId} updated successfully ", "Updated");
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
                Product product = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {product.ProductId}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    productRepository.DeleteProduct(product);
                    LoadList();
                    MessageBox.Show($"{product.ProductId} deleted successfully", "Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
            cbCategory.SelectedIndex = 0;
            txtProductName.Text = "";
            UnitPrice.Text = "";
            cbStatus.SelectedIndex = 0;
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = textBoxFilter.Text;
            int count = 0;
            IEnumerable<Product> products = productRepository.SearchByName(search);
            foreach (Product p in products)
            {
                count++;
            }
            Ghi.Text = $"Record: {count}";
            DataGrid.ItemsSource = products;
        }


        private void btAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show($"Do you want to order this products?", "Confirm Order", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    List<Product> selectedProducts = new List<Product>();
                    foreach (var selectedItem in DataGrid.SelectedItems)
                    {
                        if (selectedItem is Product selectedProduct)
                        {
                            selectedProducts.Add(selectedProduct);
                        }
                    }
                    int getOrderId = 0;
                    //total price
                    decimal total_price = 0;
                    foreach (Product selectedItem in selectedProducts)
                    {
                        total_price = (decimal)(total_price + selectedItem.UnitPrice);
                    }

                    // Add Order
                    int id = Member.SessionDataMember.members[0].MemberId;
                    try
                    {
                        Order order = new Order
                        {
                            MemberId = id,
                            OrderDate = DateTime.Now,
                            ShippedDate = null,
                            TotalPrice = total_price,
                            Status = "Processing"
                        };
                        orderRepository.InsertOrder(order);
                        MessageBox.Show($"Order ID: {order.OrderId} inserted successfully ", "Add Order");
                        getOrderId = order.OrderId;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Add Order");
                    }

                    // Add Order Detail
                    try
                    {
                        List<OrderDetail> orderDetails = new List<OrderDetail>();

                        foreach (Product selectedItem in selectedProducts)
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                OrderId = getOrderId,
                                ProductId = selectedItem.ProductId,
                                UnitPrice = (decimal)selectedItem.UnitPrice,
                                Quantity = 1
                            };
                            orderDetails.Add(orderDetail);
                        }

                        foreach (OrderDetail od in orderDetails)
                        {
                            orderDetailRepository.InsertOrderDetail(od);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Add OrderDetail");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SortByPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid.ItemsSource = productRepository.SortByPrice(SortByPrice.SelectedItem.ToString());
        }

        private void MemberPage_Click(object sender, RoutedEventArgs e)
        {
            MemberWindown memberWindown = new MemberWindown();
            memberWindown.Show();
            this.Close();
        }

        private void ProductPage_Click(object sender, RoutedEventArgs e)
        {
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
