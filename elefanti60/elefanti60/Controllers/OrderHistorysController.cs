using elefanti60.Data;
using elefanti60.Migrations;
using elefanti60.Models;
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

        [HttpGet("/Orders/{id}")]
        public async Task<OrderHistory> Get(int id)
        {
            return await _context.OrderHistory.FindAsync(id);
        }
    }
}
