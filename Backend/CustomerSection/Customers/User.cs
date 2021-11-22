using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    #region [Online Users]
    enum UserStatus {New, Registered, VIP, Inanctive, Blocked }
    abstract class User
    {
        public UserStatus UserStatus { get; set; }

        public User()
        {
            this.UserStatus = UserStatus.New;
        }
        //вертає статус у стрінг
        public string GetStatusString()
        {
            return Enum.GetName(typeof(UserStatus), this.UserStatus);

        }
    }

    //клас для онлайн користувача, що не зараєструвався у магазині
    class WebUser: User
    {
        public WebUser():base()
        {

        }
    }
    #endregion
}
