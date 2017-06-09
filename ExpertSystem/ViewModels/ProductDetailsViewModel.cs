using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertSystem.Models;

namespace ExpertSystem.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<RequireRule> Rules { get; set; }
    }
}