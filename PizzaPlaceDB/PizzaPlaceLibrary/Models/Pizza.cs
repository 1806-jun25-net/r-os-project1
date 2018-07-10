using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public class Pizza
    {

        public string Size { set; get; }
        public string Crust { set; get; }
        public string Sauce { set; get; }

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

