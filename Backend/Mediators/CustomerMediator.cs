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
    }
    #endregion
}
