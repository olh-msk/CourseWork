using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class CustomerDiscount: Discount
    {
        public string CustomerName { get; set; }
        public CustomerDiscount():base()
        {
            this.Interest = 0.20;
            this.CustomerName = "N/A";
        }
    }
}
