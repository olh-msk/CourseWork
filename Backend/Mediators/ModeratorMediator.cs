using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class ModeratorMediator
    {
        private static ModeratorMediator instance;

        private ModeratorMediator()
        {

        }
        public static ModeratorMediator Instance()
        {
            if (instance == null)
            {
                instance = new ModeratorMediator();
            }
            return instance;
        }

        public bool IfCorrectLoginPassword(string login, string password)
        {
            return ModeratorManager.Instance().IfCorrectLoginPassword(login, password);
        }
        public int GetAdminIdByLogin(string login)
        {
            return ModeratorManager.Instance().GetModeratorByLogin(login);
        }
    }
}
