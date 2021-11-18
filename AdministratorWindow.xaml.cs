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
    /// <summary>
    /// Interaction logic for AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
        }
        //обрали інший тип продукту
        private void ComboBoxProductType_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductsGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
