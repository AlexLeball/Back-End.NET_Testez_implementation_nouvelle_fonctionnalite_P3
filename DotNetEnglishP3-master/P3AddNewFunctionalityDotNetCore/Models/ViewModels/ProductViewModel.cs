using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
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
        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be a number")]
        [Range(1, int.MaxValue, ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [RequiredLocalizedAttribute("ErrorPriceValue")]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceNotGreaterThanZero")]
        public double Price { get; set; }
    }
}
