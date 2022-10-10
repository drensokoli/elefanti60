using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ShoppingCartsController(AppDbContext context)
        {
            _context = context;
        }

        // Returns all Cart Items assigned to the ID given on input
        // and the total of those cart items
        [HttpGet("{id}")]
        public async Task<ShoppingCart> Get(int id)
        {

            var list = await _context.CartItems.Where(cartItem => cartItem.UserId == id).ToListAsync();
            decimal total = 0;

            foreach (var item in list)
            {
                total = total + item.Total;
            }

            ShoppingCart cart = new ShoppingCart
            {
                Items = list,
                UserId = id,
                Total = total
            };

            return cart;
        }

    }
}
