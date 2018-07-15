using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class OrderPizzaRepository
    {

        private readonly PizzaPlaceContext _db;

        public OrderPizzaRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<OrderPizza> GetOrderPizza()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<OrderPizza> orderPizza = _db.OrderPizza.AsNoTracking().ToList();
            return orderPizza;
        }

        public void AddOrderPizza(int? order_id, int? pizza_id)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var OrderPizza = new OrderPizza
            {
                OrderId = order_id,
                PizzaId = pizza_id,
               
            };
            _db.Add(OrderPizza);
        }
        public int? GetPizzaIdBySize(string pizza, string size)
        {

            var repo = _db.Pizzas.FirstOrDefault(g => g.Name == pizza);
            if (pizza == null)
            {
                return 0;
            }
            else
            {
               
                return repo.PizzaId;
            }


        }
        public int? GetOrderByUserId(int? findUserId)
        {

            var order = _db.Orders.FirstOrDefault(g => g.UsersId == findUserId);
            if (order == null)
            {
                return 0;
            }

            else
            {
                return order.OrderId;
            }


        }


        public void EditOrderPizza(OrderPizza OrderPizza)
        {
            // would add it if it didn't exist
            _db.Update(OrderPizza);

            // sometimes we need to do it a different way
            //var trackedMovie = _db.Movie.Find(movie.Id);
            //_db.Entry(trackedMovie).CurrentValues.SetValues(movie);
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
