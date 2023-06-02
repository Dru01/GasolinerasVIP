using System;
using System.Collections.Generic;
using System.Text;

namespace GasolinerasVIP.Models
{
    public class GasStation
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public Tuple<double, double> Location { get; set; }
        public double PremiumPrice { get; set; }
        public double MagnaPrice { get; set; }
    }
}
