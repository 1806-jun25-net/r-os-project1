using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class OrdersRepository
    {

        private readonly PizzaPlaceContext _db;

        public OrdersRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Orders> GetOrderPizza()
        {
            
            List<Orders> orders = _db.Orders.AsNoTracking().ToList();
            return orders;
        }

        public void AddOrder(int user_id, int location_id, int orderTotal)
        {
            


            var order = new Orders
            {
                UsersId= user_id,
                LocationId = location_id,
                OrderTotal = orderTotal,
            };
            _db.Add(order);
        }


        public void EditOrder(Orders order)
        {
           
            _db.Update(order);

           
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
