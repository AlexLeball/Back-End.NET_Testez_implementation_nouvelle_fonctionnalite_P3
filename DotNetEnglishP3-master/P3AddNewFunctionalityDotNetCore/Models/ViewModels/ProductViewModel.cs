using Microsoft.AspNetCore.Mvc.ModelBinding;
using P3AddNewFunctionalityDotNetCore.Resources.ViewModelsRessource;
using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(ProductViewModelResource),
                  ErrorMessageResourceName = "ErrorMissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessageResourceType = typeof(ProductViewModelResource),
                  ErrorMessageResourceName = "ErrorMissingStock")]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(ProductViewModelResource),
               ErrorMessageResourceName = "ErrorStockValue")]
        public int Stock { get; set; }

        [Required(ErrorMessageResourceType = typeof(ProductViewModelResource),
                  ErrorMessageResourceName = "ErrorMissingPrice")]
        [Range(0.01, double.MaxValue, ErrorMessageResourceType = typeof(ProductViewModelResource),
               ErrorMessageResourceName = "ErrorPriceValue")]
        public double Price { get; set; }
    }
}
