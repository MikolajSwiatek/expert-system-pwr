using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertSystem.Models;

namespace ExpertSystem.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<Truck> Trucks {get; set; }
    }
}