using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Locations
    {
        public Locations()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
            Users = new HashSet<Users>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
