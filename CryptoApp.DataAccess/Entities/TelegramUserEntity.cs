using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public class TelegramUserEntity
    {
        public int Id { get; set; }
        public int AuthDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Hash { get; set; }
        public string Username { get; set; }
        public string? PhotoUrl { get; set; }
        public List<CoinTransactionEntity> Transactions { get; set; } = new List<CoinTransactionEntity>();
    }
}
