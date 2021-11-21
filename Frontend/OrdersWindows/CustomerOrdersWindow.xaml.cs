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

    public partial class CustomerOrdersWindow : Window
    {
        int customerID;
        int selectedOrderID;
        public CustomerOrdersWindow()
        {
            InitializeComponent();
        }

        public CustomerOrdersWindow(int cusID):this()
        {
            this.customerID = cusID;
            selectedOrderID = 0;
            TextLogin.Text = CustomerManager.Instance().GetCustomerById(cusID).PersonalData.Login;
            RefreshTable();
        }

        //переглянути деталі замовлення, їх не можна буде змінити-------
        private void ButtonDetails_Click(object sender, RoutedEventArgs e)
        {
            if(OrderManager.Instance().GetCustomerOrdersById(customerID).Count == 0)
            {
                MessageBox.Show("Don`t have orders",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if(selectedOrderID == 0)
            {
                MessageBox.Show("select order",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            OrderDetails window = new OrderDetails(customerID,selectedOrderID);
            window.ShowDialog();
            selectedOrderID = 0;
            RefreshTable();
        }

        private void ButtonRemoveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrderID == 0)
            {
                MessageBox.Show("Select order",
                            "Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }
            
            //запит передається на медіатор, а той на клас
            CustomerMediator.Instance().CustomerRemoveOrder(customerID, selectedOrderID);
            //оновити баблицю
            //збиваємо вибраний 
            selectedOrderID = 0;
            RefreshTable();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OrdersGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersGridTable.Items.Count == 0)
            {
                return;
            }
            object item = OrdersGridTable.SelectedItem;
            string ID = (OrdersGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedOrderID = Int32.Parse(ID);
        }

        public void RefreshTable()
        {
            ClearGridTable();
            Customer cus = CustomerMediator.Instance().GetCustomerById(customerID);
            List<Order> cusOrders = OrderManager.Instance().GetCustomerOrdersById(customerID);

            foreach (Order order in cusOrders)
            {
                OrdersGridTable.Items.Add(order);
            }
        }

        private void ClearGridTable()
        {
            //очищення таблиці
            OrdersGridTable.Items.Clear();
            OrdersGridTable.Items.Refresh();
        }
    }
}
