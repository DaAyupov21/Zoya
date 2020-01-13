using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Zoya
{
    class ShoppingContext : DbContext
    {
        public ShoppingContext() : base("DBConnection") { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Courier> Couriers { get; set; }
    }
}
