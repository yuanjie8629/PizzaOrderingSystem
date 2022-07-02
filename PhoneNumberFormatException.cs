using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrderingSystem
{
    class PhoneNumberFormatException:ApplicationException
    {
        public PhoneNumberFormatException(string exceptionMessage)
        : base(exceptionMessage)
        {
        }
    }
}
