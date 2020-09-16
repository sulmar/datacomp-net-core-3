using System;
using System.Security.Authentication.ExtendedProtection;

namespace DataComp.Training.Models
{
    public class Product : BaseEntity
    {
        public string Color { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
