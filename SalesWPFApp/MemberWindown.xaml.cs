using SalesWPFAppLibrary.DataAccess;
using SalesWPFAppLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
    /// Interaction logic for MemberWindown.xaml
    /// </summary>
    public partial class MemberWindown : Window
    {
        IMemberRepository memberRepository;
        public MemberWindown()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
            LoadMemberList();
        }

        public void LoadMemberList()
        {
            IEnumerable<Member> members = memberRepository.GetMembers();
            int count = 0;
            foreach (Member member in members)
            {
                count++;
            }
            Ghi.Text = $"Record: {count}";
            DataGrid.ItemsSource = members;
        }

        private void TextBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = textBoxFilter.Text;
            int count = 0;
            IEnumerable<Member> members = memberRepository.SearchByEmail(search);
            foreach (Member member in members)
            {
                count++;
            }
            Ghi.Text = $"Record: {count}";
            DataGrid.ItemsSource = members;
        }

        private Member GetObject()
        {
            Member member = null;
            bool gender = false;
            bool status = false;
            if (Male.IsChecked == true)
            {
                gender = true;
            }
            if(cbStatus.IsChecked == true)
            {
                status = true;
            }
            try
            {
                member = new Member
                {
                    MemberId = int.Parse(txtId.Text),
                    FullName = txtName.Text,
                    Email = txtEmail.Text,
                    Dob = DOB.SelectedDate ?? DateTime.Now,
                    Address = txtAddress.Text,
                    Gender = gender,
                    Password = txtPassword.Text,
                    Status = status
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            DOB.SelectedDate = DateTime.Now;
            txtAddress.Text = "";
            Male.IsChecked = false;
            Female.IsChecked = false;
            txtPassword.Text = "";
            cbStatus.IsChecked = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetObject();
                memberRepository.InsertMember(member);
                LoadMemberList();
                MessageBox.Show($"{member.MemberId} inserted successfully ", "Insert member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert member");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetObject();
                memberRepository.UpdateMember(member);
                LoadMemberList();
                MessageBox.Show($"{member.MemberId} updated successfully ", "Update member");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetObject();
                MessageBoxResult result = MessageBox.Show($"Do you want to delete {member.MemberId}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    memberRepository.DeleteMember(member);
                    LoadMemberList();
                    MessageBox.Show($"{member.MemberId} deleted successfully", "Delete car");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete car");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindown loginWindown = new LoginWindown();
            loginWindown.Show();
            this.Close();
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

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindown profileWindown = new ProfileWindown();
            profileWindown.Show();
        }
    }
}
