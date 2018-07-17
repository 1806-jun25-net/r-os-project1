using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaPlace.Library;
using PizzaPlace.WebApp.Models;
using PizzaPLace.DataAccess;
using PizzaPlaceLibrary;

namespace PizzaPlace.WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly PizzaPlaceContext _context;
        public OrdersRepository Repo;
        public int Count = 0;
        public decimal Order_total = 0;

        public OrdersController(PizzaPlaceContext context, OrdersRepository repo)
        {
            _context = context;
            Repo = repo;

        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var pizzaPlaceContext = _context.Orders.Include(o => o.Location).Include(o => o.Users);
            return View(await pizzaPlaceContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        //GET: place order
        public ActionResult PlaceOrder(Users user)
        {

            ViewModel myModel = new ViewModel();

            return View(myModel);
        }

        //POST: placer order
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(IFormCollection viewCollection,ViewModel myModel, Users user)
        {
            
            PizzaModel newPizza = new PizzaModel();
            OrderModel order = new OrderModel();

            ViewData["msg"] = "Add a Pizza";
            if (Count == 11)
            {
                ViewData["msg"] = "You can create only one more pizza, (max of 12 pizzas per order)";
            }
            else if(Count ==12)
            {
                ViewData["msg"] = "You have reach your maximun of pizzas per order (12 pizzas), Please place your order ";
                return RedirectToAction(nameof(PlaceOrder));
            }

            //ViewModel myModel = new ViewModel();

            newPizza.Crust = int.Parse(viewCollection["SelectedCrust"].ToString());
            newPizza.Size = viewCollection["SelectedSize"];
            newPizza.Name = viewCollection["SelectedPizza"];

            string sauce = viewCollection["SelectedSauce"];




            //  order.LocationId = user.LocationId;
            order.LocationId = user.LocationId;
            order.OrderTotal = 0;
            order.UsersId = user.UsersId;



            var toppings = new List<string>();
            toppings.Add(newPizza.Name);

            Pizzas pizza = new Pizzas
            {
                Crust = newPizza.Crust,
                Size = newPizza.Size,
                Name = newPizza.Name
            };

            //calculate price
            decimal S = 6, M = 8, L = 12;
            //quantity of topppings batch for pizza size

            if (pizza.Size == "S")
            {
                pizza.Price = S;
                Order_total += S;
            }
            else if (pizza.Size == "M")
            {
                pizza.Price = M;
                Order_total += M;

            }
            else
            {
                pizza.Price = L;
                Order_total += L;

            }
            

            //check availability of ingredients for each pizza
            bool availability = Repo.CheckInventory(toppings, pizza, sauce, order.LocationId);
            if (availability == true)
            {
                // take out toppings from db
                Repo.MinusToppings(toppings, pizza, sauce, order.LocationId);

                //add pizza
                Repo.AddPizzas(pizza.Size, pizza.Price, pizza.Name, pizza.Crust);
                Repo.SaveChanges();

                if (Count < 1)//only if the order is new is going to be created
                {
                    //add order
                    Repo.AddOrder(user.UsersId, user.LocationId, Order_total);

                }
                Count++;// increment counter to know next time, that this is not  a new order.

                //add orderPizza
                int? order_id = Repo.GetOrderByUserId(user.UsersId);
                int? pizza_id = Repo.GetPizzaIdBySize(pizza.Name, pizza.Size);
                Repo.AddOrderPizza(order_id, pizza_id);
                   

            }
            else
            {
                ViewData["msg"] = "Not enough reseources to complete your order, Please choose again...";
                return RedirectToAction(nameof(PlaceOrder));
            }


            return RedirectToAction(nameof(PlaceOrder));


        }

        //GET: invoice
    
        public IActionResult Invoice()
        {

            var orders = _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == 26);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        ////POST: invoice
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Invoice()
        //{
        //    return View();
        //}

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name");
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UsersId,LocationId,OrderTime,Price,OrderTotal")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UsersId,LocationId,OrderTime,Price,OrderTotal")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "Name", orders.LocationId);
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "FirstName", orders.UsersId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
