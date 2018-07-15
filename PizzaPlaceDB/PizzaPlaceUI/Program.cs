using System;
using PizzaPlaceLibrary;

namespace PizzaPlaceUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //var repo = new UsersRepository(new PizzaPlaceContext());
            //var users = repo.GetUsers();

            //foreach (var item in users)
            //{
            //    Console.WriteLine($"FirstName: {item.FirstName}," +
            //        $" LastName: {item.LastName}");
            //}
            //Console.ReadLine();

            //Console.WriteLine("name: ");
            //string name = Console.ReadLine();
            // Console.WriteLine("telefono: ");
            // string telefono = Console.ReadLine();
             var repo =  new OrdersRepository (new PizzaPlaceContext());

            // repo.GetUserOrder("Yessy", "Rios");

            int location_id = 1;

            var locations = repo.GetLocationOrders(location_id);
            foreach (var item in locations)
            {
                Console.WriteLine("Location: 1 " + " Order No. " + item.OrderId + " \n Order Date & time: " + item.OrderTime + "\n Order total: " + item.OrderTotal);
            }
            Console.WriteLine("------------------");
            var cheapest = repo.GetLocationOrdersMostExpensive(location_id);
            foreach (var item in locations)
            {
                Console.WriteLine("Location: 1 " + " Order No. " + item.OrderId + " \n Order Date & time: " + item.OrderTime + "\n Order total: " + item.OrderTotal);
            }
            Console.WriteLine("------------------");

            Console.ReadLine();




       




            //Order newOrder = new Order();


            //do
            //{



            //    newOrder.Menu();



            //} while (false);



        }
    }
}
