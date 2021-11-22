using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Zipcode { get; set; }

        public Address()
        {
            Street = "N/A";
            City = "N/A";
            Country = "N/A";
            Zipcode = 0;
        }

        //змінити інформацію про адресу, буде по GUI
        public void ChangeAddress()
        {

        }

        public override string ToString()
        {
            string res = "";
            res += string.Format("Street: {0}\tCity: {1}\nCountry: {2}\tZipcode {3}",
                Street,City,Country,Zipcode);
            return res;
        }
    }
}
