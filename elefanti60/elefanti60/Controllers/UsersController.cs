using elefanti60.Data;
using elefanti60.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace elefanti60.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // Asks if user with this ID exists.
        // Then returns their information if they do. 
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByID(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user == null ? NotFound() : Ok(user);
        }

        //Hash and salt password method
        public static string Hash(string password)
        {

            byte[] salt = Convert.FromBase64String("passwordSalt");

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8)
                );


            return $"{Convert.ToBase64String(salt)}:{hash}";
        }


        // Login method
        // Checks if the username and hashed password on input match the ones in the database
        [HttpPost("/{user}")]
        public async Task<ActionResult> Login(string user, string password)
        {
            var useri = await _context.Users.FirstAsync(x => x.Username == user && x.Password == Hash(password));
            if(user == null)
            {
                return NotFound();
            }
            return Ok(useri.Id);
        }

        // Returns all users containing characters given on input
        [HttpGet("username/{username}")]
        public async Task<IEnumerable<User>> GetByTitle(string username)
        {
            return await _context.Users.Where(x => x.Username.ToLower().Contains(username)).ToListAsync();
        }
       
        // Sign up feature
        // Checks if a user with the same username exists in the database
        // If not, it creates a new user and hashes the password given on input
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(User user)
        {
            var usernames = await _context.Users.ToListAsync();
            foreach (var username in usernames)
            {
                if(username.Username.ToLower() == user.Username)
                {
                    return BadRequest("This user already exists.");
                }
            }

            user.Password = Hash(user.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByID), new { id = user.Id }, user);
        }

        // Update user info and save changes
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

        // Ability to find user by id and delete user
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
