using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrderingSystem
{
    public class Customer
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        private string phoneNum;
        public string PhoneNum
        {
            get
            {
                return phoneNum;
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
        }
       
        public Customer(string name,string phoneNum,string address)
        {
            this.name = name;
            this.phoneNum = phoneNum;
            this.address = address;
        }
    }
}
