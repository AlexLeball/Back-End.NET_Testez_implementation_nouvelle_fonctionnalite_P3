﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;

namespace P3AddNewFunctionalityDotNetCore.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly ICart _cart;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStringLocalizer<ProductService> _localizer;

        public ProductService(ICart cart, IProductRepository productRepository,
            IOrderRepository orderRepository, IStringLocalizer<ProductService> localizer)
        {
            _cart = cart;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _localizer = localizer;
        }
        public List<ProductViewModel> GetAllProductsViewModel()
        {

            IEnumerable<Product> productEntities = GetAllProducts();
            return MapToViewModel(productEntities);
        }

        private static List<ProductViewModel> MapToViewModel(IEnumerable<Product> productEntities)
        {
            List<ProductViewModel> products = new();
            foreach (Product product in productEntities)
            {
                products.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Stock = product.Quantity,
                    Price = product.Price,
                    Name = product.Name,
                    Description = product.Description,
                    Details = product.Details
                });
            }

            return products;
        }

        public List<Product> GetAllProducts()
        {
            IEnumerable<Product> productEntities = _productRepository.GetAllProducts();
            return productEntities?.ToList();
        }

        public ProductViewModel GetProductByIdViewModel(int id)
        {
            List<ProductViewModel> products = GetAllProductsViewModel().ToList();
            return products.Find(p => p.Id == id);
        }


        public Product GetProductById(int id)
        {
            List<Product> products = GetAllProducts().ToList();
            return products.Find(p => p.Id == id);
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            return product;
        }

        public async Task<IList<Product>> GetProduct()
        {
            var products = await _productRepository.GetProduct();
            return products;
        }
        public void UpdateProductQuantities()
        {
            Cart cart = (Cart)_cart;
            foreach (CartLine line in cart.Lines)
            {
                _productRepository.UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }

        public List<string> CheckProductModelErrors(ProductViewModel product)
        {
            var modelErrors = new List<string>();

            // Perform model validation using Data Annotations
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(product, validationContext, validationResults, validateAllProperties: true);

            foreach (var validationResult in validationResults)
            {
                //// Use _localizer to fetch the localized message
                string localizedMessage = validationResult.ErrorMessage;
                modelErrors.Add(localizedMessage);
            }
            return modelErrors;
        }



        public void SaveProduct(ProductViewModel product)
        {
            var productToAdd = MapToProductEntity(product);
            _productRepository.SaveProduct(productToAdd);
        }

        private static Product MapToProductEntity(ProductViewModel product)
        {
            Product productEntity = new()
            {
                Name = product.Name,
                Price = product.Price,
                Quantity = Convert.ToInt32(product.Stock),
                Description = product.Description,
                Details = product.Details
            };
            return productEntity;
        }


        public void DeleteProduct(int id)
        {
            // TODO what happens if a product has been added to a cart and has been later removed from the inventory ?
            // delete the product form the cart by using the specific method
            // => the choice is up to the student
            _cart.RemoveLine(GetProductById(id));

            _productRepository.DeleteProduct(id);
        }
    }
}
