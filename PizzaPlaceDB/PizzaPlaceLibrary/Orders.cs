using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Orders
    {
        public Orders()
        {
            OrderPizza = new HashSet<OrderPizza>();
        }

        public int OrderId { get; set; }
        public int? UsersId { get; set; }
        public byte[] OrderTime { get; set; }
        public int? LocationId { get; set; }
        public double? OrderTotal { get; set; }

        public Locations Location { get; set; }
        public Users Users { get; set; }
        public ICollection<OrderPizza> OrderPizza { get; set; }
    }
}
