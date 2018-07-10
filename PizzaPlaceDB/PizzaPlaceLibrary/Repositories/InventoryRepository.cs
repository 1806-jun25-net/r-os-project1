using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class InventoryRepository
    {


        private readonly PizzaPlaceContext _db;

        public InventoryRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Inventory> GetInventory()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Inventory> inventory = _db.Inventory.AsNoTracking().ToList();
            return inventory;
        }

        public void AddInventory(string itemName , bool isTopping, int quantity, int location)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var inventory = new Inventory
            {
                Name = itemName,
                IsTopping = isTopping,
                Quantity = quantity,
                LocationId = location
            };
            _db.Add(inventory);
        }
        public void EditInventory(Inventory inventory)
        {
            // would add it if it didn't exist
            _db.Update(inventory);

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
