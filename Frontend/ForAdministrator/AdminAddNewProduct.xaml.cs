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

namespace CourseWork.Frontend.ForAdministrator
{
    public partial class AdminAddNewProduct : Window
    {
        int adminID;
        string selectedDate;
        string selectedType;
        int selectedAmount;

        public AdminAddNewProduct()
        {
            InitializeComponent();
        }
        public AdminAddNewProduct(int admID):this()
        {
            adminID = admID;
            selectedDate = "";
            selectedType = "";


            for(int i =1; i<=15; i++)
            {
                ComboBoxProductAmount.Items.Add(i);
            }
        }

        private void ComboBoxProductType_DropDownClosed(object sender, EventArgs e)
        {
            selectedType = ComboBoxProductType.Text;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            double price;
            double weight;
            if (name == "")
            {
                MessageBox.Show("Enter proper name",
               "Info",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                return;
            }
            if (!Double.TryParse(textBoxPrice.Text,out price)||(price<=0))
            {
                MessageBox.Show("Enter proper price",
               "Info",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                return;
            }
            if (!Double.TryParse(TextBoxWeight.Text, out weight)|| (weight<=0))
            {
                MessageBox.Show("Enter proper weight",
               "Info",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                return;
            }
            if(selectedAmount ==0 || selectedType=="" || selectedDate=="")
            {
                MessageBox.Show("Enter all information",
               "Info",
               MessageBoxButton.OK,
               MessageBoxImage.Information);
                return;
            }
            
            //передаємо посилання медіатору
            AdministratorMediator.Instance().AdministratorAddNewProduct(adminID, name,price,weight,
                                                   selectedAmount, selectedType,selectedDate);
            //оцищуємо поперерні дані
            RefreshAllFields();
        }
        private void RefreshAllFields()
        {
            textBoxName.Text = "";
            textBoxPrice.Text = "";
            TextBoxWeight.Text = "";
        }

        private void ComboBoxProductAmount_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxProductAmount.Text == "")
            {
                return;
            }
            selectedAmount = Int32.Parse(ComboBoxProductAmount.Text);
        }

        private void ProductDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (ProductDatePicker.SelectedDate == null)
            {
                return;
            }

            string[] dates = (ProductDatePicker.SelectedDate.ToString().Split())[0].Split('.');


            selectedDate = ProductDatePicker.SelectedDate.ToString();
            //MessageBox.Show("day: " + dates[0] + " mont: " + dates[1] + " year: " + dates[2]);

            selectedDate = string.Format("{0}.{1}.{2}",dates[0],dates[1],dates[2]);
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
