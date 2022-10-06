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

        [HttpGet("{id}")]
        public async Task<ShoppingCart> Get(int id)
        {
            var list = await _context.CartItems.Where(cartItem => cartItem.UserId == id).ToListAsync();
            ShoppingCart cart = new ShoppingCart
            {
                Items = list
            };
            return cart;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ShoppingCart list = await _context.Cart.Where(cart => cart.Id == id).FirstAsync();
            OrderHistory.ShoppingCarts.Add(list);


            _context.Cart.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
