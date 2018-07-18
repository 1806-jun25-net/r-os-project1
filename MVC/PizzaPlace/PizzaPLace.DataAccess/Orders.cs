using System;
using System.Collections.Generic;

namespace PizzaPLace.DataAccess
{
    public partial class Orders
    {
        public Orders()
        {
            OrderPizza = new HashSet<OrderPizza>();
        }

        public int OrderId { get; set; }
        public int? UsersId { get; set; }
        public int? LocationId { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal? Price { get; set; }
        public decimal OrderTotal { get; set; }

        public Locations Location { get; set; }
        public Users Users { get; set; }
        public ICollection<OrderPizza> OrderPizza { get; set; }
    }
}
