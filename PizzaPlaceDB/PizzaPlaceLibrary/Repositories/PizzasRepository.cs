using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class PizzasRepository
    {

        private readonly PizzaPlaceContext _db;

        public PizzasRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Pizzas> GetPizzas()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Pizzas> pizzas = _db.Pizzas.AsNoTracking().ToList();
            return pizzas;
        }

        public void AddPizzas( string size, decimal price, string name, int crust)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var Pizza = new Pizzas
            {
                Name = name,
                Price = price,
                Size = size,
                Crust = crust
            };
            _db.Add(Pizza);
        }


        public void EditPizza(Pizzas Pizza)
        {
            // would add it if it didn't exist
            _db.Update(Pizza);

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
