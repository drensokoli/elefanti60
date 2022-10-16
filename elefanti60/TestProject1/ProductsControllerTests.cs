using FluentAssertions;
using elefanti60.Controllers;
using elefanti60.Data;
using elefanti60.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace TestProject1
{
    public class ProductsControllerTests : TestBaseClass
    {
        
        
        [Fact]
        /*This method Creates a product and gets all and by its id, then gets it by title then by category and updates it
         then deletes it. So it tests all the products controller methods */

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

            //Get method test
            var get = controller.Get();
            get.Should().NotBeNull();

            //GetById Method Test
            var getById = (OkObjectResult)controller.GetByID(productModel.Id).Result;
            getById.StatusCode.Equals((int)HttpStatusCode.OK);

            //GetByTitle method test
            var getByTitle = controller.GetByTitle(productModel.Title).Result;
            getByTitle.Equals(productModel.Title);

            //GetByCategory method test
            var getByCategory = controller.GetByCategory(productModel.Category).Result;
            getByCategory.Equals(productModel.Category);

            //Update method test
            var update =(NoContentResult)controller.Update(productModel.Id, productModel).Result;
            update.StatusCode.Equals((int)HttpStatusCode.NoContent);

            //Delete method Test
            var deleteResult = (NoContentResult)controller.Delete(productModel.Id).Result;
            deleteResult.StatusCode.Equals((int)HttpStatusCode.NoContent);

        }
        
       

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenDataNotFound()
        {
            var controller = new ProductsController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.Delete(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdIsNotProductId()
        {
            var controller = new ProductsController(_appDbContext);

            var id = 5;
            var product = new Product
            {
                Id = 6,
            };

            var result = (BadRequestResult)await controller.Update(id, product);
            result.StatusCode.Equals((int)HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task GetByCategory_ShouldReturnNotFound_WhenCategoryIsNull()
        {
            var controller = new ProductsController(_appDbContext);

            var product = new Product
            {
                Category = null,
            };

            var result = (NotFoundResult)await controller.Create(product);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }
        [Fact] public async Task GetById_ShouldReturnNotFound_WhenProductIsNotFound()
        {
            var controller = new ProductsController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.GetByID(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }
    }

}