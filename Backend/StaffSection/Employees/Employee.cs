using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    abstract class Employee
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public Employee()
        {
            this.Login = "N/A";
            this.PhoneNumber = "N/A";
            this.Password = "N/A";
        }
    }
}
