using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaPLace.DataAccess;
using PizzaPlaceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaPlace.Library
{
    public class OrdersRepository
    {

        private readonly PizzaPlaceContext _db;

        public OrdersRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


          

        }



        public IEnumerable<Orders> GetOrderPizza()
        {

            List<Orders> orders = _db.Orders.AsNoTracking().ToList();
            return orders;
        }
        // get orders by location
        public IEnumerable<Orders> GetLocationOrders(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id);
            return OrderLocations;
        }
        // cheaper location orders
        public IEnumerable<Orders> GetLocationOrdersByCheapest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderBy(g => g.OrderTotal);
            return OrderLocations;
        }
        //most expensive location orders
        public IEnumerable<Orders> GetLocationOrdersMostExpensive(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderByDescending(g => g.OrderTotal);
            return OrderLocations;
        }
        //latest order in location
        public IEnumerable<Orders> GetLocationOrderLatest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderByDescending(g => g.OrderTime);
            return OrderLocations;
        }
        //earliest order in location
        public IEnumerable<Orders> GetLocationOrderEarliest(int location_id)
        {

            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderBy(g => g.OrderTime);
            return OrderLocations;
        }


        public void AddOrder(int user_id, int? location_id, decimal orderTotal)
        {

            var date_time = DateTime.Now;

            var order = new Orders
            {
                UsersId = user_id,
                LocationId = location_id,
                OrderTotal = orderTotal,
                OrderTime = date_time
            };
            _db.Add(order);
        }



        public void GetUserOrder(string name, string lastname)
        {
            var user = _db.Users.FirstOrDefault(g => g.FirstName == name && g.LastName == lastname);
            var userID = user.UsersId;
            var orders = GetOrdersTable();
            var userOrder = orders.Where(g => g.UsersId == userID);
            var allOrderPizza = GetOrderPizza();
            var allPizzas = GetPizza();


            foreach (var item in orders)
            {
                var orderPizza = allOrderPizza.Where(g => g.OrderId == item.OrderId);
                Console.WriteLine("Order No.: " + item.OrderId + "  Date & Time:" + item.OrderTime);

                foreach (var pizzas in allPizzas)
                {
                    var myPizzas = _db.OrderPizza.Where(g => g.PizzaId == pizzas.PizzaId);

                    foreach (var pizza in myPizzas)
                    {
                        var pizzaName = _db.Pizzas.FirstOrDefault(g => g.PizzaId == pizza.PizzaId);
                        Console.WriteLine("Pizzas: " + pizzaName.Name);
                    }
                }
                // var pizzaNames = _db.Pizzas.Where(g => g.N.ame == pizzasOfOrder);

            }
        }

        public void GetUserOrderByEarliest(string name, string lastname)
        {
            var user = _db.Users.FirstOrDefault(g => g.FirstName == name && g.LastName == lastname);
            var userID = user.UsersId;
            var orders = GetOrdersTable();
            var userOrder = orders.Where(g => g.UsersId == userID);
            var allOrderPizza = GetOrderPizza();
            var allPizzas = GetPizza();


            foreach (var item in orders)
            {
                var orderPizza = allOrderPizza.Where(g => g.OrderId == item.OrderId).OrderBy(g => g.OrderTime);
                Console.WriteLine("Order No.: " + item.OrderId + "  Date & Time:" + item.OrderTime);

                foreach (var pizzas in allPizzas)
                {
                    var myPizzas = _db.OrderPizza.Where(g => g.PizzaId == pizzas.PizzaId);

                    foreach (var pizza in myPizzas)
                    {
                        var pizzaName = _db.Pizzas.FirstOrDefault(g => g.PizzaId == pizza.PizzaId);
                        Console.WriteLine("Pizzas: " + pizzaName.Name);
                    }
                }
                // var pizzaNames = _db.Pizzas.Where(g => g.N.ame == pizzasOfOrder);

            }
        }



        public IEnumerable<Orders> GetOrdersTable()
        {
            List<Orders> order = _db.Orders.ToList();
            return order;
        }

        public IEnumerable<Users> GetUsertable()
        {
            List<Users> user = _db.Users.ToList();
            return user;
        }

        public IEnumerable<Pizzas> GetPizza()
        {
            List<Pizzas> pizza = _db.Pizzas.ToList();
            return pizza;
        }
       

        public IEnumerable<Locations> GetLocation()
        {
            List<Locations> location = _db.Locations.ToList();
            return location;
        }



        public void GetUserOrderHistory(int user_id, int location_id, int order_id)
        {
            var user = _db.Users.FirstOrDefault(g => g.UsersId == user_id);//get user info

            var order = _db.Orders.FirstOrDefault(g => g.OrderId == order_id && g.UsersId == user_id);//get order details

            var orderPizza = _db.OrderPizza.FirstOrDefault(g => g.OrderId == order.OrderId);// to get pizza_id in an order
            // en vez de first or default es .where
            var pizza = _db.Pizzas.FirstOrDefault(g => g.PizzaId == orderPizza.PizzaId);// to get pizza name

            var location = _db.Locations.FirstOrDefault(g => g.LocationId == location_id);//to get location name 

            string pizzaName = pizza.Name, locationName = location.Name;

            Console.WriteLine("Order No. " + order.OrderId + "User Id: " + user.UsersId + " \n" +
                "First Name: " + user.FirstName + " Last Name: " + user.LastName + " \n" +
                "Order Details: \n " +
                  pizza.Name + "   \n" +
                  order.OrderTotal + " \n"

                  );



        }

        public void EditOrder(Orders order)
        {

            _db.Update(order);


        }

        //order pizza
       

        public void AddOrderPizza(int? order_id, int? pizza_id)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var OrderPizza = new OrderPizza
            {
                OrderId = order_id,
                PizzaId = pizza_id,

            };
            _db.Add(OrderPizza);
        }
        public int? GetPizzaIdBySize(string pizza, string size)
        {

            var repo = _db.Pizzas.LastOrDefault(g => g.Name == pizza);
            if (pizza == null)
            {
                return 0;
            }
            else
            {

                return repo.PizzaId;
            }


        }
        public int? GetOrderByUserId(int? findUserId)
        {

            var order = _db.Orders.FirstOrDefault(g => g.UsersId == findUserId);
            if (order == null)
            {
                return 0;
            }

            else
            {
                return order.OrderId;
            }


        }


        public void EditOrderPizza(OrderPizza OrderPizza)
        {
            // would add it if it didn't exist
            _db.Update(OrderPizza);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }


        public IEnumerable<Locations> GetLocations()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Locations> Locations = _db.Locations.AsNoTracking().ToList();
            return Locations;
        }


        public void AddPizzas(string size, decimal? price, string name, int? crust)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var Pizza = new Pizzas
            {
                Name = name,
                Price = price,
                Size = size,
                Crust = crust
            };
            _db.Add(Pizza);
        }


        public string GetLocationById(int? id)
        {


            var location = _db.Locations.FirstOrDefault(g => g.LocationId == id);
            string name;
            name = location.Name.ToString();
            return name;

        }

        //inventory



        //save changes
        public bool CheckInventory(List<string> myToppings, Pizzas pizza, string salsa, int? location)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            Console.WriteLine(configuration.GetConnectionString("PizzaPlace"));
            var optionsBuilder = new DbContextOptionsBuilder<PizzaPlaceContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPlace"));
            ////////////////////////////////////////////////////////////////////////////////////////

            var noTopping = new List<string>();
            var repoLocation = new OrdersRepository(new PizzaPlaceContext((optionsBuilder.Options)));
            string locationName = repoLocation.GetLocationById(location);
            string sauce = salsa;
            bool check = true;
            string size = " ";

            //set sauce string 

            //if (s.Sauce == "1")
            //{
            //    sauce = "BBQ Sauce";
            //}
            //else if (s.Sauce == "2")
            //{
            //    sauce = " Marinara Sauce";
            //}
            //else
            //{
            //    sauce = "Alfredo Sauce ";
            //}

            

                size = pizza.Size;//size of pizza


                // checking availability quantity, how many of topping, sauce  and dough unit needs to be taken out per size
                int takeout = 0;

                if (size == "S")
                {
                    takeout = 1;

                }
                else if (size == "M")
                {
                    takeout = 2;
                }
                else if (size == "L")
                {
                    takeout = 3;
                }


                var items = _db.Inventory.FirstOrDefault(g => g.LocationId == location);

                //has dough?
                if (items.LocationId == location)
                {
                    var d = _db.Inventory.FirstOrDefault(g => g.Name == "dough" && g.LocationId == location);

                    if (d.Quantity < takeout)
                    {
                        Console.WriteLine(locationName + " Doesn't have any more dough");
                    }
                }
                //has souce?
                if (items.LocationId == location)
                {
                    var d = _db.Inventory.FirstOrDefault(g => g.Name == sauce && g.LocationId == location);
                    if (d.Quantity < takeout)
                    {
                        Console.WriteLine(locationName + " Doesn't have any more of the " + sauce);
                    }

                }

                foreach (var topping in myToppings)
                {


                    if (topping == "Cheese" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Cheese" && g.LocationId == location);


                        if (id.Quantity < takeout)
                        {
                            noTopping.Add("Cheese");

                        }


                    }
                    else if (topping == "Pepperoni" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Pepperoni" && g.LocationId == location);


                        if (id.Quantity < takeout)
                        {
                            noTopping.Add("Pepperoni");
                        }
                    }
                    else if (topping == "Sausage" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Sausage" && g.LocationId == location);


                        if (id.Quantity < takeout)
                        {
                            noTopping.Add("Sausage");

                        }
                    }




                }


            

            if (noTopping.Count() > 0)
            {
                Console.WriteLine("Sorry " + locationName + " doesn't have in it's inventory: \n ");
                foreach (var topping in noTopping)
                {
                    Console.WriteLine(topping + " \n");
                }

                check = false;
            }

            return check;
        }
        public void MinusToppings(List<string> myToppings, Pizzas pizza, string salsa, int? location)
        {

            string sauce = salsa;
            string size = pizza.Size;
   
                // how many of topping and dough unit to take out depending on size of pizza
                int minus = 0;

                if (size == "S")
                {
                    minus = 1;

                }
                else if (size == "M")
                {
                    minus = 2;
                }
                else if (size == "L")
                {
                    minus = 3;
                }


                var items = _db.Inventory.FirstOrDefault(g => g.LocationId == location);
                //take out dough
                if (items.LocationId == location)
                {
                    var d = _db.Inventory.FirstOrDefault(g => g.Name == "dough" && g.LocationId == location);

                    d.Quantity = d.Quantity - minus;
                }
                //take out sauce 
                if (items.LocationId == location)
                {
                    var d = _db.Inventory.FirstOrDefault(g => g.Name == sauce && g.LocationId == location);
                    if (d.Quantity < minus)
                    {
                        d.Quantity = d.Quantity - minus;
                    }

                }



                foreach (var topping in myToppings)
                {


                    if (topping == "Cheese" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Cheese" && g.LocationId == location);

                        id.Quantity = id.Quantity - minus;


                    }
                    else if (topping == "Pepperoni" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Pepperoni" && g.LocationId == location);

                        id.Quantity = id.Quantity - minus;
                    }
                    else if (topping == "Sausage" && items.LocationId == location)
                    {
                        var id = _db.Inventory.FirstOrDefault(g => g.Name == "Sausage" && g.LocationId == location);

                        id.Quantity = id.Quantity - minus;
                    }




                }
            


            SaveChanges();

        }

        public IEnumerable<Inventory> GetInventory()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Inventory> inventory = _db.Inventory.AsNoTracking().ToList();
            return inventory;
        }

        public void AddInventory(string itemName, bool isTopping, int quantity, int location)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var inventory = new Inventory
            {
                Name = itemName,
                IsTopping = isTopping,
                Quantity = quantity,
                LocationId = location
            };
            _db.Add(inventory);
        }

        public void EditInventory(Inventory inventory)
        {
            // would add it if it didn't exist
            _db.Update(inventory);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        //users

        public IEnumerable<Users> GetUsers()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Users> users = _db.Users.AsNoTracking().ToList();
            return users;
        }

        public void AddUsers(string name, string lastName, string phone)
        {
            


            var User = new Users             {
                FirstName = name,
                LastName = lastName,
                Phone = phone,
                LocationId = 1
            };

            _db.Add(User);
        }

        public int? GetUserIDByPhone(string findUser, string phone)
        {

            var user = _db.Users.FirstOrDefault(g => g.FirstName == findUser && g.Phone == phone);
            if (user == null)
            {
                return 1;
            }
            else
            {
                return user.UsersId;
            }


        }


        public void EditUser(Users User)
        {
            // would add it if it didn't exist
            _db.Update(User);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }





        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}