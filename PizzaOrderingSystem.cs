using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections;
using System.Transactions;
using System.Net.Http.Headers;

namespace PizzaOrderingSystem
{
    class PizzaOrderingSystem
    {
        public static void Main()
        {

            bool repeatFlag = true;
            Orders order = new Orders(RecordCustomer());
            do
            {
                bool optionRepeat = true;
                Console.Clear();
                Console.WriteLine("Pizza Operating System\n");
                Console.WriteLine("Name: " + order.Cust.Name);
                Console.WriteLine("Phone Number: " + order.Cust.PhoneNum);
                Console.WriteLine("Address: " + order.Cust.Address);
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add pizza to the order");
                Console.WriteLine("2. Add drink to the order");
                Console.WriteLine("3. Done");

                //Get Menu Option
                do
                {
                    try
                    {
                        int option;
                        string check;
                        Console.Write("\nOption {1-3}: ");
                        check = Console.ReadLine();

                        if (!(check.All(char.IsDigit)))
                            throw new FormatException("Value should contain only numbers!");

                        option = Convert.ToInt32(check);

                        if (option < 1 || option > 3)
                            throw new ArgumentOutOfRangeException("", "Value should be ranged from '1' to '3' only!");

                        optionRepeat = false;
                        switch (option)
                        {
                            case 1:
                                Console.Clear();
                                OrderPizza(order);
                                break;
                            case 2:
                                Console.Clear();
                                OrderDrink(order);
                                break;
                            case 3:
                                //The order must contain at least 1 order item. Else, the program will not proceed to write file.
                                if (order.GetItemList().Length == 0)
                                {
                                    Console.WriteLine("The order must contain at least 1 item to proceed!");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    Console.Clear();
                                    repeatFlag = false;
                                    CheckOut(order);
                                }
                                break;

                        }

                       

                    }

                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine("Invalid Input! " + ex.Message);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid Input! " + ex.Message);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("File-related Error Found: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception Error Found: " + ex.Message);
                    }

                } while (optionRepeat);
            } while (repeatFlag);

        }

        //Record customer information
        public static Customer RecordCustomer()
        {
            string name = "", phoneNum = "", address="";
            bool repeatFlag = true;
            Console.WriteLine("Pizza Operating System");

            //Get Customer Name
            do
            {
                try
                {
                    Console.WriteLine("\nName: ");
                    name = Console.ReadLine();
                    if (name.All(char.IsDigit))
                        throw new FormatException("Name should not have digit");
                    repeatFlag = false;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found :" + ex.Message);
                }
            } while (repeatFlag);

            repeatFlag = true;

            //Get Customer Phone Number
            do
            {
                try
                {
                    Console.WriteLine("\nPhone Number {Without '-'}: ");
                    phoneNum = Console.ReadLine();
                    if (!(phoneNum.All(char.IsDigit)) || phoneNum.Length < 10 || phoneNum.Length > 11)
                        throw new PhoneNumberFormatException("Phone number can only contain 10 or 11 digits, any non-numerical character is not allowed");

                    repeatFlag = false;
                }
                catch (PhoneNumberFormatException ex)
                {
                    Console.WriteLine("Invalid Input!\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found :" + ex.Message);
                }

            } while (repeatFlag);

            repeatFlag = true;

            //Get Customer Address
            do
            {
                try
                {
                    Console.WriteLine("\nAddress: ");
                    address = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(address))
                        throw new AddressFormatException("Value cannot be null!");
                    repeatFlag = false;
                }
                catch (AddressFormatException ex)
                {
                    Console.WriteLine("Invalid Input!\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found :" + ex.Message);
                }
            } while (repeatFlag);

            return new Customer(name, phoneNum, address);
        }

        //Pizza order
        public static void OrderPizza(Orders order)
        {
            Pizza piz = new Pizza();
            int option, quantity;
            char answer;
            bool repeatFlag = true;
            string size = "", type = "";
            Console.WriteLine("Pizza Operating System\n");
            Console.WriteLine("Name: " + order.Cust.Name);
            Console.WriteLine("Phone Number: " + order.Cust.PhoneNum);
            Console.WriteLine("Address: " + order.Cust.Address);

            //Get Pizza Size
            Console.WriteLine("\n\nPizza Size:");
            for (int i = 0; i < piz.SizeChoice.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, piz.SizeChoice[i]);

            do
            {
                try
                {
                    Console.Write("\nOption {1-3}: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    size = piz.ObtainSize(option);

                    repeatFlag = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }

            } while (repeatFlag);

            repeatFlag = true;

            //Get Pizza Crust Type
            Console.WriteLine("\nCrust Type:");
            for (int i = 0; i < piz.PizzaTypeChoice.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, piz.PizzaTypeChoice[i]);

            do
            {
                try
                {
                    Console.Write("\nOption {1-2}: ");

                    option = Convert.ToInt32(Console.ReadLine());
                    type = piz.ObtainType(option);

                    repeatFlag = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }

            } while (repeatFlag);

            repeatFlag = true;

            //Get Pizza Toppings
            Console.WriteLine("\nPizza Toppings:");
            for (int i = 0; i < piz.ToppingChoice.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, piz.ToppingChoice[i]);
            do
            {
                try
                {
                    
                    Console.Write("\nOption {1-6}: ");
                    option = Convert.ToInt32(Console.ReadLine());

                    piz.RecordTopping(option);
                    Console.WriteLine("Add another topping ('Y'/'N')?");
                    bool ansFlag = true;
                    do
                    {
                        try
                        {
                            Console.Write("\nAnswer: ");
                            answer = Convert.ToChar(Console.ReadLine());
                            if (answer != 'y' && answer != 'Y' && answer != 'n' && answer != 'N')
                                throw new ArgumentOutOfRangeException("","Value must be 'Y' or 'N'");

                            if (answer == 'N' || answer == 'n')
                                repeatFlag = false;

                            ansFlag = false;

                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            Console.WriteLine("Invalid Input! " + ex.Message);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid Input! Value must be 'Y' or 'N'");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception Error Found: " + ex.Message);
                        }
                    } while (ansFlag);

                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }

            } while (repeatFlag);

            repeatFlag = true;

            //Get Pizza Quantity
            do
            {
                try
                {
                    Console.Write("\nPizza Quantity: ");
                    quantity = Convert.ToInt32(Console.ReadLine());

                    piz = new Pizza(size, type, piz.GetToppingList(), quantity);
                    int index = order.CheckDuplicatePizza(piz);

                    if (index == -1)
                    {
                        //if duplicate drink is not found, add new pizza
                        OrderItem item = piz;
                        order.RecordAnItem(item);
                        Console.WriteLine("\nYour pizza order is sucessfully added!");
                    }
                    else
                    {
                        //if duplicate pizza is found, ask user whether to add on same pizza
                        bool ansFlag = true;
                        Console.WriteLine("\nIt is found that you have ordered {0} of the same pizza.", order.GetItemList()[index].Quantity);
                        Console.WriteLine("Do you want to add on ('Y'/'N')?");
                        do
                        {
                            try
                            {
                                Console.Write("\nAnswer: ");
                                answer = Convert.ToChar(Console.ReadLine());
                                if (answer != 'y' && answer != 'Y' && answer != 'n' && answer != 'N')
                                    throw new ArgumentOutOfRangeException("", "Value must be 'Y' or 'N'");

                                if (answer == 'Y' || answer == 'y')
                                {
                                    quantity += order.GetItemList()[index].Quantity;
                                    //remove old pizza data and add new pizza data with new quantity
                                    piz = new Pizza(piz.Size, piz.Type, piz.GetToppingList(), quantity);
                                    order.UpdateItemQuantity(index, piz);
                                    Console.WriteLine("\nYour pizza order is sucessfully added!");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Your pizza order is denied.");
                                }
                            
                                ansFlag = false;

                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine("Invalid Input! " + ex.Message);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input! Value must be 'Y' or 'N'");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception Error Found: " + ex.Message);
                            }

                        } while (ansFlag);
                       
                    }

                    repeatFlag = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exceptio Error Found: " + ex.Message);
                }
            } while (repeatFlag);
            Console.ReadLine();
        }

        //Drink order
        public static void OrderDrink(Orders order)
        {
            Drink drk = new Drink();
            int option, quantity;
            string size="", type="";
            bool repeatFlag = true;
            Console.WriteLine("Pizza Operating System\n");
            Console.WriteLine("Name: " + order.Cust.Name);
            Console.WriteLine("Phone Number: " + order.Cust.PhoneNum);
            Console.WriteLine("Address: " + order.Cust.Address);

            //Get Drink Size
            Console.WriteLine("\nDrink Size:");
            for (int i = 0; i < drk.SizeChoice.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, drk.SizeChoice[i]);

            do
            {
                try
                {
                    Console.Write("\nOption {1-3}: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    size = drk.ObtainSize(option);

                    repeatFlag = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }
            } while (repeatFlag);

            repeatFlag = true;

            //Get Drink Type 
            Console.WriteLine("\nDrink Type:");
            for (int i = 0; i < drk.DrinkTypeChoice.Length; i++)
                Console.WriteLine("{0}. {1}", i + 1, drk.DrinkTypeChoice[i]);
            do
            {
                try
                {
                    Console.Write("\nOption {1-2}: ");
                    option = Convert.ToInt32(Console.ReadLine());
                    type = drk.ObtainType(option);

                    repeatFlag = false;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("Invalid Input! " + ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }
            } while (repeatFlag);


            repeatFlag = true;

            //Get Drink Quantity
            do
            {
                try
                {
                    Console.Write("\nDrink Quantity: ");
                    quantity = Convert.ToInt32(Console.ReadLine());

                    drk = new Drink(size, type, quantity);
                    
                    int index = order.CheckDuplicateDrink(drk);

                    if (index == -1)
                    {
                        //if duplicate drink is not found, add new drink
                        OrderItem item = drk;
                        order.RecordAnItem(item);
                        Console.WriteLine("\nYour drink order is sucessfully added!");
                    }
                    else
                    {
                        //if duplicate drink is found, ask user whether to add on same drink
                        bool ansFlag = true;
                        char answer;
                        Console.WriteLine("\nIt is found that you have ordered {0} of the same drink.", order.GetItemList()[index].Quantity);
                        Console.WriteLine("Do you want to add on ('Y'/'N')?");
                        do
                        {
                            try
                            {
                                Console.Write("\nAnswer: ");
                                answer = Convert.ToChar(Console.ReadLine());
                                if (answer != 'y' && answer != 'Y' && answer != 'n' && answer != 'N')
                                    throw new ArgumentOutOfRangeException("", "Value must be 'Y' or 'N'");

                                if (answer == 'Y' || answer == 'y')
                                {
                                    quantity += order.GetItemList()[index].Quantity;
                                    //remove old drink data and add new drink data with new quantity
                                    drk = new Drink(drk.Size, drk.Type, quantity);
                                    order.UpdateItemQuantity(index, drk);
                                    Console.WriteLine("\nYour drink order is sucessfully added!");
                                }
                                else
                                {
                                    Console.WriteLine("Your drink order is denied.");
                                }

                                ansFlag = false;

                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                Console.WriteLine("Invalid Input! " + ex.Message);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid Input! Value must be 'Y' or 'N'");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception Error Found: " + ex.Message);
                            }
                        } while (ansFlag);
                    }
                    
                    repeatFlag = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Input! Value should contain only numbers!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Error Found: " + ex.Message);
                }
            } while (repeatFlag);
            Console.ReadLine();
        }

        //End of order
        //Write order into text file
        public static void CheckOut(Orders order)
        {
            try
            {
                StreamWriter writer = new StreamWriter("Receipt.txt");
                OrderItem[] item = order.GetItemList();
                int countPizza = 0, countDrink = 0;
                //Write Title and Customer Information into text file
                writer.WriteLine("Pizza Order");
                writer.WriteLine("Delivery To:");
                writer.WriteLine("{0}\n{1}\n{2}\n", order.Cust.Name, order.Cust.PhoneNum.Insert(3, "-"), order.Cust.Address);
                writer.Write("Total Price(RM): " + order.ComputeTotalPrice().ToString("0.00"));
                writer.WriteLine("\t\tDelivery Charge(RM): " + order.ComputeDeliveryCharge(order.ComputeTotalPrice()).ToString("0.00"));
                writer.WriteLine("*Free Delivery for any order RM20 and above*");
                writer.WriteLine("\nNo\t\tItems\t\t\t\t\t\tSize      Quantity    Price Per Unit(RM)\tPrice(RM)");

                //To determine the number of pizza and drink ordered respectively
                foreach (OrderItem i in item)
                {
                    if (i is Pizza)
                        countPizza++;
                    else
                        countDrink++;
                }

                //To store the output of the result
                string[] outputPizza = new string[countPizza];
                string[] outputDrink = new string[countDrink];
                for (int i = 0; i < item.Length; i++)
                {
                    int counterPiz = 0, counterDrk = 0;
                    if (item[i] is Pizza)
                    {
                        Pizza p = (Pizza)item[i];
                        string[] toppingList = p.GetToppingList();
                        string topping = "";
                        double price = p.ComputePricePerUnit() * p.Quantity;
                        for (int a = 0; a < toppingList.Length; a++)
                            if (a == toppingList.Length - 1)
                            {
                                topping += toppingList[a];
                                topping += ")";
                            }
                            else
                            {
                                topping += toppingList[a];
                                topping += ", ";
                            }

                        outputPizza[counterPiz] += string.Format("{0,-8}", i + 1);
                        if (p.Type==p.PizzaTypeChoice[0])
                            outputPizza[counterPiz] += string.Format("{0}  Crust Pizza({1,-37}", p.Type, topping);
                        else if (p.Type==p.PizzaTypeChoice[1])
                            outputPizza[counterPiz] += string.Format("{0} Crust Pizza({1,-37}", p.Type, topping);
                        outputPizza[counterPiz] += string.Format("{0,-14}", p.Size);
                        outputPizza[counterPiz] += string.Format("{0,-10}",p.Quantity);
                        outputPizza[counterPiz] += string.Format("{0,10}", p.ComputePricePerUnit().ToString("0.00"));
                        outputPizza[counterPiz] += string.Format("{0,21}\n", price.ToString("0.00"));
                        counterPiz++;
                    }
                    else if (item[i] is Drink)
                    {
                        Drink d = (Drink)item[i];
                        double price = d.ComputePricePerUnit() * d.Quantity;

                        outputDrink[counterDrk] += string.Format("{0,-8}", i + 1);
                        outputDrink[counterDrk] += string.Format("{0,-55}", d.Type);
                        outputDrink[counterDrk] += string.Format("{0,-14}", d.Size);
                        outputDrink[counterDrk] += string.Format("{0,-10}", d.Quantity);
                        outputDrink[counterDrk] += string.Format("{0,10}", d.ComputePricePerUnit().ToString("0.00"));
                        outputDrink[counterDrk] += string.Format("{0,21}\n", price.ToString("0.00"));
                        counterDrk++;
                    }
                }
                //To arrange the data so that the list can look more organized
                Array.Sort(outputPizza);
                Array.Sort(outputDrink);

                //Write sorted result into text file
                foreach (string o in outputPizza)
                    writer.Write(o);

                foreach (string o in outputDrink)
                    writer.Write(o);

                writer.Close();

                //Design of Exiting Program
                for (int i = 1; i <= 50; i++)
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\n\nLoading...");
                    for (int j = 1; j <= i; j++)
                        Console.Write("|");
                    Console.Write("\n{0}%", 2 * i);
                    if (i > 1 && i < 10)
                        Console.Write("\t\t\t\t Accessing Main Memory...");
                    else if (i > 10 && i < 25)
                        Console.Write("\t\t\t\t Accessing Cache...");
                    else if (i > 25 && i < 50)
                        Console.Write("\t\t\t\t Saving Changes...");
                    else
                        Console.Write("\t\t\t\t Complete!!\n\n");
                    Thread.Sleep(50 - i);
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
