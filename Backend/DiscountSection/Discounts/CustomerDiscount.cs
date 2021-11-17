using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class CustomerDiscount: Discount
    {
        public CustomerDiscount():base()
        {
            this.Interest = 0.20;
        }
    }
}
