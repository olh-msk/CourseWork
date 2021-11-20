using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Products Max Amounts]
    //зберігає максимальну кількість продуктів
    class ProductMaxAmounts
    {
        private static ProductMaxAmounts instance;

        //збрігає id продукту і його максимальну кількість
        Dictionary<int, int> listOfProductMaxAmounts;

        private ProductMaxAmounts()
        {
            listOfProductMaxAmounts = new Dictionary<int, int>();
        }

        public static ProductMaxAmounts Instance()
        {
            if(instance == null)
            {
                instance = new ProductMaxAmounts();
            }
            return instance;
        }
        //встановлює макс кількість-----
        public void SetProductMaxAmount(int prodID, int maxAmount)
        {
            listOfProductMaxAmounts[prodID] = maxAmount;
        }
        //вертає макс кількість залежно від ід продукту----
        public int GetProductMaxAmount(int prodID)
        {
            int res = 0;
            if(listOfProductMaxAmounts.ContainsKey(prodID))
            {
                res = listOfProductMaxAmounts[prodID];
            }
            //якщо нема ід тоді 0
            return res;
        }
        public void RemoveProductMaxAmount(int prodID)
        {
            listOfProductMaxAmounts.Remove(prodID);
        }
    }
    #endregion
}
