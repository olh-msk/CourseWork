using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    class ShopFacade
    {
        private static ShopFacade instance;

        public StorageManager StorageManager { get; private set; }
        public CustomerManager CustomerManager { get; private set; }
        public StaffManager StaffManager { get; private set; }
        public OrderManager OrderManager { get; private set; }

        private ShopFacade()
        {
            this.StorageManager = StorageManager.Instance();
            this.CustomerManager = CustomerManager.Instance();
            this.StaffManager = StaffManager.Instance();
            this.OrderManager = OrderManager.Instance();
        }

        public static ShopFacade Instance()
        {
            if(instance == null)
            {
                instance = new ShopFacade();
            }
            return instance;
        }

    }
}
