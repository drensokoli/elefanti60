using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1;
using FluentAssertions;
using elefanti60.Controllers;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace elefanti60Tests
{
    public class CategoriesControllerTests : TestBaseClass
    {
        [Fact]
        public async Task Create_ShouldCreateCategory()
        {
            var controller = new CategoriesController(_appDbContext);

            var category = new Category { Id = 0, Name = "Test" };

            //Create method Test
            var result = (OkObjectResult)controller.Create(category).Result;
            result.StatusCode.Equals((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Category>();

            var categoryModel = (Category)result.Value;

            //Get method Test
            var get = controller.Get();
            get.Should().NotBeNull();


            //Delete method Test
            var deleteResult = (NoContentResult)controller.Delete(categoryModel.Id).Result;
            deleteResult.StatusCode.Equals((int)HttpStatusCode.NoContent);



        }
        [Fact]

        // Delete method test when no cateogry found
        public async Task Delete_ShouldReturNotFound_WhenNoCategoryFound()
        {
            var controller = new CategoriesController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.Delete(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }

        [Fact]

        // Update method test when Id is not the category id
        public async Task Update_ShouldReturnBadRequest_WhenNoIdIsNotCategoryId()
        {
            var controller = new CategoriesController(_appDbContext);

            var id = 5;
            var category = new Category
            {
                Id = 6,
            };

            var result = (BadRequestResult)await controller.Update(id, category);
            result.StatusCode.Equals((int)HttpStatusCode.BadRequest);
        }
    }
}
