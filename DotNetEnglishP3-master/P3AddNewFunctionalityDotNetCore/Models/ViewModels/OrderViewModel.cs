using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zip { get; set; }

        [Required]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
