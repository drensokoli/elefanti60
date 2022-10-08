using Microsoft.VisualStudio.TestTools.UnitTesting;
using elefanti60.Controllers;
using elefanti60.Data;
using elefanti60.Models;
using Microsoft.EntityFrameworkCore;
using Assert = Xunit.Assert;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetProductsTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "elefanti60")
           .Options;

            using (var context = new AppDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Products.Add(
                    new Product { Id = 1, Category = "telefon", Description = "Smartphone", Price = 1600, Title = "iPhone 14 Pro Max", Stock = 5 }
                    );
                context.Products.Add(
                    new Product { Id = 2, Category = "laptop", Description = "laptopi", Price = 1000, Title = "laptop lenovo", Stock = 5 }
                    );
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                ProductsController controller = new ProductsController(context);
                IEnumerable<Product> products = await controller.Get();

                 Assert.Equal(2, products.Count());
            }
        }
    }
}