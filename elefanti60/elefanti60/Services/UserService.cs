using elefanti60.Data;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace elefanti60.Services
{
    public class UserService:IUsersService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetByID(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
    }
}
