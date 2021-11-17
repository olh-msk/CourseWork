using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    #region [Customer Mediator]
    //медіатор між формами і фасадом покупців
    class CustomerMediator
    {
        private static CustomerMediator instance;
        

        private CustomerMediator()
        {

        }
        public static CustomerMediator Instance()
        {
            if(instance == null)
            {
                instance = new CustomerMediator();
            }
            return instance;
        }


    }
    #endregion
}
