using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Moderator]
    interface IOperationModeratorGetCustomerOrderList
    {
        List<Order> GetCustomerOrders(int cusID);
    }
    interface IOperationModeratorCreateDiscounts
    {
        void CreateCustomerDiscount(int cusID, int interest);
        void CreateProductDiscount(int prodID, int interest);
    }

    class Moderator : Employee, IOperationModeratorCreateDiscounts,
                                IOperationModeratorGetCustomerOrderList
    {
        //відповідає за id створенних модераторів
        private static int moderatorNextUniqueId = 1;

        public int ModeratorId { get; private set; }

        public Moderator():base()
        {
            this.ModeratorId = moderatorNextUniqueId++;
        }
        //створити знижки передаємо на посередника
        public void CreateCustomerDiscount(int cusID, int interest = 10)
        {
            ShopMediator.Instance().ModeratorAddNewCustomerDiscount(cusID, interest);
        }

        public void CreateProductDiscount(int prodID, int interest = 15)
        {
            ShopMediator.Instance().ModeratorAddNewProductDiscount(prodID, interest);
        }

        public List<Order> GetCustomerOrders(int cusID)
        {
            return ShopMediator.Instance().ModeratorGetCustomerOrderList(cusID);
        }
    }
    #endregion
}
