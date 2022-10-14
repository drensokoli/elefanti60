using elefanti60.Controllers;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace TestProject2.ServicesTest
{
    [TestClass]
    public class UsersServiceTests
    {
        private readonly Mock<IUsersService> _usersService = new();
        private readonly UsersController  _usersController;

        public UsersServiceTests()
        {
            _usersController = new UsersController(_usersService.Object);
        }
        [Fact]
        public async Task Get_ShouldReturnOk_WhenDataFound()
        {
            //Arrange
            var id = 1;
            var user = new User();
            _usersService.Setup(x => x.GetByID(It.IsAny<int>())).ReturnsAsync(user);
            //Act
            var result = await _usersController.GetByID(id);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
