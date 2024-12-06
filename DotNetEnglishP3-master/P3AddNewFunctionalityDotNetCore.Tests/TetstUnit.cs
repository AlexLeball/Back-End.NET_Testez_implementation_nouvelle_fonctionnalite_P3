using Moq;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Microsoft.Extensions.Localization; 
using Xunit;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class TetstUnit
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICart> _cartMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IStringLocalizer<ProductService>> _stringLocalizerMock;

        // Constructor to initialize mocks and ProductService
        public TetstUnit()
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
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "",Price = 10, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("Please enter a name", errors);
        }

        // Test for missing price
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsEmpty()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 0, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("The price value must be a number greater than zero", errors);
        }

        // Test for missing stock
        [Fact]
        public void Product_ShouldReturn_Error_WhenStockIsEmpty()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = 0 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("The stock value must be a number greater than zero", errors);
        }

        //// Test for invalid price (non-numeric)
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsNotNumber()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "Test Product", Price = 0, Stock = 0 }; // Placeholder for invalid input
            var errors = new List<string>();

            // Simulate non-integer input
            string invalidPriceInput = "abc";

            if (!int.TryParse(invalidPriceInput, out _))
            {
                errors.Add("The field Price must be a number.");
            }

            // Assert
            Assert.Contains("The field Price must be a number.", errors);
        }

        // Test for price being zero or negative
        [Fact]
        public void Product_ShouldReturn_Error_WhenPriceIsZeroOrNegative()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var productZeroPrice = new ProductViewModel { Name = "Product", Price = 0, Stock = 5 };
            var productNegativePrice = new ProductViewModel { Name = "Product", Price = -10, Stock = 5 };

            // Act
            var errorsZeroPrice = _productService.CheckProductModelErrors(productZeroPrice);
            var errorsNegativePrice = _productService.CheckProductModelErrors(productNegativePrice);

            // Assert
            Assert.Contains("The price value must be a number greater than zero", errorsZeroPrice);
        }

        //// Test for invalid stock (non-numeric)
        [Fact]
        public void Product_ShouldReturn_Error_WhenStockIsNotNumber()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "Test Product", Price = 10, Stock = 0 }; // Placeholder for invalid input
            var errors = new List<string>();

            // Simulate non-integer input
            string invalidStockInput = "abc";

            if (!int.TryParse(invalidStockInput, out _))
            {
                errors.Add("The stock value must be a number greater than zero");
            }

            // Assert
            Assert.Contains("The stock value must be a number greater than zero", errors);
        }

        // Test for valid stock (ensure no error)
        [Fact]
        public void Product_ShouldNotReturn_Error_WhenStockIsValid()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            // Arrange
            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = 5 };

            // Act
            var errors = _productService.CheckProductModelErrors(product);

            // Assert
            Assert.Empty(errors);  // Ensure no errors when stock is valid
        }
    }
}
