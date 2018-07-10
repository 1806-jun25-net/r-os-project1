using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    class LocationRepository
    {
        private readonly PizzaPlaceContext _db;

        public LocationRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Locations> GetLocations()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Locations> Locations = _db.Locations.AsNoTracking().ToList();
            return Locations;
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
