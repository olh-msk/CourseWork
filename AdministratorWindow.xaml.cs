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

namespace CourseWork
{

    public partial class AdministratorWindow : Window
    {
        int admodistratorID;
        int selectedProductID;
        string currentStorage;
        public AdministratorWindow()
        {
            InitializeComponent();
        }
        public AdministratorWindow(int adminID):this()
        {
            admodistratorID = adminID;
            TextBlockLogin.Text = AdministratorMediator.Instance().GetAdministratorById(adminID).Login;
        }

        //обрали інший тип продукту
        private void ComboBoxProductType_DropDownClosed(object sender, EventArgs e)
        {
            //міняємо ід вибраного прудукта назад на 0
            selectedProductID = 0;

            string type = ComboBoxProductType.Text;
            //якщо ніочго не вибрали, то нічого не робити
            if (type == "")
            {
                return;
            }
            RefreshTable(type);

            currentStorage = type;

        }
        public void RefreshTable(string type)
        {
            ClearGridTable();
            if (type == "Meat")
            {
                foreach (Product prod in StorageManager.Instance().MeatStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(0, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;
                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Dairy")
            {
                foreach (Product prod in StorageManager.Instance().DairyStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(0, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;
                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Household")
            {
                foreach (Product prod in StorageManager.Instance().HouseholdStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(0, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;
                    ProductsGridTable.Items.Add(prod);
                }
            }
            //CorrectTableData();

        }
        private void ClearGridTable()
        {
            //очищення таблиці
            ProductsGridTable.Items.Clear();
            ProductsGridTable.Items.Refresh();
        }

        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідна перевірка, щоб не вийшо помилки
            if (currentStorage != ComboBoxProductType.Text)
            {
                return;
            }
            if (ProductsGridTable.Items.Count == 0)
            {
                return;
            }
            object item = ProductsGridTable.SelectedItem;
            string ID = (ProductsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductID = Int32.Parse(ID);
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

        private void ButtonUserStatus_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.ForAdministrator.AdminChangeUserStatus window = new CourseWork.Frontend.ForAdministrator.AdminChangeUserStatus(admodistratorID);

            window.ShowDialog();
        }
        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonAddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.ForAdministrator.AdminAddNewProduct window = new Frontend.ForAdministrator.AdminAddNewProduct(admodistratorID);
            window.ShowDialog();
            RefreshTable(currentStorage);
        }

        private void ButtonRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductID == 0)
            {
                MessageBox.Show("Select product",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            //запит пененаправляється в медіатор, щоб не було сильної зв'язності
            AdministratorMediator.Instance().RemoveHoleProduct(admodistratorID, selectedProductID, currentStorage);
            //оновлюємо таблицю
            RefreshTable(currentStorage);
            selectedProductID = 0;
        }
    }
}
