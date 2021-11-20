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
    public partial class CreateProductDiscount : Window
    {
        int moderatorId;
        int selectedProductID;
        string currentStorage;
        int interest;
        public CreateProductDiscount()
        {
            InitializeComponent();
        }

        public CreateProductDiscount(int moderId):this()
        {
            moderatorId = moderId;
            selectedProductID = 0;
            currentStorage = "";
            interest = 0;

            for(int i =1; i<=80; i++)
            {
                ComboBoxProductInterest.Items.Add(i);
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductID == 0)
            {
                MessageBox.Show("Select Product",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if(interest == 0)
            {
                MessageBox.Show("select interest",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if(ProductDiscountManager.Instance().IfProductHasDiscount(selectedProductID))
            {
                MessageBox.Show("Product already has discount",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            //передаємо сигнал медіатору
            ModeratorMediator.Instance().CreateNewProductDiscount(moderatorId,selectedProductID,
                                                                 interest);
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
                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Dairy")
            {
                foreach (Product prod in StorageManager.Instance().DairyStorage)
                {
                    ProductsGridTable.Items.Add(prod);
                }
            }
            else if (type == "Household")
            {
                foreach (Product prod in StorageManager.Instance().HouseholdStorage)
                {
                    ProductsGridTable.Items.Add(prod);
                }
            }
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

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBoxProductInterest_DropDownClosed(object sender, EventArgs e)
        {
            if(ComboBoxProductInterest.Text =="")
            {
                return;
            }
            interest = Int32.Parse(ComboBoxProductInterest.Text);
        }
    }
}
