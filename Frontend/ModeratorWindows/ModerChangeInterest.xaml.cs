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
    public partial class ModerChangeInterest : Window
    {
        List<int> newInterest;
        public ModerChangeInterest()
        {
            InitializeComponent();
        }
        public ModerChangeInterest(List<int> inter):this()
        {
            newInterest = inter;

            for(int i =1; i<=80; i++)
            {
                ComboBoxInterest.Items.Add(i);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxInterest.Text=="")
            {
                return;
            }
            newInterest[0] = Int32.Parse(ComboBoxInterest.Text);
            this.Close();
        }
    }
}
