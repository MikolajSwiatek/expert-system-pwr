using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ExpertSystem.Enums;

namespace ExpertSystem.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        public string Name { get; set; }

        public Unit CapacityUnit { get; set; }

        public ProductType? Type { get; set; }

        public TruckType? TruckType { get; set; }

        public virtual ICollection<ProductRule> Rules { get; set; }
    }
}