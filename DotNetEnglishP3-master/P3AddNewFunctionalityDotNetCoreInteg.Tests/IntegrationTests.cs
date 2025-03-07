using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;


namespace P3AddNewFunctionalityDotNetCoreInteg.Tests
{
    public class ProductIntegrationTests : IDisposable
    {
        private readonly P3Referential _context;
        private readonly ProductService _productService;
        private readonly Cart _cart;
        private readonly OrderRepository _orderRepository;

        public ProductIntegrationTests()
        {
            // In-memory configuration for testing
            var inMemorySettings = new Dictionary<string, string>
            {
                { "ConnectionStrings:P3Referential", "Server=(localdb)\\mssqllocaldb;Database=P3ReferentialTestDB;Trusted_Connection=True;" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            // Use DbContextOptionsBuilder to set up the context
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer(configuration.GetConnectionString("P3Referential"))
                .Options;

            // Initialize the DbContext
            _context = new P3Referential(options, configuration);

            // Ensure the database is fresh for every test
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Initialize concrete implementations
            var productRepository = new ProductRepository(_context);
            _cart = new Cart();
            _orderRepository = new OrderRepository(_context);

            // Initialize ProductService with no mocked dependencies
            _productService = new ProductService(
                _cart,
                productRepository,
                _orderRepository,
                null // No localization for integration tests
            );
        }

        [Fact]
        public void SaveProduct_ShouldPersistToDatabase()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Name = "Integration Test Product",
                Price = 50.0,
                Stock = 10,
                Description = "Integration Test Description",
                Details = "Integration Test Details"
            };

            // Act
            _productService.SaveProduct(productViewModel);

            // Assert
            var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Integration Test Product");
            Assert.NotNull(productFromDb); // Ensure the product exists in the database
            Assert.Equal(50.0, productFromDb.Price); // Validate the price
            Assert.Equal(10, productFromDb.Quantity); // Validate the stock
        }

        [Fact]
        public void DeleteProduct_ShouldRemoveFromDatabase()
        {
            // Arrange
            var productViewModel = new ProductViewModel
            {
                Name = "Test Product to Delete",
                Price = 40.0,
                Stock = 20,
                Description = "Delete Test Description",
                Details = "Delete Test Details"
            };

            _productService.SaveProduct(productViewModel);
            var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Test Product to Delete");
            Assert.NotNull(productFromDb); // Ensure the product was added

            // Act
            _productService.DeleteProduct(productFromDb.Id);

            // Assert
            var deletedProduct = _context.Product.FirstOrDefault(p => p.Name == "Test Product to Delete");
            Assert.Null(deletedProduct); // Ensure the product no longer exists
        }

        // Clean up the context after tests run
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
