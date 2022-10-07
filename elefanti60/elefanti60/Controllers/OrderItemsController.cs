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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create([FromBody] CartItemDTO orderItemDTO)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == orderItemDTO.UserId);
            var product = _context.Products.FirstOrDefault(x => x.Id == orderItemDTO.ProductId);
            var list = await _context.CartItems.Where(cartItem => cartItem.UserId == user.Id).ToListAsync();
            OrderItem orderItem = null;

            foreach (var item in list)
            {
                orderItem = new OrderItem()
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Total

                };
            }

            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();

            //await _context.CartItems.RemoveRange();
            return Ok(orderItem);
        }
    }
}
