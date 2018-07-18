using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaPLace.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.WebApp.Models
{
    public class ViewModel
    {
        public IEnumerable<Pizzas> Pizzas { get; set; }
        public IEnumerable<Orders> Orders { get; set; }
        public IEnumerable<Users> Users { get; set; }
        public IEnumerable<OrderPizza> OrderPizzas { get; set; }

        public int Count { get; set; } = 1;

        [Required]
        public SelectListItem SelectedSauce { get; set; }

        [Required]
        public SelectListItem SelectedPizza { get; set; }

        [Required]
        public SelectListItem SelectedCrust { get; set; }

        [Required]
        public SelectListItem SelectedSize { get; set; }

       public int UseridTD { get; set; }




    }
}
