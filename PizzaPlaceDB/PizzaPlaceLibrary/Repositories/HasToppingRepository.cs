using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary.Repositories
{
    class HasToppingRepository
    {


        private readonly PizzaPlaceContext _db;

        public HasToppingRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<HasTopping> GetHasToppings()
        {
            
            List<HasTopping> hasTopping = _db.HasTopping.AsNoTracking().ToList();
            return hasTopping;
        }

        public void AddToHasTopping( int pizza_id, int item_id)
        {
           


            var hasTopping = new HasTopping
            {
                PizzaId = pizza_id,
                ItemId = item_id
              
            };
            _db.Add(hasTopping);
        }
        public void EditHasTopping(HasTopping hasTopping)
        {
            
            _db.Update(hasTopping);

           
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
