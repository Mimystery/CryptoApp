using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Models
{
    public class CoinTransaction 
    {
        public CoinTransaction(Guid id, int telegramUserId, string coinId, string symbol, string name, string imageUrl, double amount, double price, DateTime transactionDate)
        {
            Id = id;
            TelegramUserId = telegramUserId;
            CoinId = coinId;
            Symbol = symbol;
            Name = name;
            ImageUrl = imageUrl;
            Amount = amount;
            Price = price;
            TransactionDate = transactionDate;
        }

        public Guid Id { get; }
        public int TelegramUserId { get; }
        public string CoinId { get; }
        public string Symbol { get; }
        public string Name { get;  }
        public string ImageUrl { get; }
        public double Amount { get; }
        public double Price { get; }
        public DateTime TransactionDate { get; }
    }
}
