using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace PizzaOrderingSystem
{
    class Orders
    {

        private ArrayList itemList;


        private Customer cust;
        public Customer Cust
        {
            get
            {
                return cust;
            }
        }

        public Orders(Customer cust)
        {
            this.cust = cust;
            itemList = new ArrayList();
        }


        public void RecordAnItem(OrderItem item)
        {
            itemList.Add(item);
        }


        public OrderItem[] GetItemList()
        {
            OrderItem[] item = new OrderItem[itemList.Count];
            for (int i = 0; i < itemList.Count; i++)
                item[i] = (OrderItem)itemList[i];

            return item;
        }


        public double ComputeDeliveryCharge(double price)
        {
            if (price < 20)
                return 5;

            else
                return 0;
            
        }

        public double ComputeTotalPrice()
        {
            double totalPrice = 0;
            foreach (OrderItem i in GetItemList())
                totalPrice += (i.ComputePricePerUnit() * i.Quantity);

            totalPrice += ComputeDeliveryCharge(totalPrice);

            return totalPrice;
        }

        public void UpdateItemQuantity(int index, OrderItem item)
        {
            itemList.RemoveAt(index);
            RecordAnItem(item);
        }


        public int CheckDuplicatePizza(Pizza p)
        {
            int index = -1;
            bool toppingCompare = true;
            OrderItem[] item = GetItemList();
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] is Pizza)
                {
                    Pizza piz = (Pizza)item[i];
                    string[] topping = piz.GetToppingList();
                    string[] pTopping = p.GetToppingList();
                    foreach(string old in topping)
                        foreach (string compare in pTopping)
                        {
                            if (old == null || compare == null || old != compare) 
                                    toppingCompare = false;

                            else
                                toppingCompare = true;

                        }

                    if (piz.Type == p.Type && piz.Size == p.Size && topping.Length == pTopping.Length && toppingCompare)
                    {
                        index = i;
                        break;
                    }
                }
            }


            return index;
        }
        

        public int CheckDuplicateDrink(Drink d)
        {
            int index = -1;
            OrderItem[] item = GetItemList();
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i] is Drink)
                {
                    Drink drk = (Drink)item[i];

                    if (drk.Size == d.Size && drk.Type == d.Type )
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }
       
    }
}
