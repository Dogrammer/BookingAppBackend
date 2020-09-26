using System;
using System.Collections.Generic;
using System.Text;

namespace BookingCore.ViewModels
{
    public class PricingPeriodDetailViewModel
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double Price { get; set; }
    }
}
