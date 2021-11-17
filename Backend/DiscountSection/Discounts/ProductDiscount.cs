using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class ProductDiscount: Discount
    {

        public ProductDiscount():base()
        {
            this.Interest = 0.15;
        }
    }
}
