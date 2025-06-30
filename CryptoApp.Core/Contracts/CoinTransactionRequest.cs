using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public class CoinTransactionRequest
    {
        public int TelegramUserId { get; set; }
        public string CoinId { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
