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

namespace CourseWork.Frontend.ModeratorWindows
{
    public partial class ModeratorWindow : Window
    {
        int moderatorID;
        public ModeratorWindow()
        {
            InitializeComponent();
        }
        public ModeratorWindow(int modID):this()
        {
            moderatorID = modID;
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

        private void CustomersGridTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //профіль модератора
        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
