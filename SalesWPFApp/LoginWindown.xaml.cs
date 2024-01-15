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
    /// Interaction logic for LoginWindown.xaml
    /// </summary>
    public partial class LoginWindown : Window
    {
        IMemberRepository memberRepository;
        public LoginWindown()
        {
            InitializeComponent();
            memberRepository = new MemberRepository();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            // Check required
            string msg = string.Empty;
            if (txtEmail.Text == string.Empty)
            {
                msg += "Email is required. \n";
            }
            if (passwordBox.Password == string.Empty)
            {
                msg += "Password is required.";
            }
            if (msg.Length > 0)
            {
                MessageBox.Show(msg);
            }

            // Check login

            if (memberRepository.Login(txtEmail.Text, passwordBox.Password) == false)
            {
                MessageBox.Show("Email or password wrong! input again");
            }
            else
            {
                 MemberWindown memberWindown = new MemberWindown();
                 memberWindown.Show();
                 this.Close();
            }
        }


        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindown registerWindown = new RegisterWindown();
            registerWindown.Show();
            this.Close();
        }



        // Catch Event

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                textPassword.Visibility = Visibility.Collapsed;
            else
                textPassword.Visibility = Visibility.Visible;
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }


        private void txtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
                textEmail.Visibility = Visibility.Collapsed;
            else
                textEmail.Visibility = Visibility.Visible;
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

    }
}

