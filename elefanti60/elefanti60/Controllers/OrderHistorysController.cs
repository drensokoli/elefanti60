﻿using elefanti60.Data;
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

        // Returns all Items that the User has Ordered
        // and the total of those ordered items
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHistory>> Get(int id)
        {

            var list = await _context.OrderItems.Where(orderItem => orderItem.UserId == id).ToListAsync();
            decimal total = 0;
            
            foreach (var item in list)
            {
                total = total + item.Total;
            }

            Models.OrderHistory order = new Models.OrderHistory
            {
                OrderedItems = list,
                UserId = id,
                Total = total
            };

            return order;   
        }
    }
}
