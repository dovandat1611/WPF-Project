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
    /// Interaction logic for RegisterWindown.xaml
    /// </summary>
    public partial class RegisterWindown : Window
    {
        IMemberRepository memberRepository;
        LoginWindown loginWindown;
        public RegisterWindown()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private Member GetObject()
        {
            Member member = null;
            bool gender = false;
            if (Male.IsChecked == true) {
                gender = true;
            }
            try
            {
                member = new Member
                {
                    FullName = Name.Text,
                    Email = Email.Text,
                    Dob = DOB.SelectedDate ?? DateTime.Now,
                    Address = Address.Text,
                    Gender = gender,
                    Password = pbPassword.Password,
                    Status = true
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return member;
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string password = pbPassword.Password;
            string rePassword = pbRePassword.Password;

            if (password.Equals(rePassword))
            {
                try
                {
                    Member member = GetObject();
                    memberRepository.InsertMember(member);
                    MessageBox.Show("Register successfully!", "Register");
                    loginWindown = new LoginWindown();
                    loginWindown.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Insert member");
                }
            }
            else
            {
                MessageBox.Show("Password and RePassword do not match", "Register");
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            loginWindown = new LoginWindown();
            loginWindown.Show();
            this.Close();
        }
    }
}
