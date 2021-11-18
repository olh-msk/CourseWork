using System;
using System.Collections.Generic;
using System.IO;
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
    //призначений чисто для ініціалізації---------------------
    //викорстовується чисто в демонстративних цілях програми
    public partial class InitializingWindow : Window
    {
        public InitializingWindow()
        {
            InitializeComponent();
            Shop shop = Shop.Instance();
            //ініціалізація
            string pathToCustomers = @"C:\Users\Acer\OneDrive\Робочий стіл\C#\CourseWork\Backend\CustomersData.txt";

            string pathToProducts = @"C:\Users\Acer\OneDrive\Робочий стіл\C#\CourseWork\Backend\ProductData.txt";
            string pathToAdmins = @"C:\Users\Acer\OneDrive\Робочий стіл\C#\CourseWork\Backend\AdministratorData.txt";
            string pathToModers = @"C:\Users\Acer\OneDrive\Робочий стіл\C#\CourseWork\Backend\ModeratorData.txt";

            CustomerMediator.Instance().ReadCustomersFromTxtFile(pathToCustomers);
            StorageMediator.Instance().ReadProductsFromFile(pathToProducts);
            StaffMediator.Instance().ReadAdminsFromFile(pathToAdmins);
            StaffMediator.Instance().ReadModersFromFile(pathToModers);
            
            LogInWindow window = new LogInWindow();

            window.Show();

            this.Close();
        }
    }
}
