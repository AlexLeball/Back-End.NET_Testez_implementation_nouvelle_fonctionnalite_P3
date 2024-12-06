using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P3AddNewFunctionalityDotNetCore;
using P3AddNewFunctionalityDotNetCore.Data;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the real database context with an in-memory database for testing
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<P3Referential>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<P3Referential>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryIdentityDbForTesting");
            });

            // Optionally seed test data
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<P3Referential>();
                var identityDb = scopedServices.GetRequiredService<AppIdentityDbContext>();

                db.Database.EnsureCreated();
                identityDb.Database.EnsureCreated();

                // Add test data
                SeedTestData(db, identityDb);
            }
        });
    }

    private void SeedTestData(P3Referential db, AppIdentityDbContext identityDb)
    {
        if (!db.Product.Any())
        {
            db.Product.Add(new Product { Id = 1, Name = "Test Product", Price = 10.0 });
            db.SaveChanges();
        }

        if (!identityDb.Users.Any())
        {
            identityDb.Users.Add(new IdentityUser { UserName = "testuser", Email = "testuser@example.com" });
            identityDb.SaveChanges();
        }
    }
}


namespace TestIntegration
{
    public class IntegrationTest 
    {
        private readonly string _connectionString = "P3Referential";
   







        [Fact]
            public void Test1()
            {
            


            }
        
    }
}