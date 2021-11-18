using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    class StaffMediator
    {
        private static StaffMediator instance;

        public AdministratorManager AdministratorManager { get; private set; }
        public ModeratorManager ModeratorManager { get; private set; }

        private StaffMediator()
        {
            this.AdministratorManager = AdministratorManager.Instance();
            this.ModeratorManager = ModeratorManager.Instance();
        }
        public static StaffMediator Instance()
        {
            if (instance == null)
            {
                instance = new StaffMediator();
            }
            return instance;
        }

        public Moderator GetModeratorById(int moderID)
        {
            return ModeratorManager.GetModeratorById(moderID);
        }
        public Administrator GetAdministratorById(int adminID)
        {
            return AdministratorManager.GetAdministratorById(adminID);
        }
        public void ReadAdminsFromFile(string path)
        {
            AdministratorManager.ReadAdminsFromFile(path);
        }
        public void ReadModersFromFile(string path)
        {
            ModeratorManager.ReadModersFromFile(path);
        }
    }
}
