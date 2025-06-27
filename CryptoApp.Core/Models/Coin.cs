using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Models
{
    public class Coin
    {
        public Coin(string id, string symbol, string name, string imageUrl)
        {
            Id = id;
            Symbol = symbol;
            Name = name;
            ImageUrl = imageUrl;
        }

        public string Id { get; }
        public string Symbol { get; }
        public string Name { get; }
        public string ImageUrl { get; }
    }
}
