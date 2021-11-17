using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class VIPCustomer: Customer
    {

        public VIPCustomer():base()
        {
            this.UserStatus = UserStatus.VIP;
        }
    }
}
