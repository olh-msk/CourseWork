using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [For Customer Order]
    enum OrderSatus { New, InProcess, InRoad, Delivered, Canceled}
    
    interface IOperationOrderAddRemoveProduct
    {
        void AddProduct(int prodID);
        void Removeproduct(int prodID);
    }
    class Order
    {
        //відповідає за id створенних 
        private static int orderNextUniqueId = 1;

        //записуються ID замовлених продуктів і їх кількість
        Dictionary<int,int> orderedProducts;

        public int OrderId { get; private set; }

        public DateTime OrderDate { get; set; }
        public string OrderDateString { get; set; }

        public bool SelfDelivery { get; set; }

        public OrderSatus OrderSatus { get; set; }


        public Order()
        {
            this.OrderId = orderNextUniqueId++;

            this.OrderDate = DateTime.Now;
            OrderDateString = OrderDate.ToShortDateString();

            this.SelfDelivery = false;

            this.OrderSatus = OrderSatus.New;

            this.orderedProducts = new Dictionary<int, int>();
        }

        //тут у майбутньому можна реалізувати відрахування коштів покупцю
        //замовлені продукти
        public void CancelOrder()
        {
            this.OrderSatus = OrderSatus.Canceled;
        }

        public void UpdateOrderStatus(OrderSatus stat)
        {
            this.OrderSatus = stat;
        }


        //дає загальну ціну разом з знижками
        //через медіатор
        public double GetTotalProductPrice()
        {
            double allPrice = 0;
            foreach(var pair in orderedProducts)
            {
                allPrice += ShopMediator.Instance().GetProductByID(pair.Key).Price * pair.Value;
            }
            return allPrice;
        }

        public double GetTotalPriceWithDicounts(int cusID)
        {
            return ShopMediator.Instance().CalculatePriceWithDiscount(cusID,orderedProducts);
        }

        //якщо покумець вирішить змінити замовлення
        //додати продукт у замовлення
        //сигнал приймається з медіатора
        public void AddProduct(int prodID, int amount = 1)
        {
            // Якщо ще є продукти на складі
            if(ShopMediator.Instance().CheckProductMinAmount(prodID))
            {
                ShopMediator.Instance().TakeProductFromStorage(prodID);
                for(int i =0; i < amount; i++)
                {
                    if (ifHasProductInOrder(prodID))
                    {
                        orderedProducts[prodID]++;
                    }
                    //продукту ще нема, тому створити
                    else
                    {
                        orderedProducts[prodID] = 0;
                        orderedProducts[prodID]++;
                    }   
                }
            }
        }

        //видалити зі замовлення продукт
        public void Removeproduct(int prodID, int amount = 1)
        {
            //якщо нема місця на складі, то буде наднормаю
            //у майбутньому можна реалізувати знижку на цей продукт
            //щоб покупець передумав його викидати
            // Якщо ще є продукти на складі
            if (ShopMediator.Instance().CheckProductMaxAmount(prodID))
            {
                
                for (int i = 0; i < amount; i++)
                {
                    if (ifHasProductInOrder(prodID))
                    {
                        //якщо кількість цього продукту вже рівна 0,
                        //то видалити його
                        if(orderedProducts[prodID] == 0)
                        {
                            RemoveHoleProduct(prodID);
                        }
                        else
                        {
                            ShopMediator.Instance().SendProductToStorage(prodID);
                            orderedProducts[prodID]--;
                        }
                        
                    }
                    //неможливо забрати те, чого нема
                    //продукту ще нема, нічого не робити
                }
            }
        }
        //чи є продукт у замовленні
        public bool ifHasProductInOrder(int prodID)
        {
            return orderedProducts.ContainsKey(prodID);
        }

        //видалє цілий ключ і його значення
        public void RemoveHoleProduct(int prodID)
        {
            orderedProducts.Remove(prodID);
        }

        //встановити спосок покупок
        public void SetOrderedProducts(Dictionary<int,int> _orderedProducts)
        {
            this.orderedProducts = _orderedProducts;
        }

        public override string ToString()
        {
            string res = "";

            foreach(var pair in orderedProducts)
            {
                res += string.Format("Prod ID {0}\t amount: {1}\n",pair.Key, pair.Value);
            }
            return res;
        }
    }
    #endregion
}
