using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Discount]
    interface IDiscount
    {
        void ChangeInterest(double newInter);
    }
    abstract class Discount : IDiscount
    {
        private static int DiscountNextUniqueNumber = 1;

        public int DiscountId { get; private set; }
        //знижка у відсотках від 0 до 1
        public double Interest { get; set; }

        public Discount()
        {
            this.DiscountId = DiscountNextUniqueNumber++;
            this.Interest = 0.1;
        }

        public void ChangeInterest(double newInter)
        {
            this.Interest = newInter;
        }
    }
    #endregion
}
