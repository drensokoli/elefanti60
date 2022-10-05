
using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Order_HistroyController : Controller
    {


        private readonly AppDbContext _context;
        public Order_HistroyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        //Pjesa pasi te krijohet shopping cart

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> GetByID(int id)
        //{


        //    return 
        //}


    }
}
