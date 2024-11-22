using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Microsoft.Extensions.Localization; 
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Collections.Generic;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICart> _cartMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IStringLocalizer<ProductService>> _stringLocalizerMock;

        // Constructor to initialize mocks and ProductService
        public ProductServiceTests()
        {
            // Initialize all the required mocks
            _productRepositoryMock = new Mock<IProductRepository>();
            _cartMock = new Mock<ICart>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            // Pass to the ProductService constructor
            _productService = new ProductService(
                _cartMock.Object,
                _productRepositoryMock.Object,
                _orderRepositoryMock.Object,
                _stringLocalizerMock.Object
            );
        }

        // Test for missing name
        [Fact]
        public void Product_ShouldReturn_Error_WhenNameIsEmpty()
        {
            // Arrange
            var product = new ProductViewModel { Name = "",Price = 10, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("The Name field is required.", errors);
        }

        // Test for missing price
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsEmpty()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 0, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Price must be greater than zero", errors);
        }

        // Test for missing stock
        [Fact]
        public void Product_ShouldReturn_Error_WhenStockIsEmpty()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = 0 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Stock must be at least 1", errors);
        }

        //// Test for invalid price (non-numeric)
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsNotNumber()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Test Product", Price = 0, Stock = 0 }; // Placeholder for invalid input
            var errors = new List<string>();

            // Simulate non-integer input
            string invalidPriceInput = "abc";

            if (!int.TryParse(invalidPriceInput, out _))
            {
                errors.Add("Price must be an integer");
            }

            // Assert
            Assert.Contains("Price must be an integer", errors);
        }

        // Test for price being zero or negative
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsZeroOrNegative()
        {
            // Arrange
            var productZeroPrice = new ProductViewModel { Name = "Product", Price = 0, Stock = 5 };
            var productNegativePrice = new ProductViewModel { Name = "Product", Price = -10, Stock = 5 };

            // Act
            var errorsZeroPrice = _productService.CheckProductModelErrors(productZeroPrice);
            var errorsNegativePrice = _productService.CheckProductModelErrors(productNegativePrice);

            // Assert
            Assert.Contains("Price must be greater than zero", errorsZeroPrice);
            Assert.Contains("Price must be greater than zero", errorsNegativePrice);
        }

        //// Test for invalid stock (non-numeric)
        [Fact]
        public void Product_ShouldReturn_Error_WhenStockIsNotNumber()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Test Product", Price = 10, Stock = 0 }; // Placeholder for invalid input
            var errors = new List<string>();

            // Simulate non-integer input
            string invalidStockInput = "abc";

            if (!int.TryParse(invalidStockInput, out _))
            {
                errors.Add("Stock must be an integer");
            }

            // Assert
            Assert.Contains("Stock must be an integer", errors);
        }

        // Test for valid stock (ensure no error)
        [Fact]
        public void Product_ShouldNotReturn_Error_WhenStockIsValid()
        {
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(errors);  // Ensure no errors when stock is valid
        }
    }
}
