using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class OrderPizza
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaId { get; set; }
        public int? Quantity { get; set; }

        public Orders Order { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
