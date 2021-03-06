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
    public partial class LogInWindow : Window
    {

        public LogInWindow()
        {
            InitializeComponent();
        }

        //натиснули кнопку залогінитись
        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = PassBox.Password;
            string role = ComboBoxRole.Text;

            if(role =="")
            {
                MessageBox.Show("Select Your role",
                    "Select Role",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //виконуємо різні операції під різні ролі
            if(role == "Customer")
            {
                if (CustomerMediator.Instance().IfCorrectLoginPassword(login, password))
                {
                    int customerID = CustomerMediator.Instance().GetCustomerIdByLogin(login);
                    if (customerID == -1)
                    {
                        MessageBox.Show("Wrong Input Data:\nDon`t have such customer",
                            "Don`t have such customer",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    textBoxLogin.ToolTip = "";
                    PassBox.ToolTip = "";
                    ShopWindow window = new ShopWindow(customerID);
                    window.Show();
                    this.Close();
                }
                //якщо неправильний логін чи пароль, то виводимо
                //підказку на це
                else
                {
                    textBoxLogin.ToolTip = "Try Again";
                    PassBox.ToolTip = "Try Again";
                    MessageBox.Show("Wrong Login or Password",
                        "Check your data",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            }
            else if(role == "Admin")
            {
                if (AdministratorMediator.Instance().IfCorrectLoginPassword(login, password))
                {
                    int adminID = AdministratorMediator.Instance().GetAdminIdByLogin(login);
                    if (adminID == -1)
                    {
                        MessageBox.Show("Wrong Input Data:\nDon`t have such customer",
                            "Don`t have such customer",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    textBoxLogin.ToolTip = "";
                    PassBox.ToolTip = "";
                    AdministratorWindow window = new AdministratorWindow(adminID);
                    window.Show();
                    this.Close();
                }
                //якщо неправильний логін чи пароль, то виводимо
                //підказку на це
                else
                {
                    textBoxLogin.ToolTip = "Try Again";
                    PassBox.ToolTip = "Try Again";
                    MessageBox.Show("Wrong Login or Password",
                        "Check your data",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            }
            else if(role == "Moderator")
            {
                if (ModeratorMediator.Instance().IfCorrectLoginPassword(login, password))
                {
                    int moderID = ModeratorMediator.Instance().GetModerIdByLogin(login);
                    if (moderID == -1)
                    {
                        MessageBox.Show("Wrong Input Data:\nDon`t have such customer",
                            "Don`t have such customer",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    textBoxLogin.ToolTip = "";
                    PassBox.ToolTip = "";
                    ModeratorWindow window = new ModeratorWindow(moderID);
                    window.Show();
                    this.Close();
                }
                //якщо неправильний логін чи пароль, то виводимо
                //підказку на це
                else
                {
                    textBoxLogin.ToolTip = "Try Again";
                    PassBox.ToolTip = "Try Again";
                    MessageBox.Show("Wrong Login or Password",
                        "Check your data",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            }
        }
        //хочуть зареєструватись-----------------
        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            string role = ComboBoxRole.Text;
            if(role != "Customer")
            {
                MessageBox.Show("Only customers can sign in",
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            //відкриваємо форму для регістрації
            CustomerRegistrationWindow newWindow = new CustomerRegistrationWindow();
            newWindow.Show();
            this.Close();
        }
    }
}
