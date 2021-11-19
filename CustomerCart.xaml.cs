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
    /// <summary>
    /// Interaction logic for CustomerCart.xaml
    /// </summary>
    public partial class CustomerCart : Window
    {
        int customerId;
        public CustomerCart()
        {
            InitializeComponent();
        }
        public CustomerCart(int cusID)
        {
            customerId = cusID;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
