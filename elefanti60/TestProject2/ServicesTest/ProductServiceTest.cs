using elefanti60.Controllers;
using elefanti60.Interfaces;
using elefanti60.Models;
using elefanti60.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject2.ServicesTest
{
    [TestClass]
    public class ProductServiceTest
    {
        private readonly Mock<IProductsService> _productsServices = new();
        private readonly ProductsController _producsController;

        public ProductServiceTest()
        {
            _producsController = new ProductsController(_productsServices.Object);
        }
        [Fact]
        public async Task GetById_ShouldReturnProductWithId()
        {
            //Arrange
            var id = 1;
            var product = new Product();
            _productsServices.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(product);
            //Act
            var result = await _producsController.GetById(id);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
