using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Staff Manager]
      
    class StaffManager
    {
        private static StaffManager instance;

        public AdministratorManager AdministratorManager { get; private set; }
        public ModeratorManager ModeratorManager { get; private set; } 

        private StaffManager()
        {
            this.AdministratorManager = AdministratorManager.Instance();
            this.ModeratorManager = ModeratorManager.Instance();
        }

        public static StaffManager Instance()
        {
            if(instance == null)
            {
                instance = new StaffManager();
            }
            return instance;
        }
    }
    #endregion
}
