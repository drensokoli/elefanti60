using elefanti60.Data;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;

namespace elefanti60.Services
{
    public class ProductsService:IProductsService
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task <Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            return product;
        }
    }
}
