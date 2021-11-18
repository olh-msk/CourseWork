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
    public partial class CustomerRegistrationWindow : Window
    {
        //може мпілкуватись  змедіатором
        //призначеним для форми і покпців
        private CustomerMediator customerMediator;

        PersonalData customerData; 

        public CustomerRegistrationWindow()
        {
            customerData = new PersonalData();
            customerMediator = CustomerMediator.Instance();
            InitializeComponent();
            ButtonCreateAcount.Visibility = Visibility.Collapsed;
            //передаємо на заповнення інформацію
            MainRegisterFrame.Content = new CustomerAcountRegisterPage(customerData);
        }
        //перейти на заповнення адреси----------------
        private void ButtonAdress_Click(object sender, RoutedEventArgs e)
        {
            if(customerData.Login == "N/A" || customerData.PnoneNumber == "N/A"||
                customerData.Email == "N/A")
            {
                MessageBox.Show("You should first fill these fields and save them",
                   "Fill fields",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                return;
            }
            MainRegisterFrame.Content = new AddressPage(customerData);

            ButtonAdress.Visibility = Visibility.Collapsed;
            ButtonCreateAcount.Visibility = Visibility.Visible;
        }
        //створити акаунт---------
        private void ButtonCreateAcount_Click(object sender, RoutedEventArgs e)
        {
            if (customerData.Address.Street == "N/A" || customerData.Address.Country == "N/A"
                || customerData.Address.Zipcode == 0)
            {
                MessageBox.Show("You should first fill address fields and save them",
                   "Fill fields",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                return;
            }

            CustomerMediator.Instance().CreateNewCustomer(customerData);
            MessageBox.Show("Registration was successful",
                   "Successful",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
            LogInWindow window = new LogInWindow();
            window.Show();
            this.Close();
        }
    }
}
