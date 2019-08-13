using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NpvCalculator.Models
{
    public class NpvModel
    {
        public int? CalculationID { get; set; }

        [Display(Name = "Lower Bound Discount Rate (%):")]
        public double LowerBoundDiscountRate { get; set; } = 1;

        [Display(Name = "Upper Bound Discount Rate (%):")]
        public double UpperBoundDiscountRate { get; set; } = 1.5;

        [Display(Name = "Discount Rate Increment (%):")]
        public double DiscountRateIncrement { get; set; } = 0.25;

        [Display(Name = "Initial Investment ($):")]
        public double InitialInvestment { get; set; } = 0;

        public string CommaDelimetedCashFlows { get; set; }

        public List<double> ListOfCashFlows { get; set; }
    }
}