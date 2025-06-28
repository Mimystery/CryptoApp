using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoApp.Core.Contracts
{
    public class TelegramAuthResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("photo_url")]
        public string? PhotoUrl { get; set; }
        [JsonPropertyName("auth_date")]
        public int AuthDate { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}
