using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertSystem.Models
{
    public class TruckRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TruckRuleID { get; set; }

        public int TruckID { get; set; }
        
        public int RequireRuleID { get; set; }

        public virtual Truck Truck { get; set; }

        public virtual RequireRule Rule { get; set; }
    }
}