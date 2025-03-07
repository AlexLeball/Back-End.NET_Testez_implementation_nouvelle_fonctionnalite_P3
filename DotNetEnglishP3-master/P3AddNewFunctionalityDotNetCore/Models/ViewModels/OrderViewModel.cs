using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using P3AddNewFunctionalityDotNetCore.Resources.ViewModelsRessource;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        // Reference the resource class and resource name for error message localization
        [Required(ErrorMessageResourceName = "ErrorMissingName", ErrorMessageResourceType = typeof(OrderViewModelResource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingAddress", ErrorMessageResourceType = typeof(OrderViewModelResource))]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCity", ErrorMessageResourceType = typeof(OrderViewModelResource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingZipCode", ErrorMessageResourceType = typeof(OrderViewModelResource))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceName = "ErrorMissingCountry", ErrorMessageResourceType = typeof(OrderViewModelResource))]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}

