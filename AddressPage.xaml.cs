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
    public partial class AddressPage : Page
    {
        public AddressPage()
        {
            InitializeComponent();
            //ініціалізувати вибір віку
            for (int i = 6; i <= 90; i++)
            {
                ComboBoxAge.Items.Add(i);
            }

        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = PassBox.Password;
            string password_2 = PassBox_2.Password;
            int age;
            if(!Int32.TryParse(ComboBoxAge.Text, out age))
            {
                MessageBox.Show("Select your age",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //у майбутньому можна буде передавати смс на цей номер
            //ьелефона для підтвердження
            string phoneNumber = TextBoxPhoneNumber.Text;
        }
    }
}
