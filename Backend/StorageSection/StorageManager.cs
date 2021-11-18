using System;
using System.Collections.Generic;
using System.IO;
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

        //чисто у демонстративних цілях
        //1 - м'ясо, 2- молочний, 3-хатні
        //додати у сховище
        public void AddToStorage(int storType, Product prod)
        {
            if(storType == 1)
            {
                MeatStorage.AddProductToStorage(prod);
            }
            else if(storType == 2)
            {
                DairyStorage.AddProductToStorage(prod);
            }
            else
            {
                HouseholdStorage.AddProductToStorage(prod);
            }
        }

        //отримати повнйи список продуктів
        public List<Product> GetAllProductsList()
        {
            List<Product> prodList = new List<Product>();
            foreach(Product prod in this.MeatStorage)
            {
                prodList.Add(prod);
            
            
            }
            return prodList;
        }

        //Чисто у демонстративних цілях є читата з TXT файлу
        //самі продукти будуть у базах даних
        public void ReadProductsFromTextFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string prodType;

                string line;
                //поки не зчитаються всі дані
                while ((line = reader.ReadLine()) != null)
                {
                    Product product = null;
                    string[] lineSplit = line.Split();
                    prodType = lineSplit[0];

                    //який є тип продукту
                    if(prodType == "Dairy")
                    {
                        product = StorageManager.Instance().ProductFactory.CreateDairyProduct();
                    }
                    else if(prodType == "Meat")
                    {
                        product = StorageManager.Instance().ProductFactory.CreateMeatProduct();
                    }
                    else
                    {
                        product  = StorageManager.Instance().ProductFactory.CreateHouseholdProduct();
                    }

                    product.Name = lineSplit[1];
                    product.Weight = Double.Parse(lineSplit[2]);
                    product.Price = Double.Parse(lineSplit[3]);
                    product.Amount = Int32.Parse(lineSplit[4]);

                    string[] dateSplit = lineSplit[5].Split(':');

                    int day = Int32.Parse(dateSplit[0]);
                    int month = Int32.Parse(dateSplit[1]);
                    int year = Int32.Parse(dateSplit[2]);

                    DateTime exDate = new DateTime(year, month, day);

                    product.ExpirationDate = exDate;

                    //це тестовий варіант лише для демонстрції функціоналу
                    if (prodType == "Dairy")
                    {
                        AddToStorage(2, product);
                    }
                    else if (prodType == "Meat")
                    {
                        AddToStorage(1,product);
                    }
                    else
                    {
                        AddToStorage(3, product);
                    }
                }
            }
        }
    }
    #endregion
}
