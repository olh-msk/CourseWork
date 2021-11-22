using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Shop Mediator]
    //відповідає за виклики функцій і зв'язки між різними класами
    class ShopMediator
    {
        private static ShopMediator instance;

        private ShopMediator()
        {
        }
        public static ShopMediator Instance()
        {
            if (instance == null)
            {
                instance = new ShopMediator();
            }
            return instance;
        }
        //якщо є вже такий продукт, то просто кількість збільшити
        // 1 - М'ясний, 2- Молочний, 3- Предмети для дому
        //Краща логіка добавлання продуктів буду у наступних версіях програми
        public void AdministratorAddProduct(int storageNum, Product prod)
        {
            //якщо вже є такий продукт, просто збільшити кількість
            if (StorageManager.Instance().IfProductExistInStorages(prod.ProductId))
            {
                Product storProd = StorageManager.Instance().GetProductByID(prod.ProductId);
                //якщо є міце на складі
                if (CheckProductMaxAmount(storProd.ProductId))
                {
                    storProd.Amount++;
                }
            }
            //продукт ще не існує, додаємо до вибраного складу
            //в встановлюємо його максимальну кульість на складі
            // 1 - М'ясний, 2- Молочний, 3- Предмети для дому
            else
            {
                if (storageNum == 1)
                {
                    ShopFacade.Instance().StorageManager.MeatStorage.AddProductToStorage(prod);
                    ProductMaxAmounts.Instance().SetProductMaxAmount(prod.ProductId, 20);
                }
                else if (storageNum == 2)
                {
                    ShopFacade.Instance().StorageManager.DairyStorage.AddProductToStorage(prod);
                    ProductMaxAmounts.Instance().SetProductMaxAmount(prod.ProductId, 20);
                }
                else if (storageNum == 3)
                {
                    ShopFacade.Instance().StorageManager.HouseholdStorage.AddProductToStorage(prod);
                    ProductMaxAmounts.Instance().SetProductMaxAmount(prod.ProductId, 20);
                }
            }
        }
        //якщо вже існує, то зменшити кількість
        //якщо кількіст = 0, то видалити з сховищ
        public void AdministratorRemoveProduct(int storageNum, int prodID)
        {
            //якщо такий продукт існує
            if (StorageManager.Instance().IfProductExistInStorages(prodID))
            {
                Product prod = StorageManager.Instance().GetProductByID(prodID);
                //є ще  продукти даного типу
                if (CheckProductMinAmount(prod.ProductId))
                {
                    prod.Amount--;
                }
                //нема більше таких продуктів, забираємо продукт зі складу
                else
                {
                    if (storageNum == 1)
                    {
                        ShopFacade.Instance().StorageManager.MeatStorage.RemoveProductFromStorage(prodID);
                        ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);
                        return;
                    }
                    else if (storageNum == 2)
                    {
                        ShopFacade.Instance().StorageManager.DairyStorage.RemoveProductFromStorage(prodID);
                        ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);
                        return;

                    }
                    else if (storageNum == 3)
                    {
                        ShopFacade.Instance().StorageManager.HouseholdStorage.RemoveProductFromStorage(prodID);
                        ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);
                        return;

                    }
                }
            }
            Product prod1 = StorageManager.Instance().GetProductByID(prodID);
            //нема більше таких продуктів, забираємо продукт зі складу
            if (!CheckProductMinAmount(prod1.ProductId))
            {
                if (storageNum == 1)
                {
                    ShopFacade.Instance().StorageManager.MeatStorage.RemoveProductFromStorage(prodID);
                    ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);
                }
                else if (storageNum == 2)
                {
                    ShopFacade.Instance().StorageManager.DairyStorage.RemoveProductFromStorage(prodID);
                    ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);

                }
                else if (storageNum == 3)
                {
                    ShopFacade.Instance().StorageManager.HouseholdStorage.RemoveProductFromStorage(prodID);
                    ProductMaxAmounts.Instance().RemoveProductMaxAmount(prodID);

                }
            }
        }

        //реалізація методу зміни статусу покупця адміністратором
        //у майбутньому можна зробити перевірку статусу, і робити
        //відповідні дії пов'язані з ним
        public void AdministratorChangeCustomerStatus(int cusID, UserStatus status)
        {
            if(ShopFacade.Instance().CustomerManager.IfCustomerExistInList(cusID))
            {
                CustomerManager.Instance().GetCustomerById(cusID).UserStatus = status;
            }
        }
        
       
        //треба перевірку чи не вийшло за максимальну
        //кількість продутків на складі
        //бо тоді не вийде додати
        //bool значить можна додати, число < max
        public bool CheckProductMaxAmount(int prodID)
        {
            bool res = false;

            //якщо продукт існує
            if(StorageManager.Instance().IfProductExistInStorages(prodID))
            {
                //отримуємо точний продукт
                Product prod = StorageManager.Instance().GetProductByID(prodID);
                //перевіряємо його максимальну кількість
                if (prod.Amount < ProductMaxAmounts.Instance().GetProductMaxAmount(prodID))
                {
                    res = true;

                }
            }
            return res;
        }
        //те саме, коли продукту немає на складі
        //ми не можемо його додати у замовлення або видалити зі складу
        //bool значить можна відняти, число >0
        public bool CheckProductMinAmount(int prodID)
        {
            bool res = false;

            //якщо продукт існує
            if (StorageManager.Instance().IfProductExistInStorages(prodID))
            {
                //отримуємо точний продукт
                Product prod = StorageManager.Instance().GetProductByID(prodID);

                //перевіряємо чи ще лишились продукти на складі
                if (prod.Amount > 0)
                {
                    res = true;

                }
            }

            return res;
        }
        //модератор------------------------
        //створює знижки
        public void ModeratorAddNewCustomerDiscount(int cusID, int interest)
        {
            CustomerDiscount disc = CustomerDiscountManager.Instance().CreateNewCustomerDiscount();
            disc.Interest = interest / 1.0;
            disc.CustomerName = CustomerManager.Instance().GetCustomerLoginById(cusID);
            CustomerDiscountManager.Instance().AddDiscountForCustomer(cusID,disc);
        }
        public void ModeratorAddNewProductDiscount(int prodID, int interest)
        {
            ProductDiscount disc = ProductDiscountManager.Instance().CreateNewProductDiscount();
            disc.Interest = interest / 1.0;
            disc.ProductName = StorageManager.Instance().GetProductNameById(prodID);
            ProductDiscountManager.Instance().AddDiscountForProduct(prodID,disc);
        }
        //отримує список замовлень--
        public List<Order> ModeratorGetCustomerOrderList(int cusID)
        {
            return OrderManager.Instance().GetCustomerOrdersById(cusID);
        }
        //дії по отриманню та вертанню продукта-------------------------------
        //забираєтся продукт з складу
        public void TakeProductFromStorage(int prodID)
        {
            //якщо  є кількість на складі
            if(CheckProductMinAmount(prodID))
            {
                Product prod = StorageManager.Instance().GetProductByID(prodID);
                prod.Amount--;
            }
        }
        //вертається продукт зі складу
        public void SendProductToStorage(int prodID)
        {
            if(CheckProductMaxAmount(prodID))
            {
                StorageManager.Instance().GetProductByID(prodID).Amount++;
            }
        }
        //стоврити нове замовлення-----------------
        public void CartCreateNewOrder(int cusID, Dictionary<int,int> productsInCart)
        {
            Order newOrdr = OrderManager.Instance().CreateNewOrder(productsInCart);
            OrderManager.Instance().AddNewCustomerOrder(cusID, newOrdr);
        }
        //порахувати ціну замовлення зі знижкою------
        public double CalculatePriceWithDiscount(int cusID, Dictionary<int,int> orderedProducts)
        {
            double allPrice = 0;
            //знижка покупця
            CustomerDiscount cus_dis = CustomerDiscountManager.Instance().GetCustomerDiscount(cusID);

            foreach(var pair in orderedProducts)
            {
                //знижка на кожний продукт
                ProductDiscount prod_disc = ProductDiscountManager.Instance().GetProductDiscount(pair.Key);
                allPrice += StorageManager.Instance().GetProductByID(pair.Key).Price * pair.Value;
                allPrice = allPrice - allPrice * prod_disc.Interest;

            }
            //вираховується знижка
            allPrice = allPrice - allPrice * cus_dis.Interest;

            return allPrice;
        }
    }
    #endregion
}
