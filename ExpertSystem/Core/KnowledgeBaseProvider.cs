using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpertSystem.Enums;

namespace ExpertSystem.Core
{
    public static class KnowledgeBaseProvider
    {
        private static IDictionary<ProductType, IEnumerable<string>> RulesCodeForProduct { get; set; }

        private static void SetRules()
        {
            RulesCodeForProduct = new Dictionary<ProductType, IEnumerable<string>>();
        }


    }
}