using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
       /* private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByID(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product == null ? NotFound() : Ok(product);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> DeleteAll()
        {
            var itemsFromCart = await _context.Products.FindAsync();
            _context.Carts.Remove(itemsFromCart);

            return NoContent();
        }
       */
    }
}
