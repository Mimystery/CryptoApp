using CryptoApp.Core.Models;

namespace CryptoApp.DataAccess.Repositories
{
    public interface ITelegramUserRepository
    {
        Task<TelegramUser> GetById(int id);
        Task Add(TelegramUser telegramUser);
    }
}