using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public class CoinSummaryResponse
    {
        public string CoinId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double TotalAmount { get; set; }
        public double AveragePrice { get; set; }
        public double TotalCost { get; set; }
        public double Invested { get; set; }
        public double Earned { get; set; }
        public double InvestedOnSold { get; set; }
    }
}
