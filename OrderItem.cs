using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrderingSystem
{
    public abstract class OrderItem
    {

        private string size;
        public string Size
        {
            get
            {
                return size;
            }
        }

        private string type;
        public string Type
        {
            get
            {
                return type;
            }
        }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
        }

        private static readonly string[] sizeChoice = { "Small", "Medium", "Large" };
        public string[] SizeChoice
        {
            get
            {
                return sizeChoice;
            }
        }

        
        public OrderItem()
        {

        }

        public OrderItem(string size, string type, int quantity)
        {
            this.size = size;
            this.quantity = quantity;
            this.type = type;
        }

        public abstract string ObtainSize(int choice);
        public abstract string ObtainType(int choice);
        public abstract double ComputePricePerUnit();


    }
}
