using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaPlace.Library;
using PizzaPlace.WebApp.Models;
using PizzaPLace.DataAccess;
using PizzaPlaceLibrary;

namespace PizzaPlace.WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly PizzaPlaceContext _context;
        public OrdersRepository Repo { get; }

        public UsersController(PizzaPlaceContext context, OrdersRepository repo)
        {
            _context = context;
            Repo = repo;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        // GET: Users
        public IActionResult Home()
        {
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsersId,FirstName,LastName,LocationId,Phone")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }



        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }
        //GET: Users/Register
        public IActionResult Register()
        {

            
            UserModel user = new UserModel();
            return View(user);
        }


        //POST: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Register(UserModel user)
        {
            bool isUser = false;
            

            var allUsers = Repo.GetUsers();

            foreach (var oneUser in allUsers)
            {
                if (oneUser.FirstName == user.Name && oneUser.Phone == user.Phone)
                {
                    user.UsersId = oneUser.UsersId;
                    isUser = true;             
                    goto newUser;
                }
               
            }

            newUser:
            if(isUser == true)
            {
                var myUser = new Users
                {
                    UsersId = user.UsersId,
                    FirstName = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                   
                };
                
            }
            else if (isUser == false)
            {


                //create new user
                Repo.AddUsers(user.Name, user.LastName, user.Phone);
                Repo.SaveChanges();
                
               
                
            }
            TempData["msg"] = "user id " + user.UsersId;

            return RedirectToAction("ChooseLocation","Locations", user);
           // return View(user);

        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsersId,FirstName,LastName,LocationId,Phone")] Users users)
        {
            if (id != users.UsersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UsersId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult SearchUser()
        {
            Users user = new Users();
            return View(user);
        }


        //search user 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearcUser(Users user)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            Console.WriteLine(configuration.GetConnectionString("PizzaPlace"));
            var optionsBuilder = new DbContextOptionsBuilder<PizzaPlaceContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPlace"));
            ////////////////////////////////////////////////////////////////////////////////////////

            //var repo = new UserRepository(new PizzaPalacedbContext(optionsBuilder.Options));
            var use = Repo.GetUsertable();
            var ELUSER = use.FirstOrDefault(e => e.FirstName == user.FirstName);
            if (ELUSER == null)
            {
                TempData["Error"] = "Error: User not found";
            }
            else
            {
                TempData["id"] = ELUSER.UsersId;
                TempData["name"] = ELUSER.FirstName;
                TempData["last"] = ELUSER.LastName;
                TempData["phone"] = ELUSER.Phone;
                if (ELUSER.LocationId == 1)
                {
                    TempData["loc"] = "Herndon Pizza Place";
                }
                else if (ELUSER.LocationId == 2)
                {
                    TempData["loc"] = "Reston Pizza Place";
                }
               
            }
            return View();
        }

        public IActionResult UserOrder()
        {
            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserOrder(Users user)
        {
            ////////////////////////////////////////////////////////////////////////////////////////
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            Console.WriteLine(configuration.GetConnectionString("PizzaPalacedb"));
            var optionsBuilder = new DbContextOptionsBuilder<PizzaPlaceContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("PizzaPalacedb"));
            ////////////////////////////////////////////////////////////////////////////////////////


            var repo = new OrdersRepository(new PizzaPlaceContext(optionsBuilder.Options));
            var users = Repo.GetUsertable(); // Get all user
            var userorder = users.FirstOrDefault(g => g.FirstName == user.FirstName); // searching user
            var userID = user.UsersId; // user ID
            var won = Repo.GetOrdersTable(); // Get all Order
            var order = won.Where(q => q.UsersId == userID); // All user order
            var pizzas = Repo.GetOrderPizza(); // Get all Pizza
            var PizzasUser = pizzas.Where(q => q.UsersId == userID); // pizza of order

            foreach (var item in order)
            {
                TempData["Order ID:"] = item.OrderId;
                TempData["Order ID:"] = item.OrderTime;

                foreach (var item2 in pizzas)
                {


                }
            }
            return View();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }
    }
}
