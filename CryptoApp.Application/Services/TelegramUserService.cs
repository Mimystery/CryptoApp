using CryptoApp.Application.Authentication;
using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;
using CryptoApp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Application.Services
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly ITelegramUserRepository _tgUserRepository;
        private readonly IJwtService _jwtService;

        public TelegramUserService(ITelegramUserRepository tgUserRepository, IJwtService jwtService)
        {
            _tgUserRepository = tgUserRepository;
            _jwtService = jwtService;
        }

        public async Task<TelegramUser> GetTelegramUserById(int id)
        {
            return await _tgUserRepository.GetById(id);
        }

        public async Task<string> LoginTelegramUser(TelegramUser telegramUser)
        {
            var existingTgUser = await _tgUserRepository.GetById(telegramUser.Id);
            if (existingTgUser == null)
            {
                await AddTelegramUser(telegramUser);
            }

            return _jwtService.GenerateToken(telegramUser);
        }

        public bool VerifyTelegramAuth(TelegramAuthResponse telegramUser)
        {
            var botToken = "7656094747:AAEc_AyJ_FbJ_LtLaaE2SCgUwGB2kL5CdSU";

            var dataDict = new SortedDictionary<string, string>();

            dataDict["auth_date"] = telegramUser.AuthDate.ToString();
            dataDict["first_name"] = telegramUser.FirstName;
            dataDict["id"] = telegramUser.Id.ToString();

            if (!string.IsNullOrEmpty(telegramUser.LastName))
                dataDict["last_name"] = telegramUser.LastName;
            if (!string.IsNullOrEmpty(telegramUser.Username))
                dataDict["username"] = telegramUser.Username;
            if (!string.IsNullOrEmpty(telegramUser.PhotoUrl))
                dataDict["photo_url"] = telegramUser.PhotoUrl;

            // Составляем строку: key=value\nkey=value\n...
            var dataCheckString = string.Join("\n", dataDict.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            // Генерируем секретный ключ
            var secretKey = SHA256.HashData(Encoding.UTF8.GetBytes(botToken));
            using var hmac = new HMACSHA256(secretKey);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
            var hexHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hexHash == telegramUser.Hash.ToLower();
        }

        public async Task AddTelegramUser(TelegramUser telegramUser)
        {
            var existingTgUser = await _tgUserRepository.GetById(telegramUser.Id);
            if (existingTgUser != null)
            {
                throw new InvalidOperationException($"Telegram user with ID {telegramUser.Id} already exists.");
            }

            await _tgUserRepository.Add(telegramUser);
        }

        public async Task AddTransaction(int telegramUserId, CoinTransactionRequest coinTransaction)
        {
            Console.WriteLine($"Added transaction in Service: CoinId={coinTransaction.CoinId}, Symbol={coinTransaction.Symbol}, Amount={coinTransaction.Amount}, Date={coinTransaction.TransactionDate}, TgUserId={telegramUserId}");
            await _tgUserRepository.AddTransaction(telegramUserId, coinTransaction);
        }

        public async Task<List<CoinTransaction>> GetTransactionsByTelegramUserId(int telegramUserId)
        {
            return await _tgUserRepository.GetTransactionsByUserId(telegramUserId);
        }

        public async Task<List<CoinSummaryResponse>> GetSummaryOnEveryCoin(int telegramUserId)
        {
            var transactions = await _tgUserRepository.GetTransactionsByUserId(telegramUserId);
            var summary = transactions
                .GroupBy(t => t.Symbol)
                .Select(s => new CoinSummaryResponse
                {
                    CoinId = s.First().CoinId,
                    Symbol = s.Key,
                    Name = s.First().Name,
                    ImageUrl = s.First().ImageUrl,
                    TotalAmount = s.Sum(t => t.Amount),
                    AveragePrice = s.Average(t => t.Price),
                    TotalCost = s.Sum(t => t.Amount * t.Price),
                }).ToList();

            return summary;

        }

        public async Task DeleteTransactionById(Guid transactionid)
        {
            await _tgUserRepository.DeleteTransaction(transactionid);
        }

        public async Task DeleteNullIdTransactions(int userId)
        {
            await _tgUserRepository.DeleteNullIdTransactions(userId);
        }
    }
}
