using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.Models
{
    public class TelegramUser
    {
        public int Id { get; set; }
        public int AuthDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Hash { get; set; }
        public string Username { get; set; }
        public string? PhotoUrl { get; set; }
        public List<CoinTransaction> Transactions { get; set; }
    }
}
