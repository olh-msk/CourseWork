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
        void CreateCustomerDiscount(int cusID);
        void CreateProductDiscount(int prodID);
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
        public void CreateCustomerDiscount(int cusID)
        {
            ShopMediator.Instance().ModeratorAddNewCustomerDiscount(cusID);
        }

        public void CreateProductDiscount(int prodID)
        {
            ShopMediator.Instance().ModeratorAddNewProductDiscount(prodID);
        }

        public List<Order> GetCustomerOrders(int cusID)
        {
            return ShopMediator.Instance().ModeratorGetCustomerOrderList(cusID);
        }
    }
    #endregion
}
