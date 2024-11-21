//using Moq;
//using P3AddNewFunctionalityDotNetCore.Models.Repositories;
//using P3AddNewFunctionalityDotNetCore.Models.Services;
//using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
//using System.Reflection;
//using Xunit;

//namespace P3AddNewFunctionalityDotNetCore.Tests
//{
//    public class ProductServiceTests
//    {
//        /// <summary>
//        /// initialize the ProductService with a mock of the ProductRepository
//        /// </summary>
//        private readonly ProductService _productService;
//        private readonly Mock<IProductRepository> _productRepositoryMock;

//        /// <summary>
//        /// Take this test method as a template to write your test method.
//        /// A test method must check if a definite method does its job:
//        /// returns an expected value from a particular set of parameters
//        /// </summary>
//        [Fact]
//        public void Product_Return_MissingNameError_WhenNameIsEmpty()
//        {

//        // Arrange
//        var product = new ProductViewModel { Name = "", Price = 10, Stock = "5" };

//        // Act
//        var errors = _productService.CheckProductModelErrors(product);

//        // Assert
//        Assert.Contains("Name is required", errors);

//        }


//        //Missing Price

//        [Fact]
//        public void Product_Return_MissingPriceError_WhenPriceIsEmpty()
//        {
//            // Arrange
//            var product = new ProductViewModel { Name = "Product", Price = 7, Stock = "5" };

//            // Act
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert
//            Assert.Contains("Price is required", errors);
//        }

//        //Missing Stock

//        [Fact]
//        public void Product_Return_MissingStockError_WhenStockIsEmpty()
//        {
//            // Arrange
//            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = "0" };

//            // Act
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert
//            Assert.Contains("Stock is required", errors);
//        }

//        //Price is not a number

//        [Fact]
//        public void Product_Return_PriceNotNumberError_WhenPriceIsNotNumber()
//        {
//            // Arrange: Set Price as an empty string, which is invalid.
//            var product = new ProductViewModel { Name = "Product", Price = "", Stock = "5" };

//            // Act: Call the service method to check for errors
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert: Check that the error message for invalid Price is returned
//            Assert.Contains("Price must be a number", errors);
//        }
//        //Price is zero or negative

//        [Fact]
//        public void Product_Return_PriceNotGreaterThanZeroError_WhenPriceIsZeroOrNegative()
//        {
//            // Arrange
//            var product = new ProductViewModel { Name = "Product", Price = -10, Stock = "5" };

//            // Act
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert
//            Assert.Contains("Price must be greater than zero", errors);
//        }

//        //Quantity is not a number

//        [Fact]
//        public void Product_Return_StockNotNumberError_WhenStockIsNotNumber()
//        {
//            // Arrange
//            var product = new ProductViewModel { Name = "Product", Price = 1, Stock = "a" };

//            // Act
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert
//            Assert.Contains("Stock must be a number", errors);
//        }

//        //Quantity is zero or negative
//        [Fact]
//       public void Product_ShouldReturn_PriceNotGreaterThanZeroError_WhenPriceIsZeroOrNegative()
//        {
//            // Arrange
//            var productZeroPrice = new ProductViewModel { Name = "Test Product", Price = 0, Stock = "5" };
//            var productNegativePrice = new ProductViewModel { Name = "Test Product", Price = -10, Stock = "5" };

//            // Act
//            var errorsZeroPrice = _productService.CheckProductModelErrors(productZeroPrice);
//            var errorsNegativePrice = _productService.CheckProductModelErrors(productNegativePrice);

//            // Assert
//            Assert.Contains("PriceNotGreaterThanZero", errorsZeroPrice);
//            Assert.Contains("PriceNotGreaterThanZero", errorsNegativePrice);
//        }

//        //quantity exists
//        [Fact]
//        public void Product_Return_StockExistsError_WhenStockExists()
//        {
//            // Arrange
//            var product = new ProductViewModel { Name = "Product", Price = 10, Stock = "5" };

//            // Act
//            var errors = _productService.CheckProductModelErrors(product);

//            // Assert
//            Assert.DoesNotContain("Stock must be greater than zero", errors);
//        }
//    }
//}