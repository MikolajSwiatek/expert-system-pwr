using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertSystem.Models
{
    public class RequireRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequireRuleID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}