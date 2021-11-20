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

        string currentStorage;

        int selectedProductDiscountID;
        public ModeratorWindow()
        {
            InitializeComponent();
        }
        public ModeratorWindow(int moderID):this()
        {
            moderatorID = moderID;
            TextBlockLogin.Text = ModeratorManager.Instance().GetModeratorById(moderatorID).Login;
            selectedProductDiscountID = 0;
            currentStorage = "";


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

        private void ComboBoxProductType_DropDownClosed(object sender, EventArgs e)
        {
            //міняємо ід вибраного прудукта назад на 0
            selectedProductDiscountID = 0;

            string type = ComboBoxProductType.Text;
            //якщо ніочго не вибрали, то нічого не робити
            if (type == "")
            {
                return;
            }
            //оновити таблицю відносно вибраного типу
            RefreshTable(type);

            currentStorage = type;
        }

        private void ProductsDiscountsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonCustomerDiscounts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCreateProdDiscount_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.Manager.CreateProductDiscount window = new CourseWork.Frontend.Manager.CreateProductDiscount(moderatorID);
            window.ShowDialog();
            RefreshTable(currentStorage);
        }

        private void ButtonRemoveProdDiscount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonChangeProdDiscount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearGridTable()
        {
            //очищення таблиці
            ProductsDiscountsGridTable.Items.Clear();
            ProductsDiscountsGridTable.Items.Refresh();
        }
        //оновити дані таблиці----------------------
        public void RefreshTable(string type)
        {
            if (type == "")
            {
                return;
            }

            ClearGridTable();


            foreach (var pair in ProductDiscountManager.Instance())
            {
                Product prod = StorageMediator.Instance().GetProductById(pair.Key);
                ProductDiscount disc = pair.Value;

                ProductsDiscountsGridTable.Items.Add(pair);
            }
        }
        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідна перевірка, щоб не вийшо помилки
            if (currentStorage != ComboBoxProductType.Text)
            {
                return;
            }
            if (ProductsDiscountsGridTable.Items.Count == 0)
            {
                return;
            }
            object item = ProductsDiscountsGridTable.SelectedItem;
            string ID = (ProductsDiscountsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductDiscountID = Int32.Parse(ID);
        }
    }
}
