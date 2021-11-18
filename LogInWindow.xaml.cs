﻿using System;
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
        CustomerMediator customerMediator;

        public LogInWindow()
        {
            customerMediator = CustomerMediator.Instance();
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
                if (customerMediator.IfCorrectLoginPassword(login, password))
                {
                    int cusId = customerMediator.GetCustomerIdByLogin(login);
                    if (cusId == -1)
                    {
                        MessageBox.Show("Wrong Input Data:\nDon`t have such customer",
                            "Don`t have such customer",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    textBoxLogin.ToolTip = "";
                    PassBox.ToolTip = "";
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
                }

                Console.WriteLine(password);
            }
        }
        //хочуть зареєструватись
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
