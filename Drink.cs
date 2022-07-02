using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaOrderingSystem
{
    public class Drink:OrderItem
    {

        private static readonly string[] drinkTypeChoice= {"Soft Drink","Juice"};
        public string[] DrinkTypeChoice
        {
            get
            {
                return drinkTypeChoice;
            }
        }

        private static readonly double[] drinkTypePrice = { 3.50, 4.50 };
        private static readonly double[] drinkSizePrice = { 0.80, 1.00, 1.20 };
        public Drink()
            : base()
        {
        }


        public Drink(string size, string type, int quantity)
            :base(size,type,quantity)
        {
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
                    return DrinkTypeChoice[0]; //Soft Drink

                case 2:
                    return DrinkTypeChoice[1]; //Juice

                default:
                    throw new ArgumentOutOfRangeException("", "Value should be ranged from '1' to '2' only!");

            }
        }

        public override double ComputePricePerUnit()
        {
            double typePrice = 0, sizePrice = 0;
            for(int i=0;i<drinkTypeChoice.Length;i++)
                if (Type == drinkTypeChoice[i]) 
                    typePrice = drinkTypePrice[i]; 

            for(int i=0;i<SizeChoice.Length;i++)
                if (Size == SizeChoice[i])
                    sizePrice = drinkSizePrice[i]; 


            return typePrice * sizePrice;
        }
    }
}
