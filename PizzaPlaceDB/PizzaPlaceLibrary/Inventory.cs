using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Inventory
    {
        public Inventory()
        {
            PizzaTopping = new HashSet<PizzaTopping>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public bool? IsTopping { get; set; }
        public int? Quantity { get; set; }
        public int? LocationId { get; set; }

        public Locations Location { get; set; }
        public ICollection<PizzaTopping> PizzaTopping { get; set; }
    }
}
