using Microsoft.EntityFrameworkCore;
using CustomerCRUD.Models;

namespace CustomerCRUD.DataContext
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        public DbSet<Customer>? Customers { get; set; }

    }
}
