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
            //CorrectTableData();

        }
        public void CorrectTableData()
        {
            //корегування інформації, добавляння kg, $ і дати
            for (int i = 0; i < ProductsGridTable.Items.Count; i++)
            {
                //обов'язково перевести в текст блок, щоб отримати дані як string
                TextBlock textBlock = GetCell(i, 0).Content as TextBlock;
                int prodID = Int32.Parse(textBlock.Text);
                //оновлення ціни
                DataGridCell cell1 = GetCell(i, 2);
                cell1.Content = string.Format("{0:f2}$", StorageMediator.Instance().GetProductById(prodID).Price);
                //оновлення ваги
                DataGridCell cell2 = GetCell(i, 3);
                cell2.Content = string.Format("{0:f2}kg", StorageMediator.Instance().GetProductById(prodID).Weight);
                //оновлення терміну придатності
                DataGridCell cell3 = GetCell(i, 5);
                cell3.Content = string.Format("{0}", StorageMediator.Instance().GetProductById(prodID).ExpirationDate.ToShortDateString());
            }
        }

        //допоміжні функції, щоб отримати клітинку по індексу--------------------
        public DataGridCell GetCell(int row, int column)
        {
            DataGridRow rowData = GetRow(row);
            if (rowData != null)
            {
                DataGridCellsPresenter cellPresenter = GetVisualChild<DataGridCellsPresenter>(rowData);
                DataGridCell cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    ProductsGridTable.ScrollIntoView(rowData, ProductsGridTable.Columns[column]);
                    cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)ProductsGridTable.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                ProductsGridTable.UpdateLayout();
                ProductsGridTable.ScrollIntoView(ProductsGridTable.Items[index]);
                row = (DataGridRow)ProductsGridTable.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        //-------------------

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
