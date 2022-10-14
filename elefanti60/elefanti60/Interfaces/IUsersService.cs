using elefanti60.Models;

namespace elefanti60.Interfaces
{
    public interface IUsersService
    {
         Task<User> GetByID(int id);
    }
}
