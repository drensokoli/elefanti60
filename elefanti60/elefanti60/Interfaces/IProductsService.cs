using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;

namespace elefanti60.Interfaces
{
    public interface IProductsService
    {
        Task <Product> GetById(int id);
    }
}
