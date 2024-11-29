using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models;
using Microsoft.AspNetCore.Identity;
using P3AddNewFunctionalityDotNetCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P3AddNewFunctionalityDotNetCore;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;

// This file is the entry point of the application.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Localization builder
builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

//Services and Repositories
builder.Services.AddSingleton<ICart, Cart>();
builder.Services.AddSingleton<ILanguageService, LanguageService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
    .AddDataAnnotationsLocalization();


builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en"),      // Default language
        new CultureInfo("en-GB"),
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("fr"),
        new CultureInfo("es-ES"),
        new CultureInfo("es")
    };

    opts.DefaultRequestCulture = new RequestCulture("en"); // Default to "en"
    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
});


// Database configuration
builder.Services.AddDbContext<P3Referential>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("P3Referential")));
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("P3Identity")));
builder.Services.AddDefaultIdentity<IdentityUser>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) 
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.SeedDatabase(app.Configuration);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Localization configuration
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(""),
    SupportedCultures = new[] { new CultureInfo("en"), new CultureInfo("fr"), new CultureInfo("es") },
    SupportedUICultures = new[] { new CultureInfo("en"), new CultureInfo("fr"), new CultureInfo("es") }
});



app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add the endpoints to the request pipeline.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

await IdentitySeedData.EnsurePopulated(app);

app.Run();
