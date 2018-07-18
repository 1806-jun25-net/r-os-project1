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
        public decimal Order_total;
        public int Count;

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
            int orderid = int.Parse(TempData.Peek("orderid").ToString());
            //decimal order_total = decimal.Parse(TempData.Peek("order_total").ToString());
            //Repo.UpdateTotal(order_total, orderid);
            //var  order_pizzas = Repo.GetPizzasofOrder(orderid);



            var orders = await _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == orderid);


            //foreach (var item in order_pizzas)
            //{
            //    TempData["pizza_name"] = item.Name;
            //    TempData["pizza_size"]= item.Size;
            //    TempData["pizza_price"] = item.Price;
            //}

            //var oders2 = _context.Orders.Where(q => q.OrderId == orderid);



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
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(IFormCollection viewCollection,ViewModel myModel, Users user)
        {
            //int userid = int.Parse(viewCollection["UseridTD"].ToString());
            //TempData["result"] = userid;
           
            int UseridTD = int.Parse(TempData.Peek("userid").ToString());
            int LocationTD = int.Parse(TempData.Peek("locationid").ToString());
            string NameTD = TempData.Peek("firstname").ToString();
            string LastnameTD = TempData.Peek("lastname").ToString();
            string PhoneTD = TempData.Peek("phone").ToString();


            PizzaModel newPizza = new PizzaModel();
            OrderModel order = new OrderModel();

            ViewData["msg"] = "Add a Pizza";
            if (Count == 11)
            {
                ViewData["msg"] = "You can create only one more pizza, (max of 12 pizzas per order)";
            }
            else if(Count == 12)
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


                Order_total = int.Parse(TempData.Peek("order_total").ToString());
                Order_total += S;
                TempData["order_total"] = Order_total;
            }
            else if (pizza.Size == "M")
            {
                pizza.Price = M;
                Order_total = int.Parse(TempData.Peek("order_total").ToString());
                Order_total += M;
                TempData["order_total"] = Order_total;

            }
            else
            {
                pizza.Price = L;
                Order_total = int.Parse(TempData.Peek("order_total").ToString());
                Order_total += L;
                TempData["order_total"] = Order_total;

            }
            

            //check availability of ingredients for each pizza
            bool availability = Repo.CheckInventory(toppings, pizza, sauce, LocationTD);
            if (availability == true)
            {
                // take out toppings from db
                Repo.MinusToppings(toppings, pizza, sauce, LocationTD);

                //add pizza
                Repo.AddPizzas(pizza.Size, pizza.Price, pizza.Name, pizza.Crust);
                Repo.SaveChanges();

                if (TempData.Peek("Count").ToString() == "1")
                {
                    Count = 1;

                }
                else
                {
                    Count = int.Parse(TempData.Peek("Count").ToString());
                }


                if (Count < 2)//only if the order is new is going to be created
                {
                    //add order
                    Repo.AddOrder(UseridTD, LocationTD, Order_total);

                }
                // increment counter to know next time, that this is not  a new order.
                Count++;
                TempData["Count"] = Count;
                

                //add orderPizza
                int? order_id = Repo.GetOrderByUserId(UseridTD);
                int? pizza_id = Repo.GetPizzaIdBySize(pizza.Name, pizza.Size);
                TempData["orderid"] = order_id;
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
    
        public IActionResult Invoice(int? id)
        {
            int orderid = int.Parse(TempData.Peek("orderid").ToString());

            var orders = _context.Orders
                .Include(o => o.Location)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.OrderId == orderid );


           

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

        public ActionResult Search(string sortOrder, string searchString)
        {

            ViewBag.Message = "Welcome to the Orders!";
            ViewModel search = new ViewModel
            {
               Pizzas = Repo.GetPizza(),
                Orders = Repo.GetOrdersTable(),
                Users = Repo.GetUsers(),
                OrderPizzas = Repo.GetOrderPizzas()

                
            };

            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            search.Orders = from s in _context.Orders
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                search.Users = search.Users.Where(s => s.FirstName == searchString || s.Phone == searchString || s.LastName == searchString);
                foreach (var user in search.Users)
                {
                    search.Orders = search.Orders.Where(s => s.UsersId == user.UsersId);
                                           
                }
                
            }

            switch (sortOrder)
            {
                case "Price":
                    search.Orders = search.Orders.OrderBy(s => s.OrderTotal);
                    break;
                case "Price_desc":
                    search.Orders = search.Orders.OrderByDescending(s => s.OrderTotal);
                    break;
                case "Date":
                    search.Orders = search.Orders.OrderBy(s => s.OrderTime);
                    break;
                case "Date_desc":
                    search.Orders = search.Orders.OrderByDescending(s => s.OrderTime);
                    break;
                default:
                    search.Orders = search.Orders.OrderBy(s => s.OrderTotal);
                    break;
            }

            return View(search);
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

    }
}
