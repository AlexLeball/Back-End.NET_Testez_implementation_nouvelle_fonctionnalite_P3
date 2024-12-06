using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using Xunit;

namespace P3AddNewFunctionalityDotNetCoreInteg.Tests
{
    public class ProductIntegrationTests : IDisposable
    {
        private readonly P3Referential _context;
        private readonly ProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly ICart _cart;
        private readonly IOrderRepository _orderRepository;
        private readonly IStringLocalizer<ProductService> _stringLocalizerMock;

        // Constructor where we configure the context to connect to SQL Server
        public ProductIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=P3ReferentialTestDB;Trusted_Connection=True;") // Use your real test database connection string here
                .Options;

            _context = new P3Referential(options);

            // Optionally, you can clear and reset the database here (before each test) if needed.
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public void AddProduct_ShouldPersistToDatabase()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Test Product 2",
                Price = 30.0,
                Quantity = 25
            };

            // Act
            _context.Product.Add(newProduct);
            _context.SaveChanges();

            // Assert
            var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Test Product 2");

            Assert.NotNull(productFromDb); // Ensure the product exists in the database
            Assert.Equal(30.0, productFromDb.Price); // Validate the price
            Assert.Equal(25, productFromDb.Quantity); // Validate the stock
        }

        // test for updates brought by the administator if deleted or not
        [Fact]
        public void DeleteProduct_ShouldRemoveFromDatabase()
        {
            // Arrange
            var newProduct = new Product
            {
                Name = "Test Product 3",
                Price = 40.0,
                Quantity = 35
            };

            _context.Product.Add(newProduct);
            _context.SaveChanges();

            // Act
            _context.Product.Remove(newProduct);
            _context.SaveChanges();

            // Assert
            var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Test Product 3");

            Assert.Null(productFromDb); // Ensure the product does not exist in the database
        }

        // Clean up the context after tests run
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
