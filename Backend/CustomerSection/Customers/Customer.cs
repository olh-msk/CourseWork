using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    abstract class Customer: User
    {
        //змінна для id
        private static int nextCustomertId = 1;

        public int CustomerId { get; private set; }

        public ShoppingCart ShoppingCart { get; set; }
        public PersonalData PersonalData { get; set; }

        public Customer():base()
        {
            CustomerId = nextCustomertId++;
            this.ShoppingCart = new ShoppingCart();
            this.PersonalData = new PersonalData();
            this.UserStatus = UserStatus.Registered;
        }

        //для заходження у систему-----------
        //тут буде взаємодія з GUI у майбітніх версіях
        public bool LogIn(string login, string password)
        {
            return this.PersonalData.CheckForLogin(login, password);
        }


    }
}
