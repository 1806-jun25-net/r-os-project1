using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
   public  class OrdersRepository
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
            
            var OrderLocations = _db.Orders.Where(g => g.LocationId == location_id).OrderBy(g => g.OrderTotal) ;
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


        public void AddOrder(int user_id, int location_id, decimal orderTotal)
        {

            var date_time = DateTime.Now;

            var order = new Orders
            {
                UsersId= user_id,
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
                var orderPizza = allOrderPizza.Where(g => g.OrderId == item.OrderId).OrderBy(g=> g.OrderTime);
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

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
