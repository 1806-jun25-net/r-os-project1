﻿using System;
using System.Collections.Generic;

namespace PizzaPlaceLibrary
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int UsersId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? LocationId { get; set; }

        public Locations Location { get; set; }
        public ICollection<Orders> Orders { get; set; }
    }
}
