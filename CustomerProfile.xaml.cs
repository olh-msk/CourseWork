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

namespace CourseWork
{

    public partial class CustomerProfile : Window
    {
        int customerID;
        public CustomerProfile()
        {
            InitializeComponent();
        }
        public CustomerProfile(int cusID):this()
        {
            customerID = cusID;
            //ініціалізія
            SetCustomerInformation();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SetCustomerInformation()
        {
            Customer customer = CustomerMediator.Instance().GetCustomerById(customerID);

            TextBlockCustomerLogin.Text = customer.PersonalData.Login;
            TextBlockCustomerPassword.Text = customer.PersonalData.Password;
            TextBlockCustomerStatus.Text = customer.GetStatusString();
            TextBlockCustomerMoney.Text = customer.PersonalData.Money.ToString();
            TextBlockCustomerEmail.Text = customer.PersonalData.Email;
            TextBlockCustomerAge.Text = customer.PersonalData.Age.ToString();
        }

        private void ButtonAddMoney_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = CustomerMediator.Instance().GetCustomerById(customerID);
            customer.PersonalData.Money += 300;
            SetCustomerInformation();
        }
    }
}
