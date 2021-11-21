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
    public partial class ShopWindow : Window
    {
        int customerID;
        int selectedProductID;

        List<int> selectedAmount;


        string currentStorage;

        public ShopWindow()
        {
            InitializeComponent();

        }
        public ShopWindow(int cusID) : this()
        {
            customerID = cusID;
            selectedProductID = 0;
            selectedAmount = new List<int>();
            selectedAmount.Add(0);
            currentStorage = "N/A";

            TextBlockLogin.Text = CustomerMediator.Instance().GetCustomerById(customerID).PersonalData.Login;
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
            selectedProductID = 0;

            string type = ComboBoxProductType.Text;
            //якщо ніочго не вибрали, то нічого не робити
            if(type == "")
            {
                return;
            }
            //оновити таблицю відносно вибраного типу
            RefreshTable(type);

            currentStorage = type;

        }
        private void ClearGridTable()
        {
            //очищення таблиці
            ProductsGridTable.Items.Clear();
            ProductsGridTable.Items.Refresh();
        }
        //оновити дані таблиці----------------------
        public void RefreshTable(string type)
        {
            if (type == "")
            {
                return;
            }

            ClearGridTable();

            if (type == "Meat")
            {
                foreach (Product prod in StorageManager.Instance().MeatStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(customerID, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;

                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Dairy")
            {
                foreach (Product prod in StorageManager.Instance().DairyStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(customerID, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;
                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Household")
            {
                foreach (Product prod in StorageManager.Instance().HouseholdStorage)
                {
                    double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(customerID, prod.ProductId);

                    prod.PriceWithDiscounts = prod.Price - discount;
                    ProductsGridTable.Items.Add(prod);
                }
            }
        }

        //коли натиснули на клітинку, тобто добавили продукт у корзину
        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //необхідна перевірка, щоб не вийшо помилки
            if(currentStorage != ComboBoxProductType.Text)
            {
                return;
            }
            if(ProductsGridTable.Items.Count == 0)
            {
                return;
            }
            object item = ProductsGridTable.SelectedItem;
            string ID = (ProductsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductID = Int32.Parse(ID);
        }

        
        private void ButtonAddToCartProduct_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductID == 0)
            {
                MessageBox.Show("Select Product to add",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }

            //питаємось, скільк покупець хоче взяти
            HowManyProductsAddToCart windowAmount = new HowManyProductsAddToCart(selectedAmount, selectedProductID);
            
            //така функція позволить чекати, поки є 
            //вікрите нше вікно
            windowAmount.ShowDialog();

            CustomerMediator.Instance().AddProductToCustomerCart(customerID,selectedProductID, selectedAmount[0]);
            //оновити баблицю
            RefreshTable(currentStorage);
            //збиваємо кількість
            selectedAmount[0] = 0;
        }

        //відкрити мій профіль
        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            CustomerProfile window = new CustomerProfile(customerID);
            window.ShowDialog();
        }

        //побачити мою корзину
        private void ButtonSeeMyCart_Click(object sender, RoutedEventArgs e)
        {
            CourseWork.Frontend.UserCart.CustomerCart window = new CourseWork.Frontend.UserCart.CustomerCart(customerID);
            window.ShowDialog();

            RefreshTable(currentStorage);
        }

        private void ButtonMyOrders_Click(object sender, RoutedEventArgs e)
        {
            Frontend.OrdersWindows.CustomerOrdersWindow window = new Frontend.OrdersWindows.CustomerOrdersWindow(customerID);
            window.ShowDialog();
            RefreshTable(currentStorage);
        }
    }
}
