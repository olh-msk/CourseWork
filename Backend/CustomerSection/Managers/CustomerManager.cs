using System;
using System.Collections.Generic;
using System.IO;

namespace CourseWork
{
    interface IOperationAddRemoveCustomer
    {
        void AddCustomer(Customer cust);
        void RemoveCustomer(int cusID);
    }

    class CustomerManager : IOperationAddRemoveCustomer
    {
        private static CustomerManager instance;

        List<Customer> customers;

        private CustomerManager()
        {
            customers = new List<Customer>();
        }
        public static CustomerManager Instance()
        {
            if(instance == null)
            {
                instance = new CustomerManager();
            }
            return instance;
        }

        //перевіряє чи є покупець по ID
        public bool IfCustomerExistInList(int cusID)
        {
            bool res = false;
            foreach(Customer cust in customers)
            {
                if(cust.CustomerId == cusID)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        //перед цим ми будемо викликати чи є покупець у списку
        public Customer GetCustomerById(int cusID)
        {
            foreach(Customer cust in customers)
            {
                if(cust.CustomerId == cusID)
                {
                    return cust;
                }
            }
            return null;
        }
        //отримати Ід по імені
        public int GetCustomerIdByLogin(string login)
        {
            int res = -1;
            foreach(Customer cus in customers)
            {
                if(cus.PersonalData.Login == login)
                {
                    res = cus.CustomerId;
                    break;
                }
            }
            return res;
        }
        //пеервіряє чи є вже такий покупець з таким ім'ям
        public bool IfHasSuchCustomerLogin(string name)
        {
            bool res = false;
            foreach (Customer cus in customers)
            {
                if (cus.PersonalData.Login == name)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        //чи правильно ввели логі ні пароль
        public bool IfCorrectLoginPassword(string login, string password)
        {
            bool res = false;
            foreach(Customer cus in customers)
            {
                if(cus.PersonalData.Login == login &&
                    cus.PersonalData.Password == password)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        //додати покупця
        public void AddCustomer(Customer cust)
        {
            if(!IfCustomerExistInList(cust.CustomerId))
            {
                this.customers.Add(cust);
            }
        }
        //видалити покупця по ID
        public void RemoveCustomer(int cusID)
        {
            for(int i =0; i < customers.Count;i++)
            {
                if(customers[i].CustomerId == cusID)
                {
                    customers.RemoveAt(i);
                }
            }
        }

        //Метод створити покупця
        //краща реалізація буде у майбутних версіях
        //1 - VIP 2 - звичайний
        public Customer CreateCustomer(int cusType)
        {
            Customer cust = null;
            if(cusType == 1)
            {
                cust = new VIPCustomer();
            }
            else if(cusType == 2)
            {
                cust = new SimpleCustomer();
            }

            return cust;
        }

        //видалити продукт з корзини
        public void RemoveProductFromCustomerCart(int customerID, int productID, int amount)
        {
            Customer cus = CustomerMediator.Instance().GetCustomerById(customerID);
            //віднімання з корзини автоматично вертає продукт на склад
            cus.ShoppingCart.RemoveProductFormCart(productID, amount);
            Product prod = StorageMediator.Instance().GetProductById(productID);
            prod.AmountCustomerHas -= amount;
        }

        //метод чисто для показових цілей, вся інформацію буде зберігатись
        //у базах даних
        public void ReadCustomersFromTxtFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int cusType;

                string line;
                //поки не зчитаються всі дані
                while((line = reader.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split();
                    cusType = Int32.Parse(lineSplit[9]);


                    Customer cus = CreateCustomer(cusType);

                    cus.PersonalData.Login = lineSplit[0];
                    cus.PersonalData.Password = lineSplit[1];
                    cus.PersonalData.Email = lineSplit[2];
                    cus.PersonalData.Age = Int32.Parse(lineSplit[3]);
                    cus.PersonalData.PnoneNumber = lineSplit[4];
                    cus.PersonalData.Address.Street = lineSplit[5];
                    cus.PersonalData.Address.City = lineSplit[6];
                    cus.PersonalData.Address.Country = lineSplit[7];
                    cus.PersonalData.Address.Zipcode = Int32.Parse(lineSplit[8]);

                    AddCustomer(cus);
                }
            }
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            return customers.GetEnumerator();
        }

        public string GetCustomerLoginById(int cusID)
        {
            string res = "N/A";
            foreach(Customer cus in customers)
            {
                if(cus.CustomerId == cusID)
                {
                    res = cus.PersonalData.Login;
                    break;
                }
            }
            return res;
        }
    }
}
