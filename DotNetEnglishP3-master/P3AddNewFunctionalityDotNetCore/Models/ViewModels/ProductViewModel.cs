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

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Price { get; set; }
    }
}

