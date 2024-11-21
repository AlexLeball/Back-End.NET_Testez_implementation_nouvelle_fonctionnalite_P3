using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [RequiredLocalizedAttribute("ErrorStockValue")]
        [Range(1, int.MaxValue, ErrorMessage = "Stock must be at least 1.")]
        public int Stock { get; set; }

        [RequiredLocalizedAttribute("ErrorPriceValue")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public double Price { get; set; }
    }

    
}

