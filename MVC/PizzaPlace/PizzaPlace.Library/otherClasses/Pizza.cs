using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public class Pizza
    {

        public string Size { set; get; }
        public int Crust { set; get; }
        public string Sauce { set; get; }
        public decimal price { set; get; }
        public string Name { set; get; }
        public int DefaultPizzaInventoryId { set; get; }

        public List<string> Topping = new List<string>();
        public List<int> myToppingsID = new List<int>();


        public Pizza()
        {
          
            Topping.Add("Chicken");
            Topping.Add("Ham");
            Topping.Add("Onions");
            Topping.Add("Sausage");
            Topping.Add("Extra Cheese");
            Topping.Add("Bacon");
            Topping.Add("Pepperoni");
        }
    }
}

