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


            Order newOrder = new Order();

            do
            {

               

                newOrder.Menu();



            } while (false);


        }
    }
}
