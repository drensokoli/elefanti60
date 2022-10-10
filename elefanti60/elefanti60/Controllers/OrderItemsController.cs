using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<OrderItem>> Get(int id)
        {
            return await _context.OrderItems.Where(orderItem => orderItem.UserId == id).ToListAsync();
        }

        // Makes a new order of all the items in the users shopping cart
        // Function checks if user has enough credit to purchase the items in their shopping cart
        // Function checks if there are enough products in stock for the order quantity
        // If there is enough credit and enough stock:
        // 1. The credit of the user will be subtracted by the order total
        // 2. The stock of the product will be subtracted by the order amount
        // 3. The order will be saved in the OrderItems table in the database
        // 4. Cart Items will be removed from the database

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] OrderItemDTO orderItemDTO)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == orderItemDTO.UserId);
            var list = await _context.CartItems.Where(cartItem => cartItem.UserId == user.Id).ToListAsync();


            OrderItem orderItem = null;

            foreach (var item in list)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == item.ProductId);
                orderItem = new OrderItem()
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Total
                };  

                if (user.Amount < orderItem.Total)
                {
                    return BadRequest("Rrespekt gjitkujt, veresi askujt.\n" +
                        "Credit amount: "+user.Amount);
                }

                if (orderItem.Quantity > product.Stock)
                {
                    return BadRequest("Stock: " + product.Stock);
                }
                user.Amount -= orderItem.Total;
                product.Stock -= orderItem.Quantity;
                _context.OrderItems.Add(orderItem);
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return Ok("Order succesful. \n " +
                "Credit te mbetur: "+user.Amount);
        }
    }
}
