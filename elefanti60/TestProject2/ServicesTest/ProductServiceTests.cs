using elefanti60.Controllers;
using elefanti60.Interfaces;
using elefanti60.Models;
using elefanti60.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
        private readonly Mock<IProductsService> _productsService = new();
        private readonly ProductsController _producsController;

        public ProductServiceTests()
        {
            _producsController = new ProductsController(_productsService.Object);
        }
        
        [Fact]
        public async Task GetById_ShouldNotBeNull_WhenDataFound()
        {
            //Arrange
            var id = 1;
            var product = new Product();
            _productsService.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(product);
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
        //    var product = new Product();
        //    product.Title = title;
        //    _productsServices.Setup(x => x.GetByTitle(It.IsAny<string>())).ReturnsAsync(product);
        //    //Act
        //    var result = await _producsController.GetByTitle(title);
        //    //Assert
        //    result.Should().NotBeNull();
        //}

        [Fact]
        public async Task Delete_ShouldNotBeNull_WhenDataFound()
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
