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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for CustomerAcountRegisterPage.xaml
    /// </summary>
    public partial class CustomerAcountRegisterPage : Page
    {
        PersonalData customerData;
        public CustomerAcountRegisterPage()
        {
            InitializeComponent();
            //ініціалізувати вибір віку
            for (int i = 6; i <= 90; i++)
            {
                ComboBoxAge.Items.Add(i);
            }
        }
        public CustomerAcountRegisterPage(PersonalData data):this()
        {
            customerData = data;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = PassBox.Password;
            string password_2 = PassBox_2.Password;
            //у майбутньому можна буде передавати смс на цей номер
            //телефона для підтвердження
            string phoneNumber = TextBoxPhoneNumber.Text;
            string email = textBoxEmail.Text;
            int age;
            if (!Int32.TryParse(ComboBoxAge.Text, out age))
            {
                MessageBox.Show("Select your age",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //ідеться довга перевірка на коректність даних
            if(CustomerMediator.Instance().IfHasSuchLogin(login))
            {
                MessageBox.Show("An account with that login already exists\n" +
                    "Enter new login",
                    "Enter new login",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            if(password.Length < 3 || login.Length < 3)
            {
                MessageBox.Show("You should have at least 3 symbols for login and password",
                    "Enter new data",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            if(password != password_2)
            {
                MessageBox.Show("Passwords must be equal",
                    "Reenter password",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            if(phoneNumber.Length<10)
            {
                MessageBox.Show("Enter correct phone number",
                   "Reenter phone number",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                return;
            }
            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Enter proper email",
                    "Reenter email",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //коли пройде всі перевірки
            customerData.Login = login;
            customerData.Password = password;
            customerData.Email = email;
            customerData.Age = age;
            customerData.PnoneNumber = phoneNumber;
        }
    }
}
