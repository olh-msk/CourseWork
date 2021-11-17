using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Storages]
    //=========================
    interface IStorage
    {
        void AddProductToStorage(Product prod);

        void RemoveProductFromStorage(int prodID);

        IEnumerator<Product> GetEnumerator();
    }
    //===================================
    class MeatStorage : IStorage
    {
        private List<MeatProduct> products;

        //-------------
        public MeatStorage()
        {
            products = new List<MeatProduct>();
        }
        //-------------
        public void AddProductToStorage(Product prod)
        {
            MeatProduct _prod = prod as MeatProduct;
            _prod.Amount++;
            products.Add(_prod);
        }
        //----------------
        public void RemoveProductFromStorage(int prodID)
        {
            for(int i =0; i<products.Count;i++)
            {
                if(products[i].ProductId==prodID)
                {
                    products.RemoveAt(i);
                }
            }
        }
        //------------
        public IEnumerator<Product> GetEnumerator()
        {
            return products.GetEnumerator();
        }
        //індексатор-----------------
        public MeatProduct this[int index]
        {
            get
            {
                try
                {
                    return products[index];
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                    throw;
                }
            }
            set
            {
                try
                {
                    products[index] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                }
            }
        }
    }
    //=======================
    class HouseholdStorage : IStorage
    {
        private List<HouseholdProduct> products;

        public HouseholdStorage()
        {
            products = new List<HouseholdProduct>();
        }
        public void AddProductToStorage(Product prod)
        {
            HouseholdProduct _prod = prod as HouseholdProduct;
            _prod.Amount++;
            products.Add(_prod);
        }
        //-------------
        public void RemoveProductFromStorage(int prodID)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ProductId == prodID)
                {
                    products.RemoveAt(i);
                }
            }
        }
        //----------------
        public IEnumerator<Product> GetEnumerator()
        {
            return products.GetEnumerator();
        }
        //індексатор-----------------
        public HouseholdProduct this[int index]
        {
            get
            {
                try
                {
                    return products[index];
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                    throw;
                }
            }
            set
            {
                try
                {
                    products[index] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                }
            }
        }

    }
    //==================
    class DairyStorage : IStorage
    {
        List<DairyProduct> products;
        //--------------
        public DairyStorage()
        {
            products = new List<DairyProduct>();
        }
        //---------------
        public void AddProductToStorage(Product prod)
        {
            DairyProduct _prod = prod as DairyProduct;
            _prod.Amount++;
            products.Add(_prod);
        }
        //------------
        public void RemoveProductFromStorage(int prodID)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ProductId == prodID)
                {
                    products.RemoveAt(i);
                }
            }
        }
        //------------
        public IEnumerator<Product> GetEnumerator()
        {
            return products.GetEnumerator();
        }

        //індексатор-----------------
        public DairyProduct this[int index]
        {
            get
            {
                try
                {
                    return products[index];
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                    throw;
                }
            }
            set
            {
                try
                {
                    products[index] = value;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Wrong index");
                }
            }
        }
    }
    #endregion
}
