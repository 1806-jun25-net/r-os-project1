using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.WebApp.Models
{
    public class PizzaModel
    {
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public bool? Type { get; set; }
        public string Size { get; set; }
        public int Crust { get; set; }
        public decimal? Price { get; set; }
        public string Sauce { get; set; }
    }
}
