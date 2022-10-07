using elefanti60.Data;
using elefanti60.Migrations;
//using elefanti60.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistorysController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrderHistorysController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHistory>> Get(int id)
        {

            var list = await _context.OrderItems.Where(orderItem => orderItem.UserId == id).ToListAsync();
            decimal total = 0;
            foreach (var item in list)
            {
                total = total + item.Total;
            }

            OrderHistory order = new OrderHistory
            {
                OrderedItems = list,
                UserId = id,
                Total = total
            };
            return Ok(order);
        }
    }
}
