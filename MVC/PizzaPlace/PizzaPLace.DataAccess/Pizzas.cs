using System;
using System.Collections.Generic;

namespace PizzaPLace.DataAccess
{
    public partial class Pizzas
    {
        public Pizzas()
        {
            OrderPizza = new HashSet<OrderPizza>();
            PizzaTopping = new HashSet<PizzaTopping>();
        }

        public int PizzaId { get; set; }
        public string Name { get; set; }
        public bool? Type { get; set; }
        public string Size { get; set; }
        public int? Crust { get; set; }
        public decimal? Price { get; set; }

        public ICollection<OrderPizza> OrderPizza { get; set; }
        public ICollection<PizzaTopping> PizzaTopping { get; set; }
    }
}
