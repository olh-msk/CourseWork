using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Customer Discount Manager]

    interface IOperationSetGetCustomerDiscount
    {
        CustomerDiscount GetCustomerDiscount(int cusID);
        void SetDiscountForCustomer(int cusID, CustomerDiscount dis);
    }
    class CustomerDiscountManager:IOperationSetGetCustomerDiscount
    {
        private static CustomerDiscountManager instance;
        //зберігає ід покупця і його знижку, якщо вона є
        Dictionary<int, CustomerDiscount> customerDiscounts;

        private CustomerDiscountManager()
        {
            customerDiscounts = new Dictionary<int, CustomerDiscount>(); 
        }
        public static CustomerDiscountManager Instance()
        {
            if(instance == null)
            {
                instance = new CustomerDiscountManager();
            }
            return instance;
        }

        public CustomerDiscount CreateNewCustomerDiscount()
        {
            return new CustomerDiscount();
        }

        //встановити знижку для покупця
        public void SetDiscountForCustomer(int cusID, CustomerDiscount dis)
        {
            customerDiscounts[cusID] = dis;
        }

        //видалити знижку----
        public void RemoveDiscountFromCustomer(int cusID)
        {
            customerDiscounts.Remove(cusID);
        }
        //отримати знижку по ід-----
        public CustomerDiscount GetCustomerDiscount(int cusID)
        {
            if(customerDiscounts.ContainsKey(cusID))
            {
                return customerDiscounts[cusID];
            }
            return null;
        }
        //чи має покупець знижку
        public bool IfCustomerHasDiscount(int cusID)
        {
            return customerDiscounts.ContainsKey(cusID);
        }

        public IEnumerator<KeyValuePair<int, CustomerDiscount>> GetEnumerator()
        {
            return customerDiscounts.GetEnumerator();
        }
    }
    #endregion
}
