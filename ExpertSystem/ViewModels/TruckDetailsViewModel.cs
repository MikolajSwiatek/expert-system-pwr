using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertSystem.Models;

namespace ExpertSystem.ViewModels
{
    public class TruckDetailsViewModel
    {
        public Truck Truck { get; set; }

        public IEnumerable<RequireRule> Rules { get; set; }
    }
}