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

namespace CourseWork.Frontend.OrdersWindows
{
    public partial class OrderDetails : Window
    {
        int customerID;
        int orderID;
        public OrderDetails()
        {
            InitializeComponent();
        }

        public OrderDetails(int custID, int ordID):this()
        {
            customerID = custID;
            orderID = ordID;

            TextLogin.Text = CustomerManager.Instance().GetCustomerById(custID).PersonalData.Login;
            TextOrderId.Text = orderID.ToString();
            FillTable();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillTable()
        {
            if(!OrderManager.Instance().IfHasCustomerOrders(customerID))
            {
                return;
            }
            if(OrderManager.Instance().GetCustomerOrdersById(customerID).Count==0)
            {
                return;
            }
            Order order = OrderManager.Instance().GetOrderById(orderID);

            //ключ - його id значення - кількість замовлених продуктів
            foreach (KeyValuePair<int, int> pair in order)
            {
                Product prod = StorageManager.Instance().GetProductByID(pair.Key);
                //корегуємо кількість відповідно до користувача

                //очищення і втсановлення кількості, що має користувач
                prod.AmountCustomerHas = 0;
                prod.AmountCustomerHas = pair.Value;

                double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(customerID, prod.ProductId);

                prod.PriceWithDiscounts = prod.Price - discount;
                prod.TotalPriceForCustomer = prod.PriceWithDiscounts * prod.AmountCustomerHas;

                ProductsGridTable.Items.Add(prod);
            }
        }

        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
