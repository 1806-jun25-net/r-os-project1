using System;
using System.Collections.Generic;

namespace PizzaPLace.DataAccess
{
    public partial class PizzaTopping
    {
        public int Id { get; set; }
        public int? PizzaId { get; set; }
        public int? ItemId { get; set; }

        public Inventory Item { get; set; }
        public Pizzas Pizza { get; set; }
    }
}
