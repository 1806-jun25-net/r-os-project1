//using PizzaPlace.Library;
//using PizzaPLace.DataAccess;
//using System;
//using System.Collections.Generic;

//namespace PizzaPlaceLibrary
//{
//    public class Order
//    {
//        User user = new User();

//        //default location
       


       

//        public Order()
//        {
//            user.LocationID = 1;
//            //default location
//        }
//        public int CreateUser()
//        {
//            var newUser = new OrdersRepository(new PizzaPlaceContext());
           
//            Console.WriteLine(" \n Welcome to Pizza Place \n" +
//                "_____________________________________________");
//            Console.WriteLine("Please enter your name:");
//            user.Name = Console.ReadLine().ToString();
//            Console.WriteLine("Please enter your last name:");
//            user.LastName = Console.ReadLine().ToString();
//            Console.WriteLine("Please enter your phone number:");
//            user.Phone = Console.ReadLine().ToString();

            
//            int currentUserID = newUser.GetUserIDByPhone(user.Name, user.Phone);
       
          
//            if (currentUserID ==0)
//            {
//                Console.WriteLine("Welcome " + user.Name);

//                CreateNewUser();
               
                
//            }
//            else
//            {
//                // Menu();
//                Console.WriteLine("Welcome back " + user.Name);
//                Console.WriteLine("\n Press Enter to continue");
//                Console.ReadLine();


//            }


//            return currentUserID;





//        }


//        public void CreateNewUser()
//        {
            
//            var newUser = new UsersRepository(new PizzaPlaceContext());
//            newUser.AddUsers(user.Name, user.LastName, user.Phone);
//            newUser.SaveChanges();

//        }

//        public (Decimal, Pizza, List<string>) CreatePizza(Pizza newPizza)
//        {
//            List<string> myToppings = new List<string>();
//            int numToppings =0 ;
//            string size;
//            decimal price;
//            Console.Clear();


//            Console.WriteLine("Entered Create a Pizza \n");

           





//            ChooseSize(newPizza);
//            Console.Clear();
//            ChooseCrust(newPizza);
//            Console.Clear();
//            ChooseSauce(newPizza);
//            Console.Clear();

//            //myToppings = ChooseToppings(newPizza);
//            //numToppings = myToppings.Count;

//            Console.Clear();
//            price = CalculatePrice(numToppings, newPizza.Size);

//            return (price, newPizza,myToppings);


//        }

//        public void ChooseSize(Pizza newPizza)
//        {
//            string size;

//            //choose size of pizza
//            ChooseSize:
//            Console.WriteLine("Choose Size: S | M | L");
//            size = Console.ReadLine();
//            size = size.ToUpper();
//            if (size == "S" || size == "M" || size == "L")
//            {
//                newPizza.Size = size;
//            }
//            else
//            {
//                Console.WriteLine("Please choose a valid size");
//                goto ChooseSize;
//            }

           


//        }
//        public void ChooseCrust(Pizza newPizza)
//        {
             
//            //choose crust of pizza
//            ChooseCrust:
//            Console.WriteLine("Choose Crust: \n 1: Deep Crust \n 2: Cheese Stuffed Crust \n 3: Thin Crust \n 4: Original Crust ");
//            var  crust = Console.ReadLine();

//            if (crust == "1" || crust == "2" || crust == "3" || crust == "4")
//            {
//                int theCrust = int.Parse(crust);
//                newPizza.Crust = theCrust;
//            }
//            else
//            {
//                Console.WriteLine("Please choose a valid crust");
//                goto ChooseCrust;
//            }
           

//        }
//        public void ChooseSauce(Pizza newPizza)
//        {
//            string sauce;
//            //choose sauce for pizza
//            ChooseSauce:
//            Console.WriteLine("Choose Sauce: \n 1: BBQ \n 2: Marinara \n 3: Alfredo ");
//            sauce = Console.ReadLine();
//            if (sauce == "1" || sauce == "2" || sauce == "3" )
//            {
//                newPizza.Sauce = sauce;
//            }
//            else
//            {
//                Console.WriteLine("Please choose a valid crust");
//                goto ChooseSauce;
//            }

           
//        }

//        public List<int> ChooseToppings(Pizza newPizza)
//        {
//            // int length = newPizza.Topping.Count;
//            List<string> ToppingsList = new List<string>();
//            //List<int> myToppingsID = new List<int>();
//            int count =0, n = 0;
//            bool submit;
//            string answer;
           
//            //foreach(string topping in newPizza.Topping )
//            //{
//            //    Console.WriteLine(topping);

//            //}
//            // store all the toppings in another list
//            foreach (string topping in newPizza.Topping)
//            {
//                ToppingsList.Add(topping);
//                count++;
//            }

//            do
//            {
//            ChooseToppings:
//                Console.WriteLine("Choose from the topping list");

//                for (int i = 0; i < count; i++)
//                {
//                    n = i + 1;
//                    Console.WriteLine(n + ") " + ToppingsList[i]);

//                }
//                Console.WriteLine("\nPress F to finish adding toppings");
//                answer = Console.ReadLine();
//                Console.Clear();

//                if (user.LocationID == 1)
//                {
//                    switch (answer)
//                    {
//                        case "1":
//                            newPizza.myToppingsID.Add(5);
//                            Console.WriteLine(ToppingsList[0] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "2":
//                            newPizza.myToppingsID.Add(6);
//                            Console.WriteLine(ToppingsList[1] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "3":
//                            newPizza.myToppingsID.Add(7);
//                            Console.WriteLine(ToppingsList[2] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "4":
//                            newPizza.myToppingsID.Add(8);
//                            Console.WriteLine(ToppingsList[3] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "5":
//                            newPizza.myToppingsID.Add(9);
//                            Console.WriteLine(ToppingsList[4] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "6":
//                            newPizza.myToppingsID.Add(10);
//                            Console.WriteLine(ToppingsList[5] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "7":
//                            newPizza.myToppingsID.Add(11);
//                            Console.WriteLine(ToppingsList[6] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "f":
//                            submit = true;
//                            break;
//                        case "F":
//                            submit = true;
//                            break;
//                        default:
//                            Console.WriteLine("Please enter a valid answer \n");
//                            goto ChooseToppings;

//                    }
//                }
//                else
//                {
//                    switch (answer)
//                    {
//                        case "1":
//                            newPizza.myToppingsID.Add(16);
//                            Console.WriteLine(ToppingsList[0] + " added to your pizza");
//                            goto ChooseToppings;
//                        case "2":
//                            newPizza.myToppingsID.Add(17);
//                            Console.WriteLine(ToppingsList[1] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "3":
//                            newPizza.myToppingsID.Add(18);
//                            Console.WriteLine(ToppingsList[2] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "4":
//                            newPizza.myToppingsID.Add(19);
//                            Console.WriteLine(ToppingsList[3] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "5":
//                            newPizza.myToppingsID.Add(20);
//                            Console.WriteLine(ToppingsList[4] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "6":
//                            newPizza.myToppingsID.Add(21);
//                            Console.WriteLine(ToppingsList[5] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "7":
//                            newPizza.myToppingsID.Add(22);
//                            Console.WriteLine(ToppingsList[6] + " added to your pizza \n");
//                            goto ChooseToppings;
//                        case "f":
//                            submit = true;
//                            break;
//                        case "F":
//                            submit = true;
//                            break;
//                        default:
//                            Console.WriteLine("Please enter a valid answer \n");
//                            goto ChooseToppings;

//                    }
//                }
                
               
//                Console.Clear();
//                Console.WriteLine("Finished choosing toppings");

//            } while (submit == false);


//            return (newPizza.myToppingsID);

//            //choose toppings
//            //Console.WriteLine("Choose the toppings that you want for your pizza");





//        }

//        public decimal CalculatePrice( int toppingsNum, string size)
//        {
//            //pricess for size of pizza
//            decimal S = 6, M = 10, L = 15, price;
//            //quantity of topppings batch for pizza size
//            int toppingS = 1, toppingM = 2, toppingL = 3;
//            decimal pricePerTopping = 1/2;


//            if (size == "S")
//            {
//                price = S + ((toppingsNum*toppingS) * pricePerTopping);
//            }
//            else if ( size == "M")
//            {
//                price = M + ((toppingsNum *toppingM) * pricePerTopping);

//            }
//            else
//            {
//                price = L + ((toppingsNum *toppingL) * pricePerTopping);
//            }


//            return price;



//        }


//        public decimal CalculatePrice(string size)
//        {
//            //pricess for size of pizza
//            decimal S = 6, M = 10, L = 15, price;
//            //quantity of topppings batch for pizza size
           


//            if (size == "S")
//            {
//                price = S;
//            }
//            else if (size == "M")
//            {
//                price = M;

//            }
//            else
//            {
//                price = L;
//            }


//            return price;



//        }
//        public void ChangeLocation()
//        {
//            string answer;
//            Choose: 
//            Console.WriteLine("Choose new  location from the list:");
//            Console.WriteLine("1) Herndon Pizza Place \n" +
//                "2) Reston Pizza Place  ");

//            answer = Console.ReadLine().ToUpper(); 

//           switch(answer)
//            {
//                case "1":
//                    Console.WriteLine("Location changed to Herndon Pizza Place");
//                    user.LocationID = 1;
//                    break;
//                case "2":
//                    Console.WriteLine("Location changed to Reston Pizza Place");
//                    user.LocationID = 2;
//                    break;
//                default:
//                    Console.WriteLine("Invalid answer");
//                    goto Choose;
               


//            }


            

//        }
//        public (Pizza, Decimal, String, List<string>) CreatePizzaPreMade(Pizza newPizza, List<string> toppings)
//        {
//            string answer, size, sauce, pizzaName;
//            decimal price;
           

            
//            ChoosePizza:
//            Console.WriteLine("Choose one of our Pizzas \n" +
//                "\n 1) Extra Cheese Pizza" +
//                "\n 2) Pepperoni Pizza" +
//                "\n 3) Sausage Pizza");

//            answer = Console.ReadLine();

//            ChooseSize(newPizza);
//            price = CalculatePrice(newPizza.Size);
//            ChooseSauce(newPizza);
//            ChooseCrust(newPizza);

//            switch (answer)
//            {
//                case "1":
                   
          
//                    pizzaName = "Cheese Pizza";
//                    newPizza.Name = "Cheese Pizza";
//                    newPizza.price = price;
//                    newPizza.DefaultPizzaInventoryId = 9;
//                    toppings.Add("Cheese");

//                    break;
//                case "2":
                   
                  
//                    newPizza.price = price;
//                    pizzaName = "Pepperoni Pizza";
//                    newPizza.Name = "Pepperoni Pizza";
//                    newPizza.DefaultPizzaInventoryId = 11;
//                    toppings.Add("Pepperoni");
//                    break;
//                case "3":
                   
                
//                    newPizza.price = price;
//                    pizzaName = "Sausage Pizza";
//                    newPizza.Name = "Sausage Pizza";
//                    newPizza.DefaultPizzaInventoryId = 8;
//                    toppings.Add("Sausage");
//                    break;
//                default:
//                    Console.WriteLine("Invalid value");
//                    goto ChoosePizza;
              

//            }

//            return (newPizza, price, pizzaName, toppings);


//        }

//        public void Menu()
//        {

//            bool finished = false;
//            bool preMade = false;
//            bool check = true;
//            string pizzaName= " ";

//            int pizzaCount = 1;
//            decimal price = 0, totalCost= 0;
//            Pizza pizza = new Pizza();
//            List<Pizza> currentPizza = new List<Pizza>();
//            Pizza newPizza = new Pizza();
//            //hacer aqui old user method

//            int userid;

//           userid=  CreateUser();
//           List<string> myToppings = new List<string>();



//            ChooseAgain:

//            do
//            {

//                Console.Clear();
//                Choose:
//                Console.WriteLine("Choose one of the following: \n");
//                Console.WriteLine("1: Choose a Pizza ");
//                Console.WriteLine("2: Create a Pizza ");
//                Console.WriteLine("3: Finish and Place Order");
//                Console.WriteLine("4: Change Store Location");
//                Console.WriteLine("5: Cancel and Exit");
//                var answer = Console.ReadLine();
                
//                //mytoppings.Add("test");
               
//                switch (answer)
//                {

//                    case "1":
//                        if (pizzaCount == 11)
//                            Console.WriteLine("You can add only one more pizza");
//                        if (pizzaCount == 13)
//                        {
//                            Console.WriteLine("You reach you max of 12 pizzas, you cannot add more pizzas");
//                            goto Choose;

//                        }
//                        else if (pizzaCount <= 12)
//                        {
//                           (pizza, price, pizzaName, myToppings) =  CreatePizzaPreMade(newPizza, myToppings);
//                            currentPizza.Add(pizza);
//                            totalCost += price; 
//                            preMade = true;
//                            pizzaCount++;
//                            goto Choose;

//                        }
//                        else
//                            goto Choose;

//                    case "2":
//                        if (pizzaCount == 11)
//                            Console.WriteLine("You can add only one more pizza");
//                        if (pizzaCount == 13)
//                        {
//                            Console.WriteLine("You reach you max of 12 pizzas, you cannot add more pizzas");
//                            goto Choose;

//                        }
//                        else if (pizzaCount <= 12)
//                        {
//                           // (price, pizza, myToppings) = CreatePizza(newPizza);
//                            currentPizza.Add(pizza);
                            
//                            totalCost += price;
//                            pizzaCount++;
//                            goto Choose;

//                        }
//                        else
//                            goto Choose;
                   
                        
//                    case "3":
//                        finished = true;
//                        break;
//                    case "4":
//                        ChangeLocation();
//                        goto Choose;
//                    case "5":
//                        Console.WriteLine("Come back soon \n" +
//                            "Press enter to exit");
//                        Console.ReadLine();
//                        Environment.Exit(0);
//                        break;
//                    default:
//                        Console.Clear();
//                        Console.WriteLine("Invalid Answer, please write again.");
//                        goto Choose;

//                }

                
//            } while (finished == false);

//            //Console.WriteLine("Price" + price);
            
//            if (preMade == true)
//            {
//                check = PlacePreMade(totalCost, currentPizza, pizzaName, userid, myToppings);
//                if (check == false)
//                {
//                    Console.WriteLine("Press enter to choose again");
//                    Console.ReadLine();
//                    goto ChooseAgain;
//                }
//                Console.WriteLine("Order Price: " + totalCost);
//                Console.WriteLine(" Order Placed \n Press enter to exit...");
//                Console.ReadLine();
                



//            }

//            //PlaceOrder(totalCost,currentPizza, myToppings );

            


//        }

//        public bool PlacePreMade(decimal price, List<Pizza> currentPizza, string PizzaName, int userId, List<string> myToppings)
//        {
//            bool check = true;
//            var findUser = new UsersRepository(new PizzaPlaceContext());
//            var findOrderId = new OrderPizzaRepository(new PizzaPlaceContext());
//            if (userId ==0)
//            {
//                 userId = findUser.GetUserIDByPhone(user.Name, user.Phone);
               
//            }
//            var minus = new InventoryRepository(new PizzaPlaceContext());
//            bool hasDough = true, hasSauce = true;
//            List<string> noTopping = new List<string>();
//            // check inventory

//           check = minus.CheckInventory(myToppings, currentPizza, user.LocationID,hasDough, hasSauce, noTopping );
//            if (check == false)
//            {
//                return false;
//            }
                


//            //minus in invetory
//            minus.MinusToppings(myToppings, currentPizza, user.LocationID);

//            //insert pizza
//            foreach (Pizza pizzas in currentPizza)
//            {
                
//                CreatePizza(pizzas.Size, pizzas.price, pizzas.Name, pizzas.Crust);
               

//            }

//            double priceDouble = double.Parse(price.ToString());//total price
//            //insert order
//            CreateOrder(priceDouble, userId);


//            //orderPizza
//            string size =currentPizza[0].Size, nameofPizza = currentPizza[0].Size;
           
//            int? order_id = findOrderId.GetOrderByUserId(userId);
//            //int? pizza_id = findOrderId.GetPizzaIdBySize(nameofPizza, size);
//            foreach(Pizza pizzas in currentPizza)
//            {
//                var id = pizzas.DefaultPizzaInventoryId;

//                CreateOrderPizza(order_id, id);
//            }

           



//            //hasTopping
//            string name;
//            int? pizza_id;
//            foreach (Pizza pizzas in currentPizza)
//            {
//                name = pizzas.Name;
//                size = pizzas.Size;
//                pizza_id = findOrderId.GetPizzaIdBySize(name, size);
//                CreateHasTopping(pizza_id, pizzas.DefaultPizzaInventoryId);
//            }


//            return true;
//        }
//        public void CreateOrder(double total,  int userID)
//        {
//            var newOrder = new OrdersRepository(new PizzaPlaceContext());
            
//            decimal totalDecimal = decimal.Parse(total.ToString());
//            newOrder.AddOrder(userID, user.LocationID, totalDecimal);
//            newOrder.SaveChanges();
//        }
//        public void CreateOrderPizza(int? order, int? pizza)
//        {
//            var newOrderPizza = new OrderPizzaRepository(new PizzaPlaceContext());

//            newOrderPizza.AddOrderPizza(order, pizza);
//            newOrderPizza.SaveChanges();
//        }
//        public void CreatePizza( string size, decimal price, string name, int crust)
//        {
//            var newPizza = new PizzasRepository(new PizzaPlaceContext());

//            newPizza.AddPizzas( size, price,name, crust);
//            newPizza.SaveChanges();
//        }
//        public void CreateHasTopping(int? pizza_id, int inventory)
//        {
//            var newHasTopping = new PizzaToppingRepository(new PizzaPlaceContext());

//            newHasTopping.AddPizzaTopping(pizza_id, inventory);
//            newHasTopping.SaveChanges();
//        }





//        public void PlaceOrder(decimal price, List<Pizza> pizzaList, List<int> myToppings )
//        {
            
//            foreach (Pizza pizzas in pizzaList)
//            {
//                Console.WriteLine("Price" + price +
//                    "\n Pizza Crust ID :" + pizzas.Crust + "Pizza size: "+  pizzas.Size + "Pizza Sauce: " + pizzas.Sauce );

//                foreach (int toppings in myToppings)
//                {
//                    Console.WriteLine("Toppings for pizza: " + toppings);

//                }

//            }
            
//            Console.WriteLine(myToppings.Count);
//            DateTime orderTime = DateTime.Now;

           




           
//        }

//        public void OrderHistory()
//        {

//        }


//    }
//}
