using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertSystem.Models
{
    public class ProductRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductRuleID { get; set; }

        public int ProductID { get; set; }

        public int RequireRuleID { get; set; }

        public virtual Product Product { get; set; }

        public virtual RequireRule Rule { get; set; }
    }
}