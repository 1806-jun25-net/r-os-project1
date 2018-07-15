using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary.Repositories
{
    class PizzaToppingRepository
    {


        private readonly PizzaPlaceContext _db;

        public PizzaToppingRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<PizzaTopping> GetPizzaTopping()
        {
            
            List<PizzaTopping> pizzaToping = _db.PizzaTopping.AsNoTracking().ToList();
            return pizzaToping;
        }

        public void AddPizzaTopping( int? pizza_id, int item_id)
        {
           


            var hasTopping = new PizzaTopping
            {
                PizzaId = pizza_id,
                ItemId = item_id
              
            };
            _db.Add(hasTopping);
        }
        public void EditPizzaTopping(PizzaTopping hasTopping)
        {
            
            _db.Update(hasTopping);

           
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
