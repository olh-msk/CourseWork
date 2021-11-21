using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork.Frontend.UserCart
{
    public partial class CustomerCart : Window
    {
        int customerID;
        int selectedProductID;

        List<int> selectedAmount;

        public CustomerCart()
        {
            InitializeComponent();
        }
        public CustomerCart(int cusID):this()
        {
            selectedProductID = 0;
            selectedAmount = new List<int>();
            selectedAmount.Add(0);
            customerID = cusID;
            RefreshTable();
        }

        private void ButtonRemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductID == 0)
            {
                MessageBox.Show("Select product",
                            "Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }

            HowManyRemoveFromCart window = new HowManyRemoveFromCart(selectedAmount, selectedProductID);
            window.ShowDialog();

            //запит передається на медіатор, а той на клас
            CustomerMediator.Instance().RemoveProductFromCustomerCart(customerID, selectedProductID,selectedAmount[0]);
            //оновити баблицю
            RefreshTable();
            //збиваємо кількість
            selectedAmount[0] = 0;
        }

        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGridTable.Items.Count == 0)
            {
                return;
            }
            object item = ProductsGridTable.SelectedItem;
            string ID = (ProductsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductID = Int32.Parse(ID);

            string Amount = (ProductsGridTable.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
        }
        public void RefreshTable()
        {
            ClearGridTable();
            Customer cus = CustomerMediator.Instance().GetCustomerById(customerID);

            foreach (var pair in cus.ShoppingCart)
            {
                Product prod = StorageMediator.Instance().GetProductById(pair.Key);
                //очищення і втсановлення кількості, що має користувач
                prod.AmountCustomerHas = 0;
                prod.AmountCustomerHas = cus.ShoppingCart.GetProductAmountById(prod.ProductId);

                double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(customerID, prod.ProductId);

                prod.PriceWithDiscounts = prod.Price - discount;
                prod.TotalPriceForCustomer = prod.PriceWithDiscounts * prod.AmountCustomerHas;

                ProductsGridTable.Items.Add(prod);
            }
        }
        
        private void ClearGridTable()
        {
            //очищення таблиці
            ProductsGridTable.Items.Clear();
            ProductsGridTable.Items.Refresh();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //зробити замовлення і отримати чек
        private void ButtonMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if(ProductsGridTable.Items.Count==0)
            {
                MessageBox.Show("Add some products to cart",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return; 
            }
            CustomerCreateOrder window = new CustomerCreateOrder(customerID);
            window.ShowDialog();
            selectedProductID = 0;
            RefreshTable();
        }
    }
}
