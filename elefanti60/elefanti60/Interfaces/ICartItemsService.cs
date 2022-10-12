using elefanti60.Models;

namespace elefanti60.Interfaces
{
    public interface ICartItemsService
    {
        Task<IEnumerable<CartItem>> GetById(int id);
    }
}