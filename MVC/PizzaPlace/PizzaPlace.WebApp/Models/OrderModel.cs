using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.WebApp.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public int? UsersId { get; set; }

        public int? LocationId { get; set; }

        public DateTime? OrderTime { get; set; }

        public decimal? Price { get; set; }

        public decimal? OrderTotal { get; set; }

    }
}
