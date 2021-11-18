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
            MainRegisterFrame.Content = new CustomerAcountRegisterPage();
        }

        private void ButtonAdress_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
