using System;
using System.Collections.Generic;

namespace CourseWork
{
    #region [Shopping Cart]
    class ShoppingCart
    {
        //має продукт і його кількість
        Dictionary<int, int> productsInCart;

        public ShoppingCart()
        {
            productsInCart = new Dictionary<int, int>();
        }

        //додати продукт у корзину на вказану кількість, виклик 
        //буде оброблятись у медіаторі
        public void AddProductToCart(int prodID, int amount = 1)
        {
            for(int i =0; i<amount;i++)
            {
                //перевірка чи продукт є на складі
                if(ShopMediator.Instance().CheckProductMinAmount(prodID))
                {
                    //перерірка чи ми ще не маємо такого продутку у корзині
                    if (!IfCartHaveProduct(prodID))
                    {
                        //беремо 1 штуку
                        ShopMediator.Instance().TakeProductFromStorage(prodID);
                        productsInCart[prodID] = 1;
                    }
                    //інаше нарощуємо кількість взяти продуктів
                    else
                    {
                        ShopMediator.Instance().TakeProductFromStorage(prodID);
                        productsInCart[prodID]++;
                    }
                }
            }
        }
        public void RemoveProductFormCart(int prodID, int amount = 1)
        {
            if (IfCartHaveProduct(prodID))
            {
                for (int i = 0; i < amount; i++)
                {
                    //перевірка чи продукт є на складі
                    if (ShopMediator.Instance().CheckProductMaxAmount(prodID))
                    {
                        
                        //якщо вже не лишилось продуктів, то видалити
                        if (productsInCart[prodID]==0)
                        {
                            DeleteProductFromCart(prodID);
                            return;
                        }
                        //інаше вертаємо продукт на склад
                        else
                        {
                            ShopMediator.Instance().SendProductToStorage(prodID);
                            productsInCart[prodID] -= 1;
                        }
                    }
                }
                //якщо вже не лишилось продуктів, то видалити
                if (productsInCart[prodID] == 0)
                {
                    DeleteProductFromCart(prodID);
                }
            }
        }

        //виклик створення замовлення переноситься на медіатор----
        //приймає ід покупця змінну, чи самодоставка чи ні
        //маємо підрахувати грші зі знижкою і відніяти їх від покупця
        public void CreateOrder(int cusID)
        {
            //даємо туди копію списку продуктів
            ShopMediator.Instance().CartCreateNewOrder(cusID, new Dictionary<int, int>(this.productsInCart));
            //очищуємо корзину, бо ми вже маємо замовлення
            //щоб покупець вибрав нові продукти
            productsInCart.Clear();
        }
        //перерівяє чи є продукт у корзині
        public bool IfCartHaveProduct(int prodID)
        {
            return productsInCart.ContainsKey(prodID);
        }
        //вертає кількість різних продуктів у корзині
        public int GetCount()
        {
            return productsInCart.Count;
        }

        public void DeleteProductFromCart(int prodID)
        {
            productsInCart.Remove(prodID);
        }

        public int GetProductAmountById(int prodID)
        {
            return productsInCart[prodID];
        }

        public IEnumerator<KeyValuePair<int,int>> GetEnumerator()
        {
            return productsInCart.GetEnumerator();
        }

    }
    #endregion
}
