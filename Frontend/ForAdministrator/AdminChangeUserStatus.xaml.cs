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

namespace CourseWork.Frontend.ForAdministrator
{
    public partial class AdminChangeUserStatus : Window
    {
        int adminID;

        string currentStatus;

        int selectedCustomerID;

        public AdminChangeUserStatus()
        {
            InitializeComponent();
        }
        public AdminChangeUserStatus(int adminID):this()
        {
            this.adminID = adminID;

            currentStatus = "";

            selectedCustomerID = 0;

            //завопення таблиці
            RefreshTable();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ButtonChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if(currentStatus == "")
            {
                MessageBox.Show("Select status",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            if(selectedCustomerID == 0)
            {
                MessageBox.Show("Select customer",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                return;
            }
            //виклик предається педіатору, а той переадсть фасаду
            AdministratorMediator.Instance().AdministratorChangeUserStatus(adminID, selectedCustomerID, currentStatus);
            RefreshTable();
        }

        private void ComboBoxStatus_DropDownClosed(object sender, EventArgs e)
        {
            string type = ComboBoxStatus.Text;
            //якщо ніочго не вибрали, то нічого не робити
            if (type == "")
            {
                return;
            }
            currentStatus = type;
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
            selectedCustomerID = Int32.Parse(ID);
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
                    CustomersGridTable.ScrollIntoView(rowData, CustomersGridTable.Columns[column]);
                    cell = (DataGridCell)cellPresenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        public DataGridRow GetRow(int index)
        {
            DataGridRow row = (DataGridRow)CustomersGridTable.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                CustomersGridTable.UpdateLayout();
                CustomersGridTable.ScrollIntoView(CustomersGridTable.Items[index]);
                row = (DataGridRow)CustomersGridTable.ItemContainerGenerator.ContainerFromIndex(index);
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
            CustomersGridTable.Items.Clear();
            CustomersGridTable.Items.Refresh();
        }
    }
}
