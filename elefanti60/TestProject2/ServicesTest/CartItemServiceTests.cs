using elefanti60.Controllers;
using elefanti60.Interfaces;
using elefanti60.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject2.ServicesTest
{
    [TestClass]
    public class CartItemServiceTests
    {
        
            private readonly Mock<ICartItemsService> _cartItemsServices = new();
            private readonly CartItemsController _cartItemsController;

            public CartItemServiceTests()
            {
                _cartItemsController = new CartItemsController(_cartItemsServices.Object);
            }

            [Fact]
            public async Task GetById_ShouldReturnCartItems()
            {
                //Arrange
                var id = 1;
                var cartItems = new List<CartItem>() { new CartItem() }.AsEnumerable();
                _cartItemsServices.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(cartItems);
                //Act
                var result = await _cartItemsController.GetById(id);
                //Assert
                result.Should().NotBeNull();
            }

        }
    }
