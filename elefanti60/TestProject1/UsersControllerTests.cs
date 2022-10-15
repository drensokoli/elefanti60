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

namespace TestProject1
{
     public class UsersControllerTests:TestBaseClass
    {
        //[Fact] 
        //public async Task Create_ShouldReturnOk_WhenUserCreated()
        //{
        //    var controller = new UsersController(_appDbContext);

        //    var user = new User
        //    {
        //        Id = 0,
        //        Name = "User",
        //        LastName = "User",
        //        Address = "Kosovo",
        //        CardNumber = ""
                
                
               
        //    };

        //    //Create method test
        //    var result = (CreatedAtActionResult)controller.Create(user).Result;
        //    result.StatusCode.Equals((int)HttpStatusCode.OK);
        //    result.Value.Should().BeOfType<User>();

        //    var userModel = (User)result.Value;

        //    //Delete method test
        //    var deleteResult = (NoContentResult)controller.Delete(userModel.Id).Result;
        //    deleteResult.StatusCode.Equals((int)HttpStatusCode.NoContent);
        //}
    }
}
