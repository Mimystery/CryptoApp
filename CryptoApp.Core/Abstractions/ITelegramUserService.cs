using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;

namespace CryptoApp.Application.Services
{
    public interface ITelegramUserService
    {
        Task<TelegramUser> GetTelegramUserById(int id);
        Task AddTelegramUser(TelegramUser telegramUser);
        public bool VerifyTelegramAuth(TelegramAuthResponse telegramUser);
        Task<string> LoginTelegramUser(TelegramUser telegramUser);
        Task AddTransaction(int telegramUserId, CoinTransactionRequest coinTransaction);
        Task<List<CoinTransaction>> GetTransactionsByTelegramUserId(int telegramUserId);
        Task<List<CoinSummaryResponse>> GetSummaryOnEveryCoin(int telegramUserId);
    }
}