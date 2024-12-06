using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using System.Linq;
using System.Collections.Generic;
using P3AddNewFunctionalityDotNetCore.Models.Entities;

public class ProductIntegrationTests : IDisposable
{
    private readonly P3Referential _context;

    public ProductIntegrationTests()
    {
        // Load configuration from appsettings.test.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)  // Ensure it gets the correct directory
            .AddJsonFile("appsettings.test.json")  // Add the test-specific configuration file
            .Build();

        // Set up the database context using the test database connection string
        var options = new DbContextOptionsBuilder<P3Referential>()
            .UseSqlServer(configuration.GetConnectionString("P3Referential"))  // Use test DB connection string
            .Options;

        _context = new P3Referential(options);

        // Ensure the database is clean and created before tests run
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    // Test for adding a product
    [Fact]
    public void AddProduct_ShouldPersistToDatabase_WithoutInconsistencies()
    {
        // Arrange
        var newProduct = new Product
        {
            Name = "Test Product",
            Price = 30.0,
            Quantity = 25,
            Description = "Test Description",
            Details = "Test Details"
        };

        // Act: Add the product to the database
        _context.Product.Add(newProduct);
        _context.SaveChanges();

        // Assert: Ensure the product exists in the database
        var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Test Product");

        Assert.NotNull(productFromDb); // Ensure the product is in the database
        Assert.Equal(30.0, productFromDb.Price); // Validate price
        Assert.Equal(25, productFromDb.Quantity); // Validate quantity
        Assert.Equal("Test Description", productFromDb.Description); // Validate description
        Assert.Equal("Test Details", productFromDb.Details); // Validate details

        // Ensure no duplicate entries
        var productCount = _context.Product.Count(p => p.Name == "Test Product");
        Assert.Equal(1, productCount); // Ensure there's exactly one product in the database with this name
    }

    // Test for removing a product
    [Fact]
    public void DeleteProduct_ShouldRemoveFromDatabase_WithoutInconsistencies()
    {
        // Arrange: Add a product to the database
        var newProduct = new Product
        {
            Name = "Test Product to Delete",
            Price = 40.0,
            Quantity = 35,
            Description = "Test Description",
            Details = "Test Details"
        };

        _context.Product.Add(newProduct);
        _context.SaveChanges();

        // Act: Remove the product from the database
        _context.Product.Remove(newProduct);
        _context.SaveChanges();

        // Assert: Ensure the product is removed from the database
        var productFromDb = _context.Product.FirstOrDefault(p => p.Name == "Test Product to Delete");
        Assert.Null(productFromDb); // Ensure the product is no longer in the database

        // Ensure no orphaned or inconsistent records remain
        var allProducts = _context.Product.ToList();
        Assert.DoesNotContain(newProduct, allProducts); // Ensure the deleted product is not in the list anymore
    }

    // Clean up the context after tests run
    public void Dispose()
    {
        _context.Dispose();
    }
}
