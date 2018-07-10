using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public class Order
    {
        User user = new User();


       

        public Order()
        {

        }
        public void CreateUser()
        {

            Console.WriteLine("Welcome to Pizza Place");
            Console.WriteLine("Please enter your name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Please enter your last name");
            user.LastName = Console.ReadLine();
            Console.WriteLine("Please enter your email");
            user.Email = Console.ReadLine();


        }
        public (double, Pizza) CreatePizza()
        {

            int numToppings =0 ;
            string size;
            double price;
            
            Console.WriteLine("Entered Create a Pizza");


            Pizza newPizza = new Pizza();

            size = ChooseSize(newPizza);
            ChooseCrust(newPizza);
            ChooseSauce(newPizza);
            numToppings = ChooseToppings(newPizza);
            price = CalculatePrice(numToppings, size);

            return (price, newPizza);


        }

        public string ChooseSize(Pizza newPizza)
        {
            string size;

            //choose size of pizza
            ChooseSize:
            Console.WriteLine("Choose Size: S | M | L");
            size = Console.ReadLine();
            size = size.ToUpper();
            if (size == "S" || size == "M" || size == "L")
            {
                newPizza.Size = size;
            }
            else
            {
                Console.WriteLine("Please choose a valid size");
                goto ChooseSize;
            }

            return size;


        }
        public void ChooseCrust(Pizza newPizza)
        {
            string crust;
            //choose crust of pizza
            ChooseCrust:
            Console.WriteLine("Choose Crust: \n 1: Deep Crust \n 2: Cheese Stuffed Crust \n 3: Thin Crust \n 4: Original Crust ");
            crust = Console.ReadLine();

            if (crust == "1" || crust == "2" || crust == "3" || crust == "4")
            {
                newPizza.Crust = crust;
            }
            else
            {
                Console.WriteLine("Please choose a valid crust");
                goto ChooseCrust;
            }


        }
        public void ChooseSauce(Pizza newPizza)
        {
            string sauce;
            //choose sauce for pizza
            ChooseSauce:
            Console.WriteLine("Choose Sauce: \n 1: BBQ \n 2: Marinara \n 3: Alfredo ");
            sauce = Console.ReadLine();
            if (sauce == "1" || sauce == "2" || sauce == "3" )
            {
                newPizza.Crust = sauce;
            }
            else
            {
                Console.WriteLine("Please choose a valid crust");
                goto ChooseSauce;
            }
        }

        public int  ChooseToppings(Pizza newPizza)
        {
            // int length = newPizza.Topping.Count;
            List<string> ToppingsList = new List<string>();
            //List<int> myToppingsID = new List<int>();
            int count =0, n = 0, tempNum;
            bool submit;
            string answer;
           
            //foreach(string topping in newPizza.Topping )
            //{
            //    Console.WriteLine(topping);

            //}
            // store all the toppings in another list
            foreach (string topping in newPizza.Topping)
            {
                ToppingsList.Add(topping);
                count++;
            }

            do
            {
            ChooseToppings:
                Console.WriteLine("Choose from the topping list");

                for (int i = 0; i < count; i++)
                {
                    n = i + 1;
                    Console.WriteLine(n + ") " + ToppingsList[i]);

                }
                Console.WriteLine("\nPress F to finish adding toppings");
                answer = Console.ReadLine();
                Console.Clear();

                if (user.LocationID == 1)
                {
                     tempNum = 5;
                    switch (answer)
                    {
                        case "1":
                            newPizza.myToppingsID.Add(tempNum);
                            Console.WriteLine(ToppingsList[0] + " added to your pizza");
                            goto ChooseToppings;
                        case "2":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[1] + " added to your pizza");
                            goto ChooseToppings;
                        case "3":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[2] + " added to your pizza");
                            goto ChooseToppings;
                        case "4":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[3] + " added to your pizza");
                            goto ChooseToppings;
                        case "5":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[4] + " added to your pizza");
                            goto ChooseToppings;
                        case "6":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[5] + " added to your pizza");
                            goto ChooseToppings;
                        case "7":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[6] + " added to your pizza");
                            goto ChooseToppings;
                        case "f":
                            submit = true;
                            break;
                        case "F":
                            submit = true;
                            break;
                        default:
                            Console.WriteLine("Please enter a valid answer \n");
                            goto ChooseToppings;

                    }
                }
                else
                {
                    tempNum = 16;
                    switch (answer)
                    {
                        case "1":
                            newPizza.myToppingsID.Add(tempNum);
                            Console.WriteLine(ToppingsList[0] + " added to your pizza");
                            goto ChooseToppings;
                        case "2":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[1] + " added to your pizza");
                            goto ChooseToppings;
                        case "3":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[2] + " added to your pizza");
                            goto ChooseToppings;
                        case "4":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[3] + " added to your pizza");
                            goto ChooseToppings;
                        case "5":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[4] + " added to your pizza");
                            goto ChooseToppings;
                        case "6":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[5] + " added to your pizza");
                            goto ChooseToppings;
                        case "7":
                            newPizza.myToppingsID.Add(tempNum++);
                            Console.WriteLine(ToppingsList[6] + " added to your pizza");
                            goto ChooseToppings;
                        case "f":
                            submit = true;
                            break;
                        case "F":
                            submit = true;
                            break;
                        default:
                            Console.WriteLine("Please enter a valid answer \n");
                            goto ChooseToppings;

                    }
                }
                
               
                Console.Clear();
                Console.WriteLine("Finished choosing toppings");

            } while (submit == false);


            return newPizza.myToppingsID.Count;

            //choose toppings
            //Console.WriteLine("Choose the toppings that you want for your pizza");





        }

        public double CalculatePrice( int toppingsNum, string size)
        {
            //pricess for size of pizza
            double S = 6.00, M = 10.00, L = 15.00, price;
            //quantity of topppings batch for pizza size
            int toppingS = 1, toppingM = 2, toppingL = 3;
            double pricePerTopping = 0.50;


            if (size == "S")
            {
                price = S + ((toppingsNum*toppingS) * pricePerTopping);
            }
            else if ( size == "M")
            {
                price = M + ((toppingsNum *toppingM) * pricePerTopping);

            }
            else
            {
                price = L + ((toppingsNum *toppingL) * pricePerTopping);
            }


            return price;



        }
        public void ChangeLocation()
        {
            Console.WriteLine("Change location");
            Console.WriteLine("Press any key");

        }

        public void Menu()
        {

            bool finished = false;
            int pizzaCount = 1;
            double price = 0.0;
            Pizza pizza = new Pizza();
            List<Pizza> currentPizza = new List<Pizza>();

            //hacer aqui old user method
            CreateUser();



            do
            {


                Choose:
                Console.WriteLine("1: Create a Pizza ");
                Console.WriteLine("2: Finish and Place Order");
                Console.WriteLine("3: Change Store Location");
                Console.WriteLine("4: Cancel and Exit");
                var answer = Console.ReadLine();

                switch (answer)
                { 
                    case "1":
                        if (pizzaCount == 1)
                            Console.WriteLine("You can add only one more pizza");
                        if (pizzaCount == 3)
                        {
                            Console.WriteLine("You cannot add more pizzas");
                            goto Choose;

                        }
                        else if (pizzaCount <= 2)
                        {
                            (price, pizza) = CreatePizza();
                            currentPizza.Add(pizza);
                            price = +price;
                            pizzaCount++;
                            goto Choose;

                        }
                        else
                            goto Choose;
                   
                        
                    case "2":
                        finished = true;
                        break;
                    case "3":
                        ChangeLocation();
                        goto Choose;
                    case "4":
                        Console.WriteLine("Come back soon \n" +
                            "Press enter to exit");
                        Console.ReadLine();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Answer, please write again.");
                        goto Choose;

                }

                
            } while (finished == false);

            Console.WriteLine("Price" + price);
            
            

            placeOrder(price,currentPizza);

            


        }
        public void placeOrder(double price, List<Pizza> PizzaList )
        {

            foreach( Pizza pizzas in PizzaList)
            {
                Console.WriteLine("Pizza :"+ pizzas.Crust + pizzas.Size + pizzas.Sauce); 
            }
            //insert to database
            Console.WriteLine("hey");
            Console.ReadLine();




           
        }


    }
}
