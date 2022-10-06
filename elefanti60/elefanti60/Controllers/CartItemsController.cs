using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartItemsController(AppDbContext context)
        {
            _context = context;
        }  

        [HttpGet("{id}")]
        public async Task<IEnumerable<CartItem>> Get(int id)
        {
            return await _context.CartItems.Where(cartItem =>  cartItem.UserId == id).ToListAsync();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CartItemDTO cartitemdto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == cartitemdto.UserId);
            var product = _context.Products.FirstOrDefault(x => x.Id == cartitemdto.ProductId);

            if(user == null || product == null)
            {
                return NotFound();
            }


            CartItem cartItem = new CartItem()
            {
                UserId = cartitemdto.UserId,
                ProductId = cartitemdto.ProductId,
                Quantity = cartitemdto.Quantity,
                Price = product.Price,
                Total = cartitemdto.Quantity * product.Price
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }
    }
}
