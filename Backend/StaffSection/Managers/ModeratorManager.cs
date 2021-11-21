using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CourseWork
{
    #region[Moderator Manager]
    interface IOperationAddRemoveModerator
    {
        void AddModerator(Moderator moder);
        void RemoveModerator(int moderID);
    }
    //відповідає за керування адміністраторами
    class ModeratorManager : IOperationAddRemoveModerator
    {
        private static ModeratorManager instance;

        List<Moderator> moderators;


        private ModeratorManager()
        {
            moderators = new List<Moderator>();
        }
        // для сингелтону
        public static ModeratorManager Instance()
        {
            if (instance == null)
            {
                instance = new ModeratorManager();
            }
            return instance;
        }

        //чи є адміністратор у списку-----
        public bool IfModeratorExistInList(int moderID)
        {
            bool res = false;
            foreach (Moderator moder in moderators)
            {
                if (moder.ModeratorId == moderID)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        //отримати адміністратора по ID
        public Moderator GetModeratorById(int moderID)
        {
            if (IfModeratorExistInList(moderID))
            {
                foreach (Moderator moder in moderators)
                {
                    if (moder.ModeratorId == moderID)
                    {
                        return moder;
                    }
                }
            }
            return null;
        }

        //додати адміністратора
        //якщо його ще нема у списку
        public void AddModerator(Moderator moder)
        {
            if (!IfModeratorExistInList(moder.ModeratorId))
            {
                moderators.Add(moder);
            }
        }
        //видаляє адміна по ід, якщо він є у списку
        public void RemoveModerator(int moderID)
        {
            if (IfModeratorExistInList(moderID))
            {
                for (int i = 0; i < moderators.Count; i++)
                {
                    if (moderators[i].ModeratorId == moderID)
                    {
                        moderators.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        //створити модератора
        //буде взаємодіяти з GUI, щоб отримати дані
        public Moderator CreateNewModerator()
        {
            //будуть отримуватись дані для модератора і по ним його створять
            //.....
            return new Moderator();
        }

        //читання з файлу чисто у демонтративних цілях
        public void ReadModersFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split();

                    Moderator moder = CreateNewModerator();

                    moder.Login = lineSplit[0];
                    moder.Password = lineSplit[1];
                    moder.PhoneNumber = lineSplit[2];

                    AddModerator(moder);
                }
            }
        }

        public bool IfCorrectLoginPassword(string login, string password)
        {
            bool res = false;
            foreach (Moderator moderator in moderators)
            {
                if (moderator.Login == login &&
                    moderator.Password == password)
                {
                    res = true;
                    break;
                }
            }
            return res;
        }
        public int GetModeratorByLogin(string login)
        {
            int res = -1;
            foreach (Moderator moder in moderators)
            {
                if (moder.Login == login)
                {
                    res = moder.ModeratorId;
                    break;
                }
            }
            return res;
        }
        public void CreateNewProductDiscount(int moderatorId, int productID, int interest)
        {
            Moderator moder = GetModeratorById(moderatorId);
            moder.CreateProductDiscount(productID, interest);
        }
        public void CreateNewCustomerDiscount(int moderatorId, int custID,int interest)
        {
            Moderator moder = GetModeratorById(moderatorId);
            moder.CreateCustomerDiscount(custID,interest);
        }
        public void RemoveProductDiscount(int moderatorID, int prodID)
        {
            ProductDiscountManager.Instance().RemoveDiscountFromProduct(prodID);
        }
        public void RemoveCustDiscount(int moderatorID, int custID)
        {
            CustomerDiscountManager.Instance().RemoveDiscountFromCustomer(custID);
        }
    }
    #endregion
}
