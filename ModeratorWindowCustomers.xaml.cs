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
    public partial class ModeratorWindowCustomers : Window
    {

        int moderatorID;

        int selectedCustDiscountID;
        int selectedCustID;

        List<int> changeInterest;

        public ModeratorWindowCustomers()
        {
            InitializeComponent();
        }
        public ModeratorWindowCustomers(int modeID):this()
        {
            moderatorID = modeID;
            changeInterest = new List<int>();
            changeInterest.Add(0);
            TextBlockLogin.Text = ModeratorManager.Instance().GetModeratorById(moderatorID).Login;
            selectedCustDiscountID = 0;
            selectedCustID = 0;
            RefreshTable();
        }

        private void CustomersDiscountsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідна перевірка, щоб не вийшо помилки
            if (CustomersDiscountsGridTable.Items.Count == 0)
            {
                return;
            }
            Console.WriteLine("Work");
            object item = CustomersDiscountsGridTable.SelectedItem;
            string ID = (CustomersDiscountsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            string prodId = (CustomersDiscountsGridTable.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            selectedCustDiscountID = Int32.Parse(ID);
            selectedCustID = Int32.Parse(prodId);
        }
        private void ClearGridTable()
        {
            //очищення таблиці
            CustomersDiscountsGridTable.Items.Clear();
            CustomersDiscountsGridTable.Items.Refresh();
        }
        //оновити дані таблиці----------------------
        public void RefreshTable()
        {
            ClearGridTable();
            foreach (var pair in CustomerDiscountManager.Instance())
            {
                Customer cust = CustomerManager.Instance().GetCustomerById(pair.Key);
                CustomerDiscount disc = pair.Value;

                CustomersDiscountsGridTable.Items.Add(pair);
            }
        }

        private void ButtonCreateCusDiscount_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.Manager.CreateCustomerDiscount window = new CourseWork.Frontend.Manager.CreateCustomerDiscount(moderatorID);
            window.ShowDialog();
            RefreshTable();
        }

        private void ButtonRemoveCusDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustDiscountID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if (selectedCustID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }


            ModeratorMediator.Instance().RemoveCustDiscount(moderatorID, selectedCustID);

            RefreshTable();
            selectedCustDiscountID = 0;
            selectedCustID = 0;
        }

        private void ButtonChangeCusDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustDiscountID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }

            CourseWork.Frontend.ModeratorWindows.ModerChangeInterest window = new Frontend.ModeratorWindows.ModerChangeInterest(changeInterest);
            window.ShowDialog();
            if (changeInterest[0] == 0)
            {
                return;
            }

            ModeratorMediator.Instance().ChangeCustDiscountInterest(selectedCustID, changeInterest[0]);
            changeInterest[0] = 0;
            RefreshTable();
        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            Frontend.Manager.ModerPrifileWindow window = new Frontend.Manager.ModerPrifileWindow(moderatorID);
            window.ShowDialog();
        }

        private void ButtonProductDiscounts_Click(object sender, RoutedEventArgs e)
        {
            ModeratorWindow window = new ModeratorWindow(moderatorID);
            window.Show();
            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Goodbye",
                            "Bye",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            LogInWindow window = new LogInWindow();
            window.Show();
            this.Close();
        }

        private void ButtonViewOrders_Click(object sender, RoutedEventArgs e)
        {
            Frontend.OrdersWindows.ModeratorViewCustomers window = new Frontend.OrdersWindows.ModeratorViewCustomers();
            window.ShowDialog();
            RefreshTable();
        }
    }
}
