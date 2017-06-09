using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ExpertSystem.Enums;

namespace ExpertSystem.Models
{
    public class Truck
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckID { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Speed { get; set; }

        public Unit? CapacityUnit { get; set; }

        public TruckType? Type { get; set; }

        public TruckStatus? Status { get; set; }

        public virtual ICollection<TruckRule> Rules { get; set; }

        public int LocationID { get; set; }

        public virtual Location Location
        {
            get; set;
        }
    }
}