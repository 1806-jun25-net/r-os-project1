using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Inventory
    {
        public Inventory()
        {
            HasTopping = new HashSet<HasTopping>();
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public bool? IsTopping { get; set; }
        public int? Quantity { get; set; }
        public int? LocationId { get; set; }

        public Locations Location { get; set; }
        public ICollection<HasTopping> HasTopping { get; set; }
    }
}
