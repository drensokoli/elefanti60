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

        // Returns all items in users cart
        [HttpGet("{id}")]
        public async Task<IEnumerable<CartItem>> Get(int id)
        {
            return await _context.CartItems.Where(cartItem =>  cartItem.UserId == id).ToListAsync();
        }

        // Adds a new product to cart
        // Function checks if this product already exists in the users shopping cart
        // If product exists in cart, it checks if the amount about to be added is bigger than the product stock.
        // It respons accordingly by:
        // - Updating existing cart item,
        // - Creating a new cart item,
        // - Or returning a BadRequest() Action Result
        // 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CartItemDTO cartitemdto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == cartitemdto.UserId);
            var product = _context.Products.FirstOrDefault(x => x.Id == cartitemdto.ProductId);
            var item =  _context.CartItems.FirstOrDefault(x => x.ProductId == cartitemdto.ProductId && x.UserId == cartitemdto.UserId);

            if (cartitemdto.Quantity == 0)
            {
                cartitemdto.Quantity = 1;
            }
            if (user == null || product == null)
            {
                return NotFound();
            }

            if (item != null)
            {
                if (product.Stock < item.Quantity + cartitemdto.Quantity)
                {
                    return BadRequest("Stock: " + product.Stock);
                }
                else
                {
                    item.Quantity += cartitemdto.Quantity;
                    item.Total = item.Price * item.Quantity;
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(item);
                }
            }

            if (product.Stock < cartitemdto.Quantity)
            {
                return BadRequest("Stock: " + product.Stock);
            }

            CartItem cartItem = new CartItem()
            {
                UserId = cartitemdto.UserId,
                Title = product.Title,
                Description = product.Description,
                Image = product.Image,
                ProductId = cartitemdto.ProductId,    
                Quantity = cartitemdto.Quantity,
                Price = product.Price,
                Total = cartitemdto.Quantity * product.Price
            };


            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }

        // Updates a specific cart items amount and the shopping carts total based on the changes
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, CartItem cartItem)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);

            if (id != cartItem.Id) return BadRequest();
            cartItem.Total = cartItem.Price * cartItem.Quantity;
            if (product.Stock < cartItem.Quantity)
            {
                return BadRequest("Stock: " + product.Stock);
            }

            _context.Entry(cartItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Removes cart item from shopping cart
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cartItemToDelete = await _context.CartItems.FindAsync(id);

            if (cartItemToDelete == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
