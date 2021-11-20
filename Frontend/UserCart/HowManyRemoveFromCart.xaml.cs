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

namespace CourseWork.Frontend.UserCart
{
    public partial class HowManyRemoveFromCart : Window
    {
        public List<int> selectedAmount { get; set; }
        public HowManyRemoveFromCart()
        {
            InitializeComponent();
        }
        public HowManyRemoveFromCart(List<int> amount, int prodID) : this()
        {
            selectedAmount = amount;

            int haveInCart = StorageMediator.Instance().GetProductById(prodID).AmountCustomerHas;

            for (int i = 1; i <= haveInCart; i++)
            {
                ComboBoxAmount.Items.Add(i);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            string chosedAmount = ComboBoxAmount.Text;
            if (chosedAmount == "")
            {
                MessageBox.Show("Choose amount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                return;
            }

            selectedAmount[0] = Int32.Parse(chosedAmount);

            this.Close();
        }
    }
}
