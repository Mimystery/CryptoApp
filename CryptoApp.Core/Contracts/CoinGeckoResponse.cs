using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public class CoinGeckoResponse
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public Image image { get; set; }

        public class Image
        {
            public string thumb { get; set; }
            public string small { get; set; }
            public string large { get; set; }
        }
    }
}
