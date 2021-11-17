using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Storage Manager]
    class StorageManager
    {
        private static StorageManager instance;

        public AbstractProductFactory ProductFactory { get; set; }
        
        public MeatStorage MeatStorage { get; private set; }
        public DairyStorage DairyStorage { get; private set; }
        public HouseholdStorage HouseholdStorage { get; private set; }

        private StorageManager()
        {
            this.MeatStorage = new MeatStorage();
            this.HouseholdStorage = new HouseholdStorage();
            this.DairyStorage = new DairyStorage();
            this.ProductFactory = new UkrainianProductFactory();
        }

        public static StorageManager Instance()
        {
            if(instance == null)
            {
                instance = new StorageManager();
            }
            return instance;
        }

        //Пеервіряє чи взагалі існує продукт у якось зі сховищ
        //реалізація явно некоректна, буде виправленно у майбітніх версіях
        //щоб не ускладнювати прототип програми
        public bool IfProductExistInStorages(int prodID)
        {
            bool res = false;

            foreach (MeatProduct prod in this.MeatStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = true;
                    return res;
                }
            }
            foreach (DairyProduct prod in this.DairyStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = true;
                    return res;
                }
            }
            foreach (HouseholdProduct prod in this.HouseholdStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = true;
                    return res;
                }
            }
            return res;
        }
        //перед викликом цієї функції обов'язково має бути виклик
        //перевірки існування продукту взагалі
        //можна реалізувати всю цю логіку при додванні/відміманні елементу
        //через ланцюг обов'язків
        public Product GetProductByID(int prodID)
        {
            Product res = null;

            foreach (MeatProduct prod in MeatStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = prod;
                    return res;
                }
            }
            foreach (DairyProduct prod in DairyStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = prod;
                    return res;
                }
            }
            foreach (HouseholdProduct prod in HouseholdStorage)
            {
                if (prod.ProductId == prodID)
                {
                    res = prod;
                    return res;
                }
            }
            return res;
        }
    }
    #endregion
}
