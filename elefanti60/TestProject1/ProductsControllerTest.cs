using FluentAssertions;
using elefanti60.Controllers;
using elefanti60.Data;
using elefanti60.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace TestProject1
{
    public class ProductsControllerTest : TestBaseClass
    {
        
        
        [Fact]
        public async Task Create_ShouldReturnOk_WhenProductCreated()
        {

            var controller = new ProductsController(_appDbContext);

            var product = new Product
            {
                Id = 0,
                Title = "Test",
                Description = "Test",
                Category = "Telefon",
                Image = "Test",
                Stock = 10,
                Price = 100
            };

            //Create method Test
            var result = (CreatedAtActionResult)controller.Create(product).Result;
            result.StatusCode.Equals((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Product>();

            var productModel = (Product)result.Value;

            //GetById Method Test
            var getResult = (OkObjectResult)controller.GetByID(productModel.Id).Result;
            getResult.StatusCode.Equals((int)HttpStatusCode.OK);

            //Delete method Test
            var deleteResult = (NoContentResult)controller.Delete(productModel.Id).Result;
            deleteResult.StatusCode.Equals((int)HttpStatusCode.NoContent);
        }

        //[Fact]
        //public async Task GetById_ShouldReturnOk_WhenDataFound()
        //{

        //    var controller = new ProductsController(_appDbContext);
        //    var id = 2;
        //    var result = (OkResult)await controller.GetByID(id);

        //    result.StatusCode.Equals((int)HttpStatusCode.OK);
        //}
        //[Fact]
        //public async Task GetById_ShouldReturnNotFound_WhenDataFound()
        //{

        //    var controller = new ProductsController(_appDbContext);
        //    var id = 1;
        //    var result = (NotFoundResult)await controller.GetByID(id);

        //    result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        //}

        [Fact]
        public async Task Delete_ShouldReturnNotFound()
        {
            var controller = new ProductsController(_appDbContext);

            var id = 7;

            var result = (NotFoundResult)await controller.Delete(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }
    }

}