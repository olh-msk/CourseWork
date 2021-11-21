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

namespace CourseWork.Frontend.UserCart 
{
    public partial class CustomerCreateOrder : Window
    {
        int custID;
        bool selfDelivery;
        double orderPrice;
        public CustomerCreateOrder()
        {
            InitializeComponent();
        }
        public CustomerCreateOrder(int id):this()
        {
            custID = id;
            orderPrice = 0;
            selfDelivery = false;
            UpdatePrice();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            Customer cus = CustomerManager.Instance().GetCustomerById(custID);

            if(cus.PersonalData.Money < orderPrice)
            {
                MessageBox.Show("You don't have enough money",
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                return;
            }
            //надсилаємо сигнал про заключення замовлення
            CustomerMediator.Instance().CustomerCreateNewOrder(custID, orderPrice,selfDelivery);
            //зчитуємо кошти
            cus.PersonalData.Money -= orderPrice;
            this.Close();

        }
        private void UpdatePrice()
        {
            orderPrice = 0;
            Customer cus = CustomerManager.Instance().GetCustomerById(custID);
            foreach (var pair in cus.ShoppingCart)
            {
                Product prod = StorageMediator.Instance().GetProductById(pair.Key);
                //очищення і втсановлення кількості, що має користувач
                prod.AmountCustomerHas = 0;
                prod.AmountCustomerHas = cus.ShoppingCart.GetProductAmountById(prod.ProductId);

                double discount = CustomerMediator.Instance().GetPriceWithAllDiscounts(custID, prod.ProductId);

                prod.PriceWithDiscounts = prod.Price - discount;
                prod.TotalPriceForCustomer = prod.PriceWithDiscounts * prod.AmountCustomerHas;

                orderPrice += prod.TotalPriceForCustomer;
            }
            
            //доставка коштує 5% від загальної ціни
            //і безплатно для VIP
            if(selfDelivery == true)
            {
                if(!(cus.GetStatusString() == "VIP"))
                {
                    orderPrice += orderPrice * 0.05;
                }
            }

            ForTotalPrice.Text = string.Format("{0:f2}$",orderPrice);
            ForMoney.Text = string.Format("{0:f2}$",cus.PersonalData.Money);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBoxDelivery_Checked(object sender, RoutedEventArgs e)
        {
            selfDelivery = true;
            UpdatePrice();
        }

        private void CheckBoxDelivery_Unchecked(object sender, RoutedEventArgs e)
        {
            selfDelivery = false;
            UpdatePrice();
        }
    }
}
