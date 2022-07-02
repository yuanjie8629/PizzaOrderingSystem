using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrderingSystem
{
    class AddressFormatException:ApplicationException
    {
        public AddressFormatException(string exceptionMessage)
        : base(exceptionMessage)
        {
        }
    }
}
