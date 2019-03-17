using System;
using System.Collections.Generic;

namespace Ecommerce.API.Models
{
    public class CheckoutSummaryInfo
    {
        public List<CheckoutProductInfo> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
