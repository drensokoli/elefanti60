using elefanti60.Data;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Services
{
    public class CartItemsService : ICartItemsService
    {
        private readonly AppDbContext _context;

        public CartItemsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CartItem>> GetById(int id)
        {
            return await _context.CartItems.Where(cartItem => cartItem.UserId == id).ToListAsync();
        }
    }
}
