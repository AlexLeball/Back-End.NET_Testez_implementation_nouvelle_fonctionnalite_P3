using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class RequiredLocalized : RequiredAttribute
    {
        private readonly string _resourceKey;

        public RequiredLocalized(string resourceKey)
        {
            _resourceKey = resourceKey; 
        }

    }
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [RequiredLocalized("ErrorMissingName")]
        public string Name { get; set; }

        [RequiredLocalized("ErrorMissingAddress")]
        public string Address { get; set; }

        [RequiredLocalized("ErrorMissingCity")]
        public string City { get; set; }

        [RequiredLocalized("ErrorMissingZipCode")]
        public string Zip { get; set; }

        [RequiredLocalized("ErrorMissingCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
