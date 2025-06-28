using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Models
{
    public class CoinTransaction 
    {
        public string Id { get; }
        public int TelegramUserId { get; }
        public string CoinId { get; }
        public string Symbol { get; }
        public string Name { get; }
        public string ImageUrl { get; }
        public double Amount { get; }
        public double Price { get; }
        public DateTime TransactionDate { get; }
    }
}
