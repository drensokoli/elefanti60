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
    public class ProductServiceTests
    {
        private readonly Mock<IProductsService> _productsServices = new();
        private readonly ProductsController _producsController;

        public ProductServiceTests()
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

        //[Fact]
        //public async Task GetByTitle_ShouldReturnProductByTitle()
        //{
        //    //Arrange
        //    var title = "Telefon";
        //    var product = new Product() ;
        //    product.Title = title;  
        //    _productsServices.Setup(x => x.GetByTitle(title)).Returns(product.Title);
        //    //Act
        //    var result = await _producsController.GetByTitle(title);
        //    //Assert
        //    result.Should().NotBeNull();
        //}

        [Fact]
        public async Task Delete_Should()
        {
            //Arrange
            var id = 2;
            var product = new Product();
            _productsServices.Setup(x => x.Delete(It.IsAny<int>())).ReturnsAsync(product);
            //Act
            var result = await _producsController.Delete(id);
            //Assert
            result.Should().NotBeNull();
        }
    }
}
