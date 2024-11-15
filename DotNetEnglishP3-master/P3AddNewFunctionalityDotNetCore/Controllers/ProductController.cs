﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P3AddNewFunctionalityDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly IStringLocalizer<ProductController> _localizer;

        public ProductController(IProductService productService, ILanguageService languageService, IStringLocalizer<ProductController> localizer)
        {
            _productService = productService;
            _languageService = languageService;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductViewModel> products = _productService.GetAllProductsViewModel();
            return View(products);
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View(_productService.GetAllProductsViewModel().OrderByDescending(p => p.Id));
        }

        [Authorize]
        public ViewResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                var additionalErrors = _productService.CheckProductModelErrors(product);

                foreach (var error in additionalErrors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(product);
            }

            _productService.SaveProduct(product);
            return RedirectToAction("Admin");
        }


        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            _productService.DeleteProduct(id);
            return RedirectToAction("Admin");
        }
    }
}
