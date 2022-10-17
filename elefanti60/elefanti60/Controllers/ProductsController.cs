using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        
        // Returns a list of all products and their details
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        // Returns a specific product based on ID
        // Will be used for product page
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByID(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        // Returns products with a similar name
        // Will be used in search page
        [HttpGet("Title/{title}")]
        public async Task<IEnumerable<Product>> GetByTitle(string title)
        {
            return await _context.Products.Where(x => x.Title.ToLower().Contains(title)).ToListAsync();
        }

        // Returns all products belonging to a specific category
        // Will be used to filter products by category
        [HttpGet("Category/{category}")]
        public async Task<IEnumerable<Product>> GetByCategory(string category)
        {
            return await _context.Products.Where(x => x.Category == category).ToListAsync();
        }

        // Creates a new product that needs to belong to an existing category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Product product)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name.Equals(product.Category));

            if(category == null)
            {
                return NotFound();
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = product.Id }, product);
        }

        // Updates product information
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Product product)
        {
            if (id != product.Id) return BadRequest();
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Deletes product from database
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
    }
}
