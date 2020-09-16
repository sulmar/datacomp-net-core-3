using System;
using System.Collections.Generic;
using System.Text;

namespace DataComp.Training.Models.SearchCriteria
{

    public class ProductSearchCriteria : SearchCriteria
    {
        public string Color { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
