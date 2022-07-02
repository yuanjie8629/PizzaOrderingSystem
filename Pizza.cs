using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Linq;

namespace PizzaOrderingSystem
{
    public class Pizza:OrderItem
    {

        private ArrayList toppingList;

        private static readonly string[] toppingChoice = { "Cheese", "Mushrooms", "Sausage", "Pepperoni", "Cherry Tomato", "Onions" };
        public string[] ToppingChoice
        {
            get
            {
                return toppingChoice;
            }
        }

        private static readonly string[] pizzaTypeChoice = { "Thin", "Thick" };
        private static readonly double[] pizzaSizePrice = { 9.00, 16.00, 22.00 };
        private static readonly double[] pizzaToppingPrice = { 2.50, 2.00, 3.50, 4.50, 1.50, 1.20 };
        public string[] PizzaTypeChoice
        {
            get
            {
                return pizzaTypeChoice;
            }
        }

        public Pizza()
        : base()
        {
            toppingList = new ArrayList();
        }


        public Pizza(string size, string type, string[] topping, int quantity)
            : base(size, type, quantity)
        {
            toppingList = new ArrayList(topping);
        }
        public void RecordTopping(int topping)
        {
            if (topping < 1 || topping > 6)
                throw new ArgumentOutOfRangeException("", "Value should be ranged from '1' to '6' only!");
           else
                for (int i = 0; i < toppingChoice.Length; i++)
                    if (topping == i + 1)
                        toppingList.Add(toppingChoice[i]);
        }

        public string[] GetToppingList()
        {
            string[] topping = new string[toppingList.Count];

            for (int i = 0; i < toppingList.Count; i++)
                topping[i] = (string)toppingList[i];

            Array.Sort(topping);

            return topping;
        }
        
        public override string ObtainSize(int choice)
        {
            switch (choice)
            {
                case 1:
                    return SizeChoice[0]; //Small

                case 2:
                    return SizeChoice[1]; //Medium

                case 3:
                    return SizeChoice[2]; //Large

                default:
                    throw new ArgumentOutOfRangeException("", "Value should be ranged from '1' to '3' only!");

            }
        }

        public override string ObtainType(int choice)
        {
            switch (choice)
            {
                case 1:
                     return pizzaTypeChoice[0]; //Thin
                case 2:
                    return  pizzaTypeChoice[1]; //Thick
                default:
                    throw new ArgumentOutOfRangeException("", "Value should be ranged from '1' to '2' only!");
            }
        }

        public override double ComputePricePerUnit()
        {
            double toppingPrice = 0, sizePrice = 0;
            
            for(int i=0;i<SizeChoice.Length;i++)
                if (Size == SizeChoice[i])
                    sizePrice = pizzaSizePrice[i];


            string[] topping = GetToppingList();
            for (int i = 0; i < topping.Length; i++)
                for (int j = 0; j < toppingChoice.Length; j++) 
                    if (topping[i] == toppingChoice[j])
                        toppingPrice += pizzaToppingPrice[j];

            return sizePrice + toppingPrice; 
        }

    }
}
