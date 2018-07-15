using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class InventoryRepository
    {


        private readonly PizzaPlaceContext _db;

        public InventoryRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

                     }

        public bool  CheckInventory(List<string> myToppings, List<Pizza> pizza, int location, bool hasDough, bool hasSauce, List<string> noTopping)
        {
            var repoLocation = new LocationRepository(new PizzaPlaceContext());
            string locationName = repoLocation.GetLocationById(location);
            string sauce = " ";
            bool check = true;
            string size = " ";
            foreach (var s in pizza)
            {
                //set sauce string 

                if (s.Sauce == "1")
                {
                    sauce = "BBQ Sauce";
                }
                else if (s.Sauce == "2")
                {
                    sauce = " Marinara Sauce";
                }
                else
                {
                    sauce = "Alfredo Sauce ";
                }


                size = s.Size;//size of pizza


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
                    
                   if ( d.Quantity < takeout )
                    {
                        hasDough = false;
                        Console.WriteLine(locationName + " Doesn't have any more dough");
                    }
                }
                //has souce?
                if (items.LocationId == location)
                {
                    var d = _db.Inventory.FirstOrDefault(g => g.Name == sauce && g.LocationId == location);
                    if (d.Quantity < takeout)
                    {
                        hasSauce = false;
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

               
            }

            if (noTopping.Count() > 0)
            {
                Console.WriteLine("Sorry " + locationName + " doesn't have in it's inventory: \n ");
                foreach (var topping in noTopping )
                {
                    Console.WriteLine(topping +  " \n");
                }

                check = false;
            }

            return check;
        }
        public void MinusToppings(List<string> myToppings, List<Pizza> pizza, int location)
        {
            string sauce;
            string size =" ";
            foreach (var s in pizza)
            {


                if (s.Sauce == "1")
                {
                    sauce = "BBQ Sauce";
                }
                else if (s.Sauce == "2")
                {
                    sauce = " Marinara Sauce";
                }
                else
                {
                    sauce = "Alfredo Sauce ";
                }


                size = s.Size;//size of pizza


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
                if(items.LocationId == location)
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

        public void AddInventory(string itemName , bool isTopping, int quantity, int location)
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

        public void SaveChanges()
        {
            _db.SaveChanges();
        }



    }
}
