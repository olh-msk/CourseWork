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
    public partial class ModeratorWindow : Window
    {
        int moderatorID;

        int selectedProductDiscountID;
        int selectedProdID;

        List<int> changeInterest;
        public ModeratorWindow()
        {
            InitializeComponent();
        }
        public ModeratorWindow(int moderID):this()
        {
            moderatorID = moderID;
            changeInterest = new List<int>();
            changeInterest.Add(0);
            TextBlockLogin.Text = ModeratorManager.Instance().GetModeratorById(moderatorID).Login;
            selectedProductDiscountID = 0;
            selectedProdID = 0;
            RefreshTable();
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

        private void ProductsDiscountsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідна перевірка, щоб не вийшо помилки
            if (ProductsDiscountsGridTable.Items.Count == 0)
            {
                return;
            }
            Console.WriteLine("Work");
            object item = ProductsDiscountsGridTable.SelectedItem;
            string ID = (ProductsDiscountsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            string prodId = (ProductsDiscountsGridTable.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductDiscountID = Int32.Parse(ID);
            selectedProdID = Int32.Parse(prodId);
        }

        private void ButtonCustomerDiscounts_Click(object sender, RoutedEventArgs e)
        {
            ModeratorWindowCustomers window = new ModeratorWindowCustomers(moderatorID);
            window.Show();
            this.Close();
        }

        private void ButtonCreateProdDiscount_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.Manager.CreateProductDiscount window = new CourseWork.Frontend.Manager.CreateProductDiscount(moderatorID);
            window.ShowDialog();
            RefreshTable();
        }

        private void ButtonRemoveProdDiscount_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductDiscountID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if(selectedProdID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }


            ModeratorMediator.Instance().RemoveProductDiscount(moderatorID, selectedProdID);

            RefreshTable();
            selectedProductDiscountID = 0;
            selectedProdID = 0;
        }

        private void ButtonChangeProdDiscount_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductDiscountID == 0)
            {
                MessageBox.Show("select discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }

            CourseWork.Frontend.ModeratorWindows.ModerChangeInterest window = new Frontend.ModeratorWindows.ModerChangeInterest(changeInterest);
            window.ShowDialog();
            if(changeInterest[0] == 0)
            {
                return;
            }

            ModeratorMediator.Instance().ChangeProdDiscountInterest(selectedProdID, changeInterest[0]);
            changeInterest[0] = 0;
            RefreshTable();
        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            Frontend.Manager.ModerPrifileWindow window = new Frontend.Manager.ModerPrifileWindow(moderatorID);
            window.ShowDialog();
        }

        private void ClearGridTable()
        {
            //очищення таблиці
            ProductsDiscountsGridTable.Items.Clear();
            ProductsDiscountsGridTable.Items.Refresh();
        }
        //оновити дані таблиці----------------------
        public void RefreshTable()
        {
            ClearGridTable();
            foreach (var pair in ProductDiscountManager.Instance())
            {
                Product prod = StorageMediator.Instance().GetProductById(pair.Key);
                ProductDiscount disc = pair.Value;

                ProductsDiscountsGridTable.Items.Add(pair);
            }
        }

        private void ButtonViewOrders_Click(object sender, RoutedEventArgs e)
        {
            Frontend.OrdersWindows.ModeratorViewCustomers window = new Frontend.OrdersWindows.ModeratorViewCustomers();
            window.ShowDialog();
            RefreshTable();
        }
    }
}
