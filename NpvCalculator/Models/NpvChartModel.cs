using System.Collections.Generic;

namespace NpvCalculator.Models
{
    public class NpvChartModel
    {
        public List<string> Labels { get; set; } = new List<string>();

        public List<double> Values { get; set; } = new List<double>();
    }
}