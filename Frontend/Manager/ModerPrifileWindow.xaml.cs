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
    public partial class ModerPrifileWindow : Window
    {
        int moderID;
        public ModerPrifileWindow()
        {
            InitializeComponent();
        }
        public ModerPrifileWindow(int ID):this()
        {
            moderID = ID;
            Moderator moder = ModeratorMediator.Instance().GetModerById(moderID);
            TextBlockCustomerLogin.Text = moder.Login;
            TextBlockCustomerPassword.Text = moder.Password;
            TextBlockPhone.Text = moder.PhoneNumber;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
