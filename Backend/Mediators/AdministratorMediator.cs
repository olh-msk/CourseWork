using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class AdministratorMediator
    {
        private static AdministratorMediator instance;

        private AdministratorMediator()
        {

        }
        public static AdministratorMediator Instance()
        {
            if (instance == null)
            {
                instance = new AdministratorMediator();
            }
            return instance;
        }

        public bool IfCorrectLoginPassword(string login, string password)
        {
            return AdministratorManager.Instance().IfCorrectLoginPassword(login, password);
        }

        public int GetAdminIdByLogin(string login)
        {
            return AdministratorManager.Instance().GetAdministratorIdByLogin(login);
        }

        public Administrator GetAdministratorById(int adminID)
        {
            return AdministratorManager.Instance().GetAdministratorById(adminID);
        }
        public void AdministratorChangeUserStatus(int adminID, int customerID, string status)
        {
            Enum.TryParse(status, out UserStatus cusStatus);

            AdministratorManager.Instance().GetAdministratorById(adminID).ChangeCustomerStatus(customerID,cusStatus);
        }
    }
}
