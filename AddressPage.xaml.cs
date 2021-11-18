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
        PersonalData customerData;
        public AddressPage()
        {
            InitializeComponent();
        }
        public AddressPage(PersonalData data):this()
        {
            customerData = data;
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            string street = textBoxStreet.Text;
            string country = textBoxCountry.Text;
            string city = textBoxCity.Text;
            int zipcode;

            if(!Int32.TryParse(textBoxZipCode.Text, out zipcode))
            {
                MessageBox.Show("Enter proper zipcode",
                   "Reenter zipcode",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                return;
            }
            if (street.Length < 5 || country.Length < 5 ||
                city.Length < 3)
            {
                MessageBox.Show("Enter proper data",
                   "Reenter data",
                   MessageBoxButton.OK,
                   MessageBoxImage.Information);
                return;
            }

            customerData.Address.Street = street;
            customerData.Address.City = city;
            customerData.Address.Country = country;
            customerData.Address.Zipcode = zipcode;
        }
    }
}
