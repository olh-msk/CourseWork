using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class ProductDiscount: Discount
    {
        public string ProductName { get; set; }
        public ProductDiscount():base()
        {
            this.Interest = 0.15;
            this.ProductName = "N/A";
        }
    }
}
