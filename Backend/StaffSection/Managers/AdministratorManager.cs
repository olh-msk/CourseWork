using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CourseWork
{
    #region[Administrator Manager]
    interface IAdministratorManager
    {
        void AddAdministrator(Administrator admin);
        void RemoveAdministrator(int adminID);
    }
    interface IOperationIfAdministrator
    {
        bool IfCorrectLoginPassword(string login, string password);
        bool IfAdministratorExistInList(int adminID);
    }
    interface IOperationGetAdministrator
    {
        Administrator GetAdministratorById(int adminID);
        int GetAdministratorIdByLogin(string login);
    }
    //відповідає за керування адміністраторами
    class AdministratorManager : IAdministratorManager,
                                 IOperationIfAdministrator,
                                 IOperationGetAdministrator
    {
        private static AdministratorManager instance;

        List<Administrator> administrators;


        private AdministratorManager()
        {
            administrators = new List<Administrator>();
        }

        public static AdministratorManager Instance()
        {
            if(instance == null)
            {
                instance = new AdministratorManager();
            }
            return instance;
        }

        //чи є адміністратор у списку-----
        public bool IfAdministratorExistInList(int adminID)
        {
            bool res = false;
            foreach(Administrator admin in administrators)
            {
                if(admin.AdministratorId == adminID)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        //отримати адміністратора по ID
        public Administrator GetAdministratorById(int adminID)
        {
            if(IfAdministratorExistInList(adminID))
            {
                foreach (Administrator admin in administrators)
                {
                    if (admin.AdministratorId == adminID)
                    {
                        return admin;
                    }
                }
            }
            return null;
        }

        //додати адміністратора
        //якщо його ще нема у списку
        public void AddAdministrator(Administrator admin)
        {
            if(!IfAdministratorExistInList(admin.AdministratorId))
            {
                administrators.Add(admin);
            }
        }
        //видаляє адміна по ід, якщо він є у списку
        public void RemoveAdministrator(int adminID)
        {
            if(IfAdministratorExistInList(adminID))
            {
                for(int i =0; i < administrators.Count;i++)
                {
                    if(administrators[i].AdministratorId == adminID)
                    {
                        administrators.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        //сторити модератора
        //буде взаємодіяти з GUI, щоб отримати дані
        public Administrator CreateNewAdministrator()
        {
            //будуть отримуватись дані для адміністратора і по ним його створять
            //.....
            return new Administrator();
        }

        //читання з файлу використано чисто у демонстративних цілях
        public void ReadAdminsFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine())!=null)
                {
                    string[] lineSplit = line.Split();

                    Administrator admin = CreateNewAdministrator();

                    admin.Login = lineSplit[0];
                    admin.Password = lineSplit[1];
                    admin.PhoneNumber = lineSplit[2];

                    AddAdministrator(admin);
                }
            }    
        }

        public bool IfCorrectLoginPassword(string login, string password)
        {
            bool res = false;
            foreach (Administrator admin in administrators)
            {
                if (admin.Login == login &&
                    admin.Password == password)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }

        public int GetAdministratorIdByLogin(string login)
        {
            int res = -1;
            foreach (Administrator admin in administrators)
            {
                if (admin.Login == login)
                {
                    res = admin.AdministratorId;
                    break;
                }
            }
            return res;
        }

        //тут реалізовано додавання продукту з форми-------------
        public void AdministratorAddNewProduct(int adminID, string name, double price, double weight,
                                               int selectedAmount, string selectedType, string selectedDate)
        {
            int storageType = 0;
            //створення і корегування продукту
            Product prod;

            if (selectedType == "Meat")
            {
                prod = StorageManager.Instance().ProductFactory.CreateMeatProduct();
                storageType = 1;
            }
            else if (selectedType == "Dairy")
            {
                prod = StorageManager.Instance().ProductFactory.CreateDairyProduct();
                storageType = 2;
            }
            else
            {
                prod = StorageManager.Instance().ProductFactory.CreateHouseholdProduct();
                storageType = 3;
            }
            //переводимо дату
            string[] date = selectedDate.Split('.');
            int day = Int32.Parse(date[0]);
            int month = Int32.Parse(date[1]);
            int year = Int32.Parse(date[2]);

            //оновлюємо значення продукту
            prod.Amount = selectedAmount;
            prod.Name = name;
            prod.Price = price;
            prod.Weight = weight;
            prod.ExpirationDate = new DateTime(year, month, day);

            GetAdministratorById(adminID).AddProduct(storageType, prod, selectedAmount);
        }
        public void RemoveHoleProduct(int admidistratorID, int productID, string selectedType)
        {
            int storageType = 0;
            if (selectedType == "Meat")
            {
                storageType = 1;
            }
            else if (selectedType == "Dairy")
            {
                storageType = 2;
            }
            else
            {
                storageType = 3;
            }

            int amount = StorageManager.Instance().GetProductByID(productID).Amount;
            GetAdministratorById(admidistratorID).RemoveProduct(storageType,productID, amount);
        }
    }
    #endregion
}
