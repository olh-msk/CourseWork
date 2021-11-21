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

namespace CourseWork.Frontend.Manager
{
    public partial class CreateCustomerDiscount : Window
    {
        int moderatorId;
        int selectedCustID;
        int interest;

        public CreateCustomerDiscount()
        {
            InitializeComponent();
        }
        public CreateCustomerDiscount(int moderID):this()
        {
            moderatorId = moderID;
            selectedCustID = 0;
            interest = 0;

            for (int i = 1; i <= 80; i++)
            {
                ComboBoxCustInterest.Items.Add(i);
            }
            RefreshTable();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CustomersGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersGridTable.Items.Count == 0)
            {
                return;
            }
            object item = CustomersGridTable.SelectedItem;
            string ID = (CustomersGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedCustID = Int32.Parse(ID);
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustID == 0)
            {
                MessageBox.Show("Select Product",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if (interest == 0)
            {
                MessageBox.Show("select interest",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if (CustomerDiscountManager.Instance().IfCustomerHasDiscount(selectedCustID))
            {
                MessageBox.Show("Customer already has discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            //передаємо сигнал медіатору
            ModeratorMediator.Instance().CreateNewCustomerDiscount(moderatorId, selectedCustID,
                                                                 interest);
        }
        public void RefreshTable()
        {
            ClearGridTable();
            //завопення таблиці
            foreach (Customer cus in CustomerManager.Instance())
            {
                CustomersGridTable.Items.Add(cus);
            }
        }
        private void ClearGridTable()
        {
            //очищення таблиці
            CustomersGridTable.Items.Clear();
            CustomersGridTable.Items.Refresh();
        }
        private void ComboBoxCustInterest_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxCustInterest.Text == "")
            {
                return;
            }
            interest = Int32.Parse(ComboBoxCustInterest.Text);
        }
    }
}
