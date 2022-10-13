using elefanti60.Data;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Services
{
    public class ProductsService:IProductsService
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task <IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task <Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            return product;
        }
        public async Task<IEnumerable<Product>> GetByTitle(string title)
        {
            return await _context.Products.Where(x => x.Title.ToLower().Contains(title)).ToListAsync();
        }
        public async Task<Product> Delete(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            return productToDelete;
        }
    }
}
