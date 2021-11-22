using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Order Manager]
    interface IOrderManager
    {
        void AddNewCustomerOrder(int cusID, Order order);
        void RemoveCustomerOrder(int cusID, int orderID);
        void RemoveAllCustomerOrders(int cusID);
    }
    interface IOperationIfOrder
    {
        bool IfHasCustomerOrders(int cusID);
        bool IfHasOrder(int orderID);
    }
    interface IOperationGetOrder
    {
        Order GetOrderById(int orderID);
        List<Order> GetCustomerOrdersById(int cusID);
    }
    class OrderManager: IOrderManager,
                        IOperationIfOrder,
                        IOperationGetOrder
    {
        private static OrderManager instance;

        //має id замовника і його замовлення
        Dictionary<int, List<Order>> customerOrders;

        private OrderManager()
        {
            customerOrders = new Dictionary<int, List<Order>>();
        }

        public static OrderManager Instance()
        {
            if(instance == null)
            {
                instance = new OrderManager();
            }
            return instance;
        }

        public void AddNewCustomerOrder(int cusID, Order order)
        {
            //просто добавити
            if(IfHasCustomerOrders(cusID))
            {
                customerOrders[cusID].Add(order);
            }
            //якщо такого замовлення ще не було, то ініціалізувати
            //список і добавити
            else
            {
                customerOrders[cusID] = new List<Order>();
                customerOrders[cusID].Add(order);
            }
        }
        //видалити 1 замовлення з замаовлень покупця
        public void RemoveCustomerOrder(int cusID, int orderID)
        {
            if(IfHasCustomerOrders(cusID))
            {
                if(IfHasOrder(orderID))
                {
                    //якщо список замовлень вже пустий, то видалити користувача
                    //з запису про замовлення
                    if(customerOrders[cusID].Count == 0)
                    {
                        RemoveAllCustomerOrders(cusID);
                        return;
                    }
                    //знаходимо позицію елемента, що треба видалити
                    //і видаляємо
                    for(int i =0; i < customerOrders[cusID].Count;i++)
                    {
                        if(customerOrders[cusID][i].OrderId == orderID)
                        {
                            Order order = customerOrders[cusID][i];
                            //вертає всі продукти на склад
                            //ключ - його id значення - кількість замовлених продуктів
                            foreach (KeyValuePair<int, int> pair in order)
                            {
                                //якщо продукт ще існує
                                if(StorageManager.Instance().IfProductExistInStorages(pair.Key))
                                {
                                    //відправляємо продукти назад у склад
                                    for (int amount = 0; amount < pair.Value; amount++)
                                    {
                                        ShopMediator.Instance().SendProductToStorage(pair.Key);
                                    }
                                }
                                //якщо ні, то
                            }
                            customerOrders[cusID].RemoveAt(i);
                        }
                    }
                }
            }
        }
        //видалити всі замовлення покупця
        public void RemoveAllCustomerOrders(int cusID)
        {
            if(IfHasCustomerOrders(cusID))
            {
                customerOrders.Remove(cusID);
            }
        }

        public bool IfHasCustomerOrders(int cusID)
        {
            return customerOrders.ContainsKey(cusID);
        }

        //перевіряє чи існує таке замовлення
        //по вказаному ід
        public bool IfHasOrder(int orderID)
        {
            bool res = false;
            foreach(KeyValuePair<int, List<Order>> pair in customerOrders)
            {
                foreach(Order order in pair.Value)
                {
                    if(order.OrderId == orderID)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        //отримати контректне замовлення по ід
        public Order GetOrderById(int orderID)
        {
            Order ord = null;
            foreach (KeyValuePair<int, List<Order>> pair in customerOrders)
            {
                foreach (Order order in pair.Value)
                {
                    if (order.OrderId == orderID)
                    {
                        return order;
                    }
                }
            }
            return ord;
        }
        //отримати всі замовлення покупця
        //перед ним має бути перевірка на те, чи існує ключ
        public List<Order> GetCustomerOrdersById(int cusID)
        {
            return customerOrders[cusID];
        }

        //Тут має бути взаємодія з GUI, щоб уточнити інформацію про
        //покупку, буде реалізовно у майбітніх версіях
        public Order CreateNewOrder(Dictionary<int,int> productsInCart)
        {
            Order newOrd = new Order();
            newOrd.SetOrderedProducts(productsInCart);
            return newOrd;
        }

        //прийшов сигнал створити нове замовлення
        public void CustomerCreateNewOrder(int custID, double orderPrice, bool selfDelivery)
        {
            Customer cus = CustomerManager.Instance().GetCustomerById(custID);
            cus.ShoppingCart.CreateOrder(custID);
            List<Order> orders = GetCustomerOrdersById(custID);
            Order order = orders[orders.Count - 1];
            order.SelfDelivery = selfDelivery;
            order.OrderPrice = orderPrice;
        }
    }
    #endregion
}
