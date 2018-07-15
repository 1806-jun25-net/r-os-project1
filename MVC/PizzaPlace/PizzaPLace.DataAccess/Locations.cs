using System;
using System.Collections.Generic;

namespace PizzaPLace.DataAccess
{
    public partial class Locations
    {
        public Locations()
        {
            Inventory = new HashSet<Inventory>();
            Orders = new HashSet<Orders>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public ICollection<Inventory> Inventory { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
