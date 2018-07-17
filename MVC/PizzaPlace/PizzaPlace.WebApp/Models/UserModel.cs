using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPlace.WebApp.Models
{
    public class UserModel
    {
        public int UsersId { get; set; }


        [Display(Name = "Name ")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Last Name ")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Phone Number ")]
        [Required]
        public string Phone { get; set; }

        public int LocationID { get; set; } = 1;
    }
}
