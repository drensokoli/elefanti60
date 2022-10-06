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
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> Cart { get; set; }

        public DbSet<OrderHistory> OrderHistory { get; set; }


    }
}
