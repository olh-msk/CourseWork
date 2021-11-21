using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class ModeratorMediator
    {
        private static ModeratorMediator instance;

        private ModeratorMediator()
        {

        }
        public static ModeratorMediator Instance()
        {
            if (instance == null)
            {
                instance = new ModeratorMediator();
            }
            return instance;
        }

        public bool IfCorrectLoginPassword(string login, string password)
        {
            return ModeratorManager.Instance().IfCorrectLoginPassword(login, password);
        }
        public int GetAdminIdByLogin(string login)
        {
            return ModeratorManager.Instance().GetModeratorByLogin(login);
        }
        public void CreateNewProductDiscount(int moderatorId, int productID,int interest)
        {
            ModeratorManager.Instance().CreateNewProductDiscount(moderatorId, productID, interest);
        }
        public void CreateNewCustomerDiscount(int moderatorId, int custID,int interest)
        {
            ModeratorManager.Instance().CreateNewCustomerDiscount(moderatorId,custID,interest);
        }
        public void RemoveProductDiscount(int moderatorID, int prodID)
        {
            ModeratorManager.Instance().RemoveProductDiscount(moderatorID, prodID);
        }
        public void RemoveCustDiscount(int moderatorID, int custID)
        {
            ModeratorManager.Instance().RemoveCustDiscount(moderatorID, custID);
        }
        public void ChangeProdDiscountInterest(int prodID, int interest)
        {
            ProductDiscountManager.Instance().GetProductDiscount(prodID).Interest = interest;
        }
        public void ChangeCustDiscountInterest(int custID, int interest)
        {
            CustomerDiscountManager.Instance().GetCustomerDiscount(custID).Interest = interest;
        }
    }
}
