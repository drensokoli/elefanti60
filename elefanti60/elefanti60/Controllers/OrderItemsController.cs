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

                if (orderItem.Quantity > product.Stock)
                {
                    return BadRequest("Stock: " + product.Stock);
                }
                product.Stock -= orderItem.Quantity;
                _context.OrderItems.Add(orderItem);
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
