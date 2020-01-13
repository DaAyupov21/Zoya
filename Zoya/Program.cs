using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zoya
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ShoppingContext db = new ShoppingContext())
            {
              /*  Stock stock = new Stock { Count = 76, Price = 143.29, Size = 45, Type = "Юбка" };
                db.Stocks.Add(stock);
                db.SaveChanges();*/

               /* Courier courier = new Courier { Name = "Почта России" };
                db.Couriers.Add(courier);
                db.SaveChanges();*/
/*
                Customer customer = new Customer { Email = "as@as.com", Name = "Fill", SecondName = "Mayson", PhoneNumber = "+78005553535" };
                db.Customers.Add(customer);
                db.SaveChanges();*/

                foreach (var s in db.Stocks)
                {
                    Console.WriteLine($"{s.Type}  Количество = {s.Count}, Цена = {s.Price}, Размер = {s.Size}");
                }
                foreach (var c in db.Couriers)
                {
                    Console.WriteLine(c.Name);
                }
                foreach(var c in db.Customers)
                {
                    Console.WriteLine($"{c.Name} {c.SecondName} Email: {c.Email}, Phone: {c.PhoneNumber}");
                }
                //Console.ReadKey();
            }
            //данный участок кода запускает окно программы
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Shop());
        }
    }
}
