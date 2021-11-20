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

namespace CourseWork.Frontend.UserCart
{
    public partial class CustomerCart : Window
    {
        int customerID;
        int selectedProductID;

        List<int> selectedAmount;

        public CustomerCart()
        {
            InitializeComponent();
        }
        public CustomerCart(int cusID):this()
        {
            selectedProductID = 0;
            selectedAmount = new List<int>();
            selectedAmount.Add(0);
            customerID = cusID;
            RefreshTable();
        }

        private void ButtonRemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if(selectedProductID == 0)
            {
                MessageBox.Show("Select product",
                            "Warning",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }

            HowManyRemoveFromCart window = new HowManyRemoveFromCart(selectedAmount, selectedProductID);
            window.ShowDialog();

            //запит передається на медіатор, а той на клас
            CustomerMediator.Instance().RemoveProductFromCustomerCart(customerID, selectedProductID,selectedAmount[0]);
            //оновити баблицю
            RefreshTable();
            //збиваємо кількість
            selectedAmount[0] = 0;
        }

        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGridTable.Items.Count == 0)
            {
                return;
            }
            object item = ProductsGridTable.SelectedItem;
            string ID = (ProductsGridTable.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            selectedProductID = Int32.Parse(ID);

            string Amount = (ProductsGridTable.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
        }
        public void RefreshTable()
        {
            ClearGridTable();
            Customer cus = CustomerMediator.Instance().GetCustomerById(customerID);

            foreach (var pair in cus.ShoppingCart)
            {
                Product prod = StorageMediator.Instance().GetProductById(pair.Key);
                //очищення і втсановлення кількості, що має користувач
                prod.AmountCustomerHas = 0;
                prod.AmountCustomerHas = cus.ShoppingCart.GetProductAmountById(prod.ProductId);
                ProductsGridTable.Items.Add(prod);
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

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //зробити замовлення і отримати чек
        private void ButtonMakeOrder_Click(object sender, RoutedEventArgs e)
        {
            if(ProductsGridTable.Items.Count==0)
            {
                MessageBox.Show("Add some products to cart",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }



        }
    }
}
