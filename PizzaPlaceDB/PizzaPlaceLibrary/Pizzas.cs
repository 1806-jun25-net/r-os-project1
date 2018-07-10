using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Pizzas
    {
        public Pizzas()
        {
            HasTopping = new HashSet<HasTopping>();
            OrderPizza = new HashSet<OrderPizza>();
        }

        public int PizzaId { get; set; }
        public string Name { get; set; }
        public bool? Type { get; set; }
        public string Size { get; set; }
        public string Crust { get; set; }

        public ICollection<HasTopping> HasTopping { get; set; }
        public ICollection<OrderPizza> OrderPizza { get; set; }
    }
}
