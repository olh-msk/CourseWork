using System;
using System.Collections.Generic;
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
    }
    #endregion
}
