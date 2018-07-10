using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class HasTopping
    {
        public int Id { get; set; }
        public int? PizzaId { get; set; }
        public int? ItemId { get; set; }

        public Inventory Item { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
