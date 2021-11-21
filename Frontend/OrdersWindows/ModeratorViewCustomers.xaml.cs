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
    public partial class ModeratorViewCustomers : Window
    {
        int selectedCustId;
        public ModeratorViewCustomers()
        {
            InitializeComponent();
            selectedCustId = 0;
            RefreshTable();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            if(selectedCustId == 0)
            {
                MessageBox.Show("Select customer",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }

            CustomerOrdersWindow window = new CustomerOrdersWindow(selectedCustId);
            window.ShowDialog();
            selectedCustId = 0;
            RefreshTable();
            
        }

        private void CustomersGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідні перевірка, щоб не вийшо помилки

            if (CustomersGridTable.Items.Count == 0)
            {
                return;
            }


            object item = CustomersGridTable.SelectedItem;
            string ID = (CustomersGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedCustId = Int32.Parse(ID);
        }

        public void RefreshTable()
        {
            ClearGridTable();

            //завопення таблиці
            foreach (Customer cus in CustomerManager.Instance())
            {
                //додаємо лише тих, хто має замовлення
                if(OrderManager.Instance().IfHasCustomerOrders(cus.CustomerId))
                {
                    if(OrderManager.Instance().GetCustomerOrdersById(cus.CustomerId).Count != 0)
                    {
                        CustomersGridTable.Items.Add(cus);
                    }
                    
                }
            }
        }

        private void ClearGridTable()
        {
            //очищення таблиці
            CustomersGridTable.Items.Clear();
            CustomersGridTable.Items.Refresh();
        }
    }
}
