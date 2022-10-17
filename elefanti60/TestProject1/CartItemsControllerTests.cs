using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1;
using FluentAssertions;
using elefanti60.Controllers;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;

namespace elefanti60Tests
{
    [TestClass]
    public class CartItemsControllerTests : TestBaseClass
    {
        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNoCartItemFoundById()
        {

            var controller = new CartItemsController(_appDbContext);

            var id = 999;

            var result = (NotFoundResult)await controller.Delete(id);
            result.StatusCode.Equals((int)HttpStatusCode.NotFound);
        }
    }
}