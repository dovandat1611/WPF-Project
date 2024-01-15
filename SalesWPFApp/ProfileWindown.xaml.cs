using SalesWPFAppLibrary.DataAccess;
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
    /// Interaction logic for ProfileWindown.xaml
    /// </summary>
    public partial class ProfileWindown : Window
    {
        public ProfileWindown()
        {
            InitializeComponent();
            LoadProfile();
        }

        public void LoadProfile()
        {
            Member member = Member.SessionDataMember.members[0];
            Email.Text = member.Email;
            Name.Text = member.FullName;
            Address.Text = member.Address;
            if(member.Gender == true)
            {
                Male.IsChecked = true;
            }
            else
            {
                Female.IsChecked= true;
            }
            DOB.SelectedDate = member.Dob;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
