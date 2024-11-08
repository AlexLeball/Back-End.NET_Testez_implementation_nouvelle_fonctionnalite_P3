using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        private readonly string _resourceKey;

        public RequiredLocalizedAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Obtain the localizer for ProductViewModel
            var localizer = (IStringLocalizer<ProductViewModel>)validationContext.GetService(typeof(IStringLocalizer<ProductViewModel>));

            if (localizer == null)
            {
                throw new InvalidOperationException("IStringLocalizer<ProductViewModel> not found in service provider.");
            }

            // Check if value is null or whitespace
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                // Fetch localized error message using resource key
                var errorMessage = localizer[_resourceKey]?.Value ?? $"Missing required field: {_resourceKey}";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }

    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [RequiredLocalized("ErrorMissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [RequiredLocalized("ErrorStockValue")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Stock must be a number")]
        [Range(1, int.MaxValue, ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

        [RequiredLocalized("ErrorPriceValue")]
        [Range(0.01, double.MaxValue, ErrorMessage = "PriceNotGreaterThanZero")]
        public double Price { get; set; }
    }
}
