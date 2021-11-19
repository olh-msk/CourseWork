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
    }
}
