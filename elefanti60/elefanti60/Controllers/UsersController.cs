using elefanti60.Data;
using elefanti60.Interfaces;
using elefanti60.Models;
using elefanti60.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUsersService _usersService;
        public UsersController(IUsersService productsServices)
        {
            _usersService = productsServices;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByID(int id)
        {
            var user = await _userService.GetByID(id);

            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("/{user}")]
        public async Task<ActionResult> Login(string user, string password)
        {
            var useri = await _context.Users.FirstAsync(x => x.Username == user && x.Password == password);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(useri);
        }

        [HttpGet("username/{username}")]
        public async Task<IEnumerable<User>> GetByTitle(string username)
        {
            return await _context.Users.Where(x => x.Username.ToLower().Contains(username)).ToListAsync();
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, User user)
        {
            if (id != user.Id) return BadRequest();
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
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

    }
}
