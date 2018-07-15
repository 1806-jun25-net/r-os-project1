using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaPlaceLibrary
{
    public class UsersRepository
    {
        private readonly PizzaPlaceContext _db;


        public UsersRepository(PizzaPlaceContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));


        }

        public IEnumerable<Users> GetUsers()
        {
            // we don't need to track changes to these, so
            // skip the overhead of doing so
            List<Users> users = _db.Users.AsNoTracking().ToList();
            return users;
        }

        public void AddUsers(string name, string lastName, string phone)
        {
            // LINQ: First fails by throwing exception,
            // FirstOrDefault fails to just null


            var User = new Users
            {
                FirstName = name,
                LastName = lastName,
                Phone = phone,
                LocationId = 1
            };
            _db.Add(User);
        }

        public int GetUserIDByPhone(string findUser, string phone)
        {

            var user = _db.Users.FirstOrDefault(g => g.FirstName == findUser && g.Phone == phone);
            if (user == null)
            {
                return 0;
            }

            else
            {
                return user.UsersId;
            }


        }


        public void EditUser(Users User)
        {
            // would add it if it didn't exist
            _db.Update(User);

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
