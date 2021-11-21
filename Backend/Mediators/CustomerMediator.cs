using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    #region [Customer Mediator]
    //медіатор між формами і фасадом покупців
    class CustomerMediator
    {
        private static CustomerMediator instance;
        
        public CustomerManager CustomerManager { get; set; }

        private CustomerMediator()
        {
            this.CustomerManager = CustomerManager.Instance();
        }
        public static CustomerMediator Instance()
        {
            if(instance == null)
            {
                instance = new CustomerMediator();
            }
            return instance;
        }

        //перенаправляє запит від форми
        //чи правильно ввели логін і пароль
        public bool IfCorrectLoginPassword(string login, string password)
        {
            return this.CustomerManager.IfCorrectLoginPassword(login,password);
        }
        //отримати ід покупця
        public int GetCustomerIdByLogin(string login)
        {
            return this.CustomerManager.GetCustomerIdByLogin(login);
        }
        public bool IfHasSuchLogin(string login)
        {
            return this.CustomerManager.IfHasSuchCustomerLogin(login);
        }
        //створює нового користувача від форми, передає сигнали
        public void CreateNewCustomer(PersonalData customerData)
        {
            Customer cus = CustomerManager.CreateCustomer(1);
            cus.PersonalData = customerData;
            CustomerManager.AddCustomer(cus);
        }
        public Customer GetCustomerById(int cusID)
        {
            return CustomerManager.GetCustomerById(cusID);
        }
        public void ReadCustomersFromTxtFile(string path)
        {
            CustomerManager.Instance().ReadCustomersFromTxtFile(path);
        }
        public void AddProductToCustomerCart(int customerID, int productID, int amount)
        {
            CustomerManager.GetCustomerById(customerID).ShoppingCart.AddProductToCart(productID,amount);
        }

        public void RemoveProductFromCustomerCart(int customerID, int productID, int amount)
        {
            CustomerManager.RemoveProductFromCustomerCart(customerID, productID, amount);
        }

        public double GetPriceWithAllDiscounts(int custID, int prodId)
        {
            return CustomerManager.Instance().GetPriceWithAllDiscounts(custID, prodId);
        }
        public void CustomerCreateNewOrder(int custID, double orderPrice, bool selfDelivery)
        {
            OrderManager.Instance().CustomerCreateNewOrder(custID, orderPrice, selfDelivery);
        }
        public void CustomerRemoveOrder(int customerID, int orderID)
        {
            //вертаємо гроші покопцю
            CustomerManager.Instance().GetCustomerMoneyBack(customerID, orderID);
            OrderManager.Instance().RemoveCustomerOrder(customerID,orderID);
        }
    }
    #endregion
}
