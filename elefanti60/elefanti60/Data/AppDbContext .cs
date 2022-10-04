using elefanti60.Models;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> Carts { get; set; }
    }
}
