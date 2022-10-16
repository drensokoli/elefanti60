using elefanti60.Controllers;
using elefanti60.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestProject1
{
    [TestClass]
    public class UsersControllerTests : TestBaseClass
    {




        [Fact]
        public async Task Create_ShouldReturnOk_WhenUserCreated()
        {
            var controller = new UsersController(_appDbContext);

            var user = new User
            {

                Id = 0,
                Name = "User",
                LastName = "User",
                Address = "Kosovo",
                CardNumber = 123,
                Amount = 5,
                DOB = new DateTime(2000, 11, 14),
                Email = "Test@Gjirafa.com",
                Password = "11112222",
                Username = "Test Test"

            };

            //Create method test
            var result = (CreatedAtActionResult)controller.Create(user).Result;
            result.StatusCode.Equals((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<User>();

            var userModel = (User)result.Value;

            //Get method test
            var get = controller.Get();
            get.Should().NotBeNull();

            //GetById method test 
            var getById = (OkObjectResult)controller.GetByID(userModel.Id).Result;
            result.StatusCode.Equals((int)HttpStatusCode.OK);

            //GetByTitle method test
            var getByTitle = controller.GetByTitle(userModel.Username).Result;
            getByTitle.Equals(userModel.Username);

            //Update method test
            var update = (NoContentResult)controller.Update(userModel.Id, userModel).Result;
            update.StatusCode.Equals((int)HttpStatusCode.NoContent);



        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenUserDeleted()
        {
            var controller = new UsersController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.Delete(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenUserNotFound()
        {
            var controller = new UsersController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.GetByID(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);

        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenIdIsNotUserId()
        {
            var controller = new UsersController(_appDbContext);

            var id = 999;
            var user = new User { Id = 998 };

            var result = (BadRequestResult)await controller.Update(id, user);
            result.StatusCode.Equals((int)HttpStatusCode.BadRequest);
        }


    }
}
