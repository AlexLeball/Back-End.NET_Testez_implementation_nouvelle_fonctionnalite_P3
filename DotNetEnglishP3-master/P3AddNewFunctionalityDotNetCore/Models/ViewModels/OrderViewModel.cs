using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingName")]
        public string Name { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingAddress")]
        public string Address { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingCity")]
        public string City { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingZipCode")]
        public string Zip { get; set; }

        [RequiredLocalizedAttribute("ErrorMissingCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}
