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

namespace CourseWork.Frontend.ForAdministrator 
{
    public partial class EmployeeProfile : Window
    {
        int adminID;
        public EmployeeProfile()
        {
            InitializeComponent();
        }
        public EmployeeProfile(int ID):this()
        {
            adminID = ID;
            Administrator admin = AdministratorMediator.Instance().GetAdministratorById(adminID);
            TextBlockCustomerLogin.Text = admin.Login;
            TextBlockCustomerPassword.Text = admin.Password;
            TextBlockPhone.Text = admin.PhoneNumber;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
