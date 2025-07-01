using CryptoApp.Core.Contracts;
using CryptoApp.Core.Models;

namespace CryptoApp.DataAccess.Repositories
{
    public interface ITelegramUserRepository
    {
        Task<TelegramUser> GetById(int id);
        Task Add(TelegramUser telegramUser);
        Task AddTransaction(int telegramUserId, CoinTransactionRequest cointTransaction);
        Task<List<CoinTransaction>> GetTransactionsByUserId(int telegramUserId);
        Task DeleteTransaction(Guid transactionId);
        Task DeleteNullIdTransactions(int userId);
    }
}