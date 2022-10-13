using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;

namespace elefanti60.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> Get();
        Task <Product> GetById(int id);
        Task <IEnumerable<Product>> GetByTitle(string title);   
        Task <Product> Delete(int id);
    }
}
